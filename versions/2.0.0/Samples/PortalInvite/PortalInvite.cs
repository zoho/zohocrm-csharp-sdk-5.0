using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.PortalInvite.APIException;
using ActionHandler = Com.Zoho.Crm.API.PortalInvite.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.PortalInvite.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.PortalInvite.ActionWrapper;
using PortalInviteOperations = Com.Zoho.Crm.API.PortalInvite.PortalInviteOperations;
using InviteUsersParam = Com.Zoho.Crm.API.PortalInvite.PortalInviteOperations.InviteUsersParam;
using SuccessResponse = Com.Zoho.Crm.API.PortalInvite.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Portalinvite
{
	public class PortalInvite
	{
		public static void PortalInvite_1(long record, string module, long userTypeId, string type1)
		{
			PortalInviteOperations portalinviteoperations = new PortalInviteOperations(module);
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (InviteUsersParam.USER_TYPE_ID, userTypeId);
			paramInstance.Add (InviteUsersParam.TYPE, type1);
			paramInstance.Add (InviteUsersParam.LANGUAGE, "en_US");
			APIResponse<ActionHandler> response = portalinviteoperations.InviteUsers(record, paramInstance);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.PortalInvite;
						foreach (ActionResponse actionResponse in actionResponses)
						{
							if (actionResponse is SuccessResponse)
							{
								SuccessResponse successresponse = (SuccessResponse) actionResponse;
								Console.WriteLine ("Status: " + successresponse.Status.Value);
								Console.WriteLine ("Code: " + successresponse.Code.Value);
								Console.WriteLine ("Details: ");
								foreach (KeyValuePair<string, object> entry in successresponse.Details)
								{
									Console.WriteLine (entry.Key + ": " + entry.Value);
								}
								Console.WriteLine ("Message: " + successresponse.Message);
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
				long record = 440248001307008l;
				string module = "leads";
				long userTypeId = 440248001304019l;
				string type = "invite";
                PortalInvite_1(record, module, userTypeId, type);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}