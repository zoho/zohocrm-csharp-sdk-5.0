using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using PortalUserTypeOperations = Com.Zoho.Crm.API.PortalUserType.PortalUserTypeOperations;
using APIException = Com.Zoho.Crm.API.PortalUserType.APIException;
using Filters = Com.Zoho.Crm.API.PortalUserType.Filters;
using Permissions = Com.Zoho.Crm.API.PortalUserType.Permissions;
using PersonalityModule = Com.Zoho.Crm.API.PortalUserType.PersonalityModule;
using ResponseHandler = Com.Zoho.Crm.API.PortalUserType.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.PortalUserType.ResponseWrapper;
using Views = Com.Zoho.Crm.API.PortalUserType.Views;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Portalusertype
{
    public class GetUserType
	{
		public static void GetUserType_1(string portalName, string userTypeId)
		{
			PortalUserTypeOperations userTypeOperations = new PortalUserTypeOperations(portalName);
			APIResponse<ResponseHandler> response = userTypeOperations.GetUserType(userTypeId);
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
							Console.WriteLine ("UserType Default: " + userType1.Default);
							PersonalityModule personalityModule = userType1.PersonalityModule;
							if (personalityModule != null)
							{
								Console.WriteLine ("UserType PersonalityModule ID: " + personalityModule.Id);
								Console.WriteLine ("UserType PersonalityModule APIName: " + personalityModule.APIName);
								Console.WriteLine ("UserType PersonalityModule PluralLabel: " + personalityModule.PluralLabel);
							}
							Console.WriteLine ("UserType Name: " + userType1.Name);
							Console.WriteLine ("UserType Active: " + userType1.Active);
							Console.WriteLine ("UserType Id: " + userType1.Id);
							Console.WriteLine ("UserType NoOfUsers: " + userType1.NoOfUsers);
							List<Com.Zoho.Crm.API.PortalUserType.Modules> modules = userType1.Modules;
							if (modules != null)
							{
								modules.ForEach(module =>
								{
									Console.WriteLine ("UserType Modules PluralLabel: " + module.PluralLabel);
									Console.WriteLine ("UserType Modules SharedType: " + module.SharedType.Value);
									Console.WriteLine ("UserType Modules APIName: " + module.APIName);
									Permissions permissions = module.Permissions;
									if (permissions != null)
									{
										Console.WriteLine ("UserType Modules Permissions View: " + permissions.View);
										Console.WriteLine ("UserType Modules Permissions Edit: " + permissions.Edit);
										Console.WriteLine ("UserType Modules Permissions EditSharedRecords: " + permissions.EditSharedRecords);
										Console.WriteLine ("UserType Modules Permissions Create: " + permissions.Create);
										Console.WriteLine ("UserType Modules Permissions Delete: " + permissions.Delete);
									}
									Console.WriteLine ("UserType Modules Id: " + module.Id);
									List<Filters> filters = module.Filters;
									if (filters != null)
									{
										filters.ForEach(filter =>
										{
											Console.WriteLine ("UserType Modules Filters APIName: " + filter.APIName);
											Console.WriteLine ("UserType Modules Filters DisplayLabel: " + filter.DisplayLabel);
											Console.WriteLine ("UserType Modules Filters Id: " + filter.Id);
										});
									}
									List<Com.Zoho.Crm.API.PortalUserType.Fields> fields = module.Fields;
									if (fields != null)
									{
										fields.ForEach(field =>
										{
											Console.WriteLine ("UserType Modules Fields APIName: " + field.APIName);
											Console.WriteLine ("UserType Modules Fields ReadOnly: " + field.ReadOnly);
											Console.WriteLine ("UserType Modules Fields Id: " + field.Id);
										});
									}
									List<Com.Zoho.Crm.API.PortalUserType.Layouts> layouts = module.Layouts;
									if (layouts != null)
									{
										layouts.ForEach(layout =>
										{
											Console.WriteLine ("UserType Modules Layouts Name: " + layout.Name);
											Console.WriteLine ("UserType Modules Layouts DisplayLabel: " + layout.DisplayLabel);
											Console.WriteLine ("UserType Modules Layouts Id: " + layout.Id);
										});
									}
									Views views = module.Views;
									if (views != null)
									{
										Console.WriteLine ("UserType Modules Views DisplayLabel: " + views.DisplayLabel);
										Console.WriteLine ("UserType Modules Views Name: " + views.Name);
										Console.WriteLine ("UserType Modules Views Id: " + views.Id);
										Console.WriteLine ("UserType Modules Permissions Type: " + views.Type);
									}
								});
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
				string userTypeId = "440248001304019";
                GetUserType_1(portalName, userTypeId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}