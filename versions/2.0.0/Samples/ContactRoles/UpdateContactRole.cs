using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.ContactRoles.APIException;
using ActionHandler = Com.Zoho.Crm.API.ContactRoles.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.ContactRoles.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.ContactRoles.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.ContactRoles.BodyWrapper;
using ContactRole = Com.Zoho.Crm.API.ContactRoles.ContactRole;
using ContactRolesOperations = Com.Zoho.Crm.API.ContactRoles.ContactRolesOperations;
using SuccessResponse = Com.Zoho.Crm.API.ContactRoles.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Contactroles
{
	public class UpdateContactRole
	{
		public static void UpdateContactRole_1(long contactRoleId)
		{
			ContactRolesOperations contactRolesOperations = new ContactRolesOperations();
			BodyWrapper bodyWrapper = new BodyWrapper();
			List<ContactRole> contactRolesList = new List<ContactRole>();
			ContactRole cr1 = new ContactRole();
			cr1.Name = "ContactRole";
			cr1.SequenceNumber = 2;
			contactRolesList.Add (cr1);
			bodyWrapper.ContactRoles = contactRolesList;
			APIResponse<ActionHandler> response = contactRolesOperations.UpdateRole(contactRoleId, bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.ContactRoles;
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
				long contactRoleId = 5255085067923l;
                UpdateContactRole_1(contactRoleId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}