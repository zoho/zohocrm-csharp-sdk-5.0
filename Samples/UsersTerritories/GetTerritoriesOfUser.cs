using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.UsersTerritories.APIException;
using Info = Com.Zoho.Crm.API.UsersTerritories.Info;
using Manager = Com.Zoho.Crm.API.UsersTerritories.Manager;
using ResponseHandler = Com.Zoho.Crm.API.UsersTerritories.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.UsersTerritories.ResponseWrapper;
using Territory = Com.Zoho.Crm.API.UsersTerritories.Territory;
using UsersTerritoriesOperations = Com.Zoho.Crm.API.UsersTerritories.UsersTerritoriesOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Usersterritories
{
	public class GetTerritoriesOfUser
	{
		public static void GetTerritoriesOfUser_1(long userId)
		{
			UsersTerritoriesOperations usersTerritoriesOperations = new UsersTerritoriesOperations();
			APIResponse<ResponseHandler> response = usersTerritoriesOperations.GetTerritoriesOfUser(userId);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (new List<int>(){ 204, 304}.Contains(response.StatusCode))
				{
					Console.WriteLine (response.StatusCode == 204 ? "No Content" : "Not Modified");
					return;
				}
				if (response.IsExpected)
				{
					ResponseHandler responseHandler = response.Object;
					if (responseHandler is ResponseWrapper)
					{
						ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;
						List<Territory> usersTerritory = responseWrapper.Territories;
						foreach (Territory territory in usersTerritory)
						{
							Console.WriteLine ("User Territory ID: " + territory.Id);
							Manager manager = territory.Manager;
							if (manager != null)
							{
								Console.WriteLine ("User Territory Manager Name: " + manager.Name);
								Console.WriteLine ("User Territory Manager ID: " + manager.Id);
							}
							Manager reportingTo = territory.ReportingTo;
							if (reportingTo != null)
							{
								Console.WriteLine ("User Territory ReportingTo Name: " + reportingTo.Name);
								Console.WriteLine ("User Territory ReportingTo ID: " + reportingTo.Id);
							}
							Console.WriteLine ("User Territory Name: " + territory.Name);
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.PerPage != null)
							{
								Console.WriteLine ("User Info PerPage: " + info.PerPage);
							}
							if (info.Count != null)
							{
								Console.WriteLine ("User Info Count: " + info.Count);
							}
							if (info.Page != null)
							{
								Console.WriteLine ("User Info Page: " + info.Page);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("User Info MoreRecords: " + info.MoreRecords);
							}
						}
					}
					else if (responseHandler is APIException)
					{
						APIException exception = (APIException) responseHandler;
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
				long userId = 4402480254001;
                GetTerritoriesOfUser_1(userId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}