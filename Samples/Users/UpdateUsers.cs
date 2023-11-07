using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Users.APIException;
using ActionHandler = Com.Zoho.Crm.API.Users.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Users.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Users.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Users.BodyWrapper;
using Role = Com.Zoho.Crm.API.Users.Role;
using SuccessResponse = Com.Zoho.Crm.API.Users.SuccessResponse;
using UsersOperations = Com.Zoho.Crm.API.Users.UsersOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Users
{
	public class UpdateUsers
	{
		public static void UpdateUsers_1()
		{
			UsersOperations usersOperations = new UsersOperations();
			BodyWrapper request = new BodyWrapper();
			List<Com.Zoho.Crm.API.Users.Users> userList = new List<Com.Zoho.Crm.API.Users.Users>();
			Com.Zoho.Crm.API.Users.Users user1 =  new Com.Zoho.Crm.API.Users.Users();
			user1.Id = 347525001;
            Com.Zoho.Crm.API.Users.Role role = new Com.Zoho.Crm.API.Users.Role();
			role.Id = 347704903002;
			user1.Role = role;
			user1.CountryLocale = "en_US";
			userList.Add (user1);
			user1 =  new Com.Zoho.Crm.API.Users.Users();
			user1.Id = 347706116697013;
			role = new Com.Zoho.Crm.API.Users.Role();
			role.Id = 3477014903002;
			user1.Role = role;
			user1.CountryLocale = "en_US";
	//		user1.AddKeyValue(apiName, value);
			userList.Add (user1);
			request.Users = userList;
			APIResponse<ActionHandler> response = usersOperations.UpdateUsers(request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper responseWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = responseWrapper.Users;
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
                UpdateUsers_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}