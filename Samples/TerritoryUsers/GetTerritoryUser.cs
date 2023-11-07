
using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.TerritoryUsers.APIException;
using Info = Com.Zoho.Crm.API.TerritoryUsers.Info;
using ResponseHandler = Com.Zoho.Crm.API.TerritoryUsers.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.TerritoryUsers.ResponseWrapper;
using TerritoryUsersOperations = Com.Zoho.Crm.API.TerritoryUsers.TerritoryUsersOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Territoryusers
{
    public class GetTerritoryUser
	{
		public static void GetTerritoryUser_1(long user, long territory)
		{
			TerritoryUsersOperations territoryUsersOperations = new TerritoryUsersOperations();
			APIResponse<ResponseHandler> response = territoryUsersOperations.GetTerritoryUser(user, territory);
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
						ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;
						List<Com.Zoho.Crm.API.Users.Users> users = responseWrapper.Users;
						if (users != null)
						{
							foreach (Com.Zoho.Crm.API.Users.Users user1 in users)
							{
								Console.WriteLine ("Territory User ID : " + user1.Id);
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
				long territory = 440248001305227;
				long user = 4402480254001;
                GetTerritoryUser_1(user, territory);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}