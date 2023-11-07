using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using PortalUserTypeOperations = Com.Zoho.Crm.API.PortalUserType.PortalUserTypeOperations;
using APIException = Com.Zoho.Crm.API.PortalUserType.APIException;
using Owner = Com.Zoho.Crm.API.PortalUserType.Owner;
using PersonalityModule = Com.Zoho.Crm.API.PortalUserType.PersonalityModule;
using ResponseHandler = Com.Zoho.Crm.API.PortalUserType.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.PortalUserType.ResponseWrapper;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Portalusertype
{
	public class GetUserTypes
	{
		public static void GetUserTypes_1(string portalName)
		{
			PortalUserTypeOperations userTypeOperations = new PortalUserTypeOperations(portalName);
			ParameterMap paramInstance = new ParameterMap();
			APIResponse<ResponseHandler> response = userTypeOperations.GetUserTypes(paramInstance);
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
						List<Com.Zoho.Crm.API.PortalUserType.UserType> userType = responseWrapper.UserType;
						foreach (Com.Zoho.Crm.API.PortalUserType.UserType userType1 in userType)
						{
							Console.WriteLine ("UserType CreatedTime: " + userType1.CreatedTime);
							Console.WriteLine ("UserType Default: " + userType1.Default);
							Console.WriteLine ("UserType ModifiedTime: " + userType1.ModifiedTime);
							PersonalityModule personalityModule = userType1.PersonalityModule;
							if (personalityModule != null)
							{
								Console.WriteLine ("UserType PersonalityModule ID: " + personalityModule.Id);
								Console.WriteLine ("UserType PersonalityModule APIName: " + personalityModule.APIName);
								Console.WriteLine ("UserType PersonalityModule PluralLabel: " + personalityModule.PluralLabel);
							}
							Console.WriteLine ("UserType Name: " + userType1.Name);
							Owner modifiedBy = userType1.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("UserType ModifiedBy User-ID: " + modifiedBy.Id);
								Console.WriteLine ("UserType ModifiedBy User-Name: " + modifiedBy.Name);
							}
							Console.WriteLine ("UserType Active: " + userType1.Active);
							Console.WriteLine ("UserType Id: " + userType1.Id);
							Owner createdBy = userType1.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("UserType CreatedBy User-ID: " + createdBy.Id);
								Console.WriteLine ("UserType CreatedBy User-Name: " + createdBy.Name);
							}
							Console.WriteLine ("UserType NoOfUsers: " + userType1.NoOfUsers);
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
                GetUserTypes_1(portalName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}