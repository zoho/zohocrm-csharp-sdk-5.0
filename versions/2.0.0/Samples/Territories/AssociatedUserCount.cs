using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Territories.APIException;
using AssociatedUsersCount = Com.Zoho.Crm.API.Territories.AssociatedUsersCount;
using AssociatedUsersCountWrapper = Com.Zoho.Crm.API.Territories.AssociatedUsersCountWrapper;
using Info = Com.Zoho.Crm.API.Territories.Info;
using MinifiedTerritory = Com.Zoho.Crm.API.Territories.MinifiedTerritory;
using ResponseHandler = Com.Zoho.Crm.API.Territories.ResponseHandler;
using TerritoriesOperations = Com.Zoho.Crm.API.Territories.TerritoriesOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Territories
{
	public class AssociatedUserCount
	{
		public static void GetAssociatedUsercount_1()
		{
			TerritoriesOperations territoriesOperations = new TerritoriesOperations();
			APIResponse<ResponseHandler> response = territoriesOperations.GetAssociatedUserCount();
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
					if (responseHandler is AssociatedUsersCountWrapper)
					{
						AssociatedUsersCountWrapper responseWrapper = (AssociatedUsersCountWrapper) responseHandler;
						List<AssociatedUsersCount> territoryList = responseWrapper.AssociatedUsersCount;
						foreach (AssociatedUsersCount territorycount in territoryList)
						{
							Console.WriteLine ("AssociatedUsersCount count: " + territorycount.Count);
							MinifiedTerritory territory = territorycount.Territory;
							if (territory != null)
							{
								Console.WriteLine ("AssociatedUsersCount Name: " + territory.Name);
								Console.WriteLine ("AssociatedUsersCount ID: " + territory.Id);
								Console.WriteLine ("AssociatedUsersCount Subordinates: " + territory.Subordinates);
							}
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.PerPage != null)
							{
								Console.WriteLine ("Territory Info PerPage: " + info.PerPage);
							}
							if (info.Count != null)
							{
								Console.WriteLine ("Territory Info Count: " + info.Count);
							}
							if (info.Page != null)
							{
								Console.WriteLine ("Territory Info Page: " + info.Page);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("Territory Info MoreRecords: " + info.MoreRecords);
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
                GetAssociatedUsercount_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}