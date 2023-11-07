using System;
using System.Reflection;
using System.Collections.Generic;
using UserTypeUsersOperations = Com.Zoho.Crm.API.UserTypeUsers.UserTypeUsersOperations;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.UserTypeUsers.APIException;
using Info = Com.Zoho.Crm.API.UserTypeUsers.Info;
using ResponseHandler = Com.Zoho.Crm.API.UserTypeUsers.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.UserTypeUsers.ResponseWrapper;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Usertypeusers
{
    public class GetUsersOfUserType
	{
		public static void GetUsersOfUserType_1(string portalName, long userTypeId)
		{
			UserTypeUsersOperations userTypeOperations = new UserTypeUsersOperations();
			ParameterMap paramInstance = new ParameterMap();
			APIResponse<ResponseHandler> response = userTypeOperations.GetUsersOfUserType(userTypeId, portalName, paramInstance);
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
						List<Com.Zoho.Crm.API.UserTypeUsers.Users> users = responseWrapper.Users;
						foreach (Com.Zoho.Crm.API.UserTypeUsers.Users user in users)
						{
							Console.WriteLine ("Users PersonalityId: " + user.PersonalityId);
							Console.WriteLine ("Users Confirm: " + user.Confirm);
							Console.WriteLine ("Users StatusReasonS: " + user.StatusReasonS);
							Console.WriteLine ("Users InvitedTime: " + user.InvitedTime);
							Console.WriteLine ("Users Module: " + user.Module);
							Console.WriteLine ("Users Name: " + user.Name);
							Console.WriteLine ("Users Active: " + user.Active);
							Console.WriteLine ("Users Email: " + user.Email);
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.PerPage != null)
							{
								Console.WriteLine ("Users Info PerPage: " + info.PerPage);
							}
							if (info.Count != null)
							{
								Console.WriteLine ("Users Info Count: " + info.Count);
							}
							if (info.Page != null)
							{
								Console.WriteLine ("Users Info Page: " + info.Page);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("Users Info MoreRecords: " + info.MoreRecords);
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
				else if (response.StatusCode != 204)
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
				string portalName = "PortalsAPItest100";
				long userTypeId = 440248001304019;
                GetUsersOfUserType_1(portalName, userTypeId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}