using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Profiles.APIException;
using DefaultView = Com.Zoho.Crm.API.Profiles.DefaultView;
using ProfileWrapper = Com.Zoho.Crm.API.Profiles.ProfileWrapper;
using ProfilesOperations = Com.Zoho.Crm.API.Profiles.ProfilesOperations;
using ResponseHandler = Com.Zoho.Crm.API.Profiles.ResponseHandler;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Profile
{
	public class GetProfiles
	{
		public static void GetProfiles_1()
		{
			ProfilesOperations profilesOperations = new ProfilesOperations();
			ParameterMap paramInstance = new ParameterMap();
			APIResponse<ResponseHandler> response = profilesOperations.GetProfiles(paramInstance);
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
					if (responseHandler is ProfileWrapper)
					{
						ProfileWrapper responseWrapper = (ProfileWrapper) responseHandler;
						List<Com.Zoho.Crm.API.Profiles.Profile> profiles = responseWrapper.Profiles;
						foreach (Com.Zoho.Crm.API.Profiles.Profile profile in profiles)
						{
							Console.WriteLine ("Profile DisplayLabel: " + profile.DisplayLabel);
							if (profile.CreatedTime != null)
							{
								Console.WriteLine ("Profile CreatedTime: " + profile.CreatedTime);
							}
							if (profile.ModifiedTime != null)
							{
								Console.WriteLine ("Profile ModifiedTime: " + profile.ModifiedTime);
							}
							DefaultView defaultView = profile.Defaultview;
							if (defaultView != null)
							{
								Console.WriteLine ("Default view Name : " + defaultView.Name);
								Console.WriteLine ("Default view id : " + defaultView.Id);
								Console.WriteLine ("Default view type : " + defaultView.Type);
							}
							Console.WriteLine ("Profile Name: " + profile.Name);
							Console.WriteLine ("is custom profile?  " + profile.Custom);
							Console.WriteLine ("is deleted profile?  " + profile.Delete);
							Console.WriteLine ("permission type   " + profile.PermissionType);
							Com.Zoho.Crm.API.Users.MinifiedUser modifiedBy =  profile.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("Profile Modified By User-ID: " + modifiedBy.Id);
								Console.WriteLine ("Profile Modified By User-Name: " + modifiedBy.Name);
								Console.WriteLine ("Profile Modified By User-Email: " + modifiedBy.Email);
							}
							Console.WriteLine ("Profile Description: " + profile.Description);
							Console.WriteLine ("Profile ID: " + profile.Id);
							Com.Zoho.Crm.API.Users.MinifiedUser createdBy =  profile.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("Profile Created By User-ID: " + createdBy.Id);
								Console.WriteLine ("Profile Created By User-Name: " + createdBy.Name);
								Console.WriteLine ("Profile Created By User-Email: " + createdBy.Email);
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
                GetProfiles_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}