using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Profiles.APIException;
using ActionHandler = Com.Zoho.Crm.API.Profiles.ActionHandler;
using ProfilesOperations = Com.Zoho.Crm.API.Profiles.ProfilesOperations;
using DeleteProfileParam = Com.Zoho.Crm.API.Profiles.ProfilesOperations.DeleteProfileParam;
using SuccessResponse = Com.Zoho.Crm.API.Profiles.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Profile
{
	public class DeleteProfile
	{
		public static void DeleteProfile_1(long profileId, long existingprofileid)
		{
			ProfilesOperations profilesOperations = new ProfilesOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (DeleteProfileParam.TRANSFER_TO, existingprofileid);
			APIResponse<ActionHandler> response = profilesOperations.DeleteProfile(profileId, paramInstance);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is SuccessResponse)
					{
						SuccessResponse successResponse = (SuccessResponse) actionHandler;
						Console.WriteLine ("Status: " + successResponse.Status.Value);
						Console.WriteLine ("Code: " + successResponse.Code.Value);
						Console.WriteLine ("Details: ");
						foreach (KeyValuePair<string, object> entry in successResponse.Details)
						{
							Console.WriteLine (entry.Key + ": " + entry.Value);
						}
						Console.WriteLine ("Message: " + successResponse.Message);
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
				long profileId = 3477061026011l;
				long existingprofileid = 347706116715008l;
                DeleteProfile_1(profileId, existingprofileid);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}