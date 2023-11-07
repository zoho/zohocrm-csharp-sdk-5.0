using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Roles.APIException;
using ReportingTo = Com.Zoho.Crm.API.Roles.ReportingTo;
using ResponseHandler = Com.Zoho.Crm.API.Roles.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Roles.ResponseWrapper;
using RolesOperations = Com.Zoho.Crm.API.Roles.RolesOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Role
{
	public class GetRole
	{
		public static void GetRole_1(long roleId)
		{
			RolesOperations rolesOperations = new RolesOperations();
			APIResponse<ResponseHandler> response = rolesOperations.GetRole(roleId);
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
						List<Com.Zoho.Crm.API.Roles.Role> roles = responseWrapper.Roles;
						foreach (Com.Zoho.Crm.API.Roles.Role role in roles)
						{
							Console.WriteLine ("Role DisplayLabel: " + role.DisplayLabel);
							ReportingTo forecastManager = role.ForecastManager;
							if (forecastManager != null)
							{
								Console.WriteLine ("Role Forecast Manager User-ID: " + forecastManager.Id);
								Console.WriteLine ("Role Forecast Manager User-Name: " + forecastManager.Name);
							}
							Console.WriteLine ("Role ShareWithPeers: " + role.ShareWithPeers);
							Console.WriteLine ("Role Name: " + role.Name);
							Console.WriteLine ("Role Description: " + role.Description);
							Console.WriteLine ("Role ID: " + role.Id);
							ReportingTo reportingTo = role.ReportingTo;
							if (reportingTo != null)
							{
								Console.WriteLine ("Role ReportingTo User-ID: " + reportingTo.Id);
								Console.WriteLine ("Role ReportingTo User-Name: " + reportingTo.Name);
							}
							Console.WriteLine ("Role AdminUser: " + role.AdminUser);
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
				long roleId = 4402480031151;
                GetRole_1(roleId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}