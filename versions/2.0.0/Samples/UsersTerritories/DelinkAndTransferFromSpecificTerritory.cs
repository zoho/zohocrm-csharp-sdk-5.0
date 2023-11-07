using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.UsersTerritories.APIException;
using SuccessResponse = Com.Zoho.Crm.API.UsersTerritories.SuccessResponse;
using TransferActionHandler = Com.Zoho.Crm.API.UsersTerritories.TransferActionHandler;
using TransferActionResponse = Com.Zoho.Crm.API.UsersTerritories.TransferActionResponse;
using TransferActionWrapper = Com.Zoho.Crm.API.UsersTerritories.TransferActionWrapper;
using TransferAndDelink = Com.Zoho.Crm.API.UsersTerritories.TransferAndDelink;
using TransferToUser = Com.Zoho.Crm.API.UsersTerritories.TransferToUser;
using TransferWrapper = Com.Zoho.Crm.API.UsersTerritories.TransferWrapper;
using UsersTerritoriesOperations = Com.Zoho.Crm.API.UsersTerritories.UsersTerritoriesOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Usersterritories
{
	public class DelinkAndTransferFromSpecificTerritory
	{
		public static void DelinkAndTransferFromSpecificTerritory_1(long userId, long territoryId)
		{
			UsersTerritoriesOperations usersTerritoriesOperations = new UsersTerritoriesOperations();
			TransferWrapper request = new TransferWrapper();
			List<TransferAndDelink> userTerritoryList = new List<TransferAndDelink>();
			TransferAndDelink territory = new TransferAndDelink();
			TransferToUser transferToUser = new TransferToUser();
			transferToUser.Id = 3477067065;
			territory.TransferToUser = transferToUser;
			userTerritoryList.Add (territory);
			request.TransferAndDelink = userTerritoryList;
			APIResponse<TransferActionHandler> response = usersTerritoriesOperations.DelinkAndTransferFromSpecificTerritory(territoryId, userId, request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					TransferActionHandler actionHandler = response.Object;
					if (actionHandler is TransferActionWrapper)
					{
						TransferActionWrapper responseWrapper = (TransferActionWrapper) actionHandler;
						List<TransferActionResponse> actionResponses = responseWrapper.TransferAndDelink;
						foreach (TransferActionResponse actionResponse in actionResponses)
						{
							if (actionResponse is SuccessResponse)
							{
								SuccessResponse successResponse = (SuccessResponse) actionResponse;
								Console.WriteLine ("Status: " + successResponse.Status.Value);
								Console.WriteLine ("Code: " + successResponse.Code.Value);
								Console.WriteLine ("Details: ");
								foreach (KeyValuePair<string, object> entry in successResponse.Details)
								{
									Console.WriteLine (entry.Key + ": " + entry.Value);
								}
								Console.WriteLine ("Message: " + successResponse.Message);
							}
							else if (actionResponse is APIException)
							{
								APIException exception = (APIException) actionResponse;
								Console.WriteLine ("Status: " + exception.Status.Value);
								Console.WriteLine ("Code: " + exception.Code.Value);
								Console.WriteLine ("Details: ");
								foreach (KeyValuePair<string, object> entry in exception.Details)
								{
									Console.WriteLine (entry.Key + ": " + entry.Value);
								}
								Console.WriteLine ("Message: " + exception.Message);
							}
						}
					}
					else if (actionHandler is APIException)
					{
						APIException exception = (APIException) actionHandler;
						Console.WriteLine ("Status: " + exception.Status.Value);
						Console.WriteLine ("Code: " + exception.Code.Value);
						Console.WriteLine ("Details: ");
						foreach (KeyValuePair<string, object> entry in exception.Details)
						{
							Console.WriteLine (entry.Key + ": " + entry.Value);
						}
						Console.WriteLine ("Message: " + exception.Message);
					}
				}
				else
				{
                    Model responseObject = response.Model;
                    Type type = responseObject.GetType();
                    Console.WriteLine("Type is : {0}", type.Name);
                    PropertyInfo[] props = type.GetProperties();
                    Console.WriteLine("Properties (N = {0}) :", props.Length);
                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) in {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) in <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
				}
			}
		}
		public static void Call()
		{
			try
			{
				Environment environment = USDataCenter.PRODUCTION;
				IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL" ).Build();
				new Initializer.Builder().Environment(environment).Token(token).Initialize();
				long userId = 34770611709;
				long territoryId = 34770613051397;
                DelinkAndTransferFromSpecificTerritory_1(userId, territoryId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}