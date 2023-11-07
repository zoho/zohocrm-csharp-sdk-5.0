using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Roles.APIException;
using ActionHandler = Com.Zoho.Crm.API.Roles.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Roles.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Roles.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Roles.BodyWrapper;
using ReportingTo = Com.Zoho.Crm.API.Roles.ReportingTo;
using RolesOperations = Com.Zoho.Crm.API.Roles.RolesOperations;
using SuccessResponse = Com.Zoho.Crm.API.Roles.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Role
{
	public class UpdateRole
	{
		public static void UpdateRole_1(long roleId)
		{
			RolesOperations rolesOperations = new RolesOperations();
			BodyWrapper bodyWrapper = new BodyWrapper();
			List<Com.Zoho.Crm.API.Roles.Role> roles = new List<Com.Zoho.Crm.API.Roles.Role>();
			Com.Zoho.Crm.API.Roles.Role role =  new Com.Zoho.Crm.API.Roles.Role();
			role.Name = "Product Manager3421";
			ReportingTo reportingTo = new ReportingTo();
			reportingTo.Id = 3477061026008l;
			role.ReportingTo = reportingTo;
			role.Description = "Schedule and manage resources";
			role.ShareWithPeers = true;
			ReportingTo forecastManager = new ReportingTo();
	//		forecastManager.Email = "abc@zoho.com";
			forecastManager.Id = 738964291009L;
	//		forecastManager.Name = "name";
			role.ForecastManager = forecastManager;
			roles.Add (role);
			bodyWrapper.Roles = roles;
			APIResponse<ActionHandler> response = rolesOperations.UpdateRole(roleId, bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Roles;
						foreach (ActionResponse actionResponse in actionResponses)
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
				long roleId = 3477061003881;
                UpdateRole_1(roleId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}