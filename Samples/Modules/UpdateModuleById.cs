using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Modules.APIException;
using ActionHandler = Com.Zoho.Crm.API.Modules.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Modules.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Modules.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Modules.BodyWrapper;
using ModulesOperations = Com.Zoho.Crm.API.Modules.ModulesOperations;
using SuccessResponse = Com.Zoho.Crm.API.Modules.SuccessResponse;
using MinifiedProfile = Com.Zoho.Crm.API.Profiles.MinifiedProfile;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Modules
{
    public class UpdateModuleById
	{
		public static void UpdateModuleById_1(long moduleId)
		{
			ModulesOperations moduleOperations = new ModulesOperations();
			List<Com.Zoho.Crm.API.Modules.Modules> modules = new List<Com.Zoho.Crm.API.Modules.Modules>();
			List<MinifiedProfile> profiles = new List<MinifiedProfile>();
			MinifiedProfile profile = new MinifiedProfile();
			profile.Id = 3477061026014;
			profile.Delete = true;
			profiles.Add (profile);
            Com.Zoho.Crm.API.Modules.Modules module = new Com.Zoho.Crm.API.Modules.Modules();
			module.Profiles = profiles;
			module.APIName = "apiName1";
			modules.Add (module);
			BodyWrapper request = new BodyWrapper();
			request.Modules = modules;
			APIResponse<ActionHandler> response = moduleOperations.UpdateModule(moduleId, request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Modules;
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
						if (exception.Details != null)
						{
							foreach (KeyValuePair<string, object> entry in exception.Details)
							{
								Console.WriteLine (entry.Key + ": " + entry.Value);
							}
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
				long moduleId = 347706115237003;
                UpdateModuleById_1(moduleId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}