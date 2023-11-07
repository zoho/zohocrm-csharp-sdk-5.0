using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using HeaderMap = Com.Zoho.Crm.API.HeaderMap;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Modules.APIException;
using Argument = Com.Zoho.Crm.API.Modules.Argument;
using MinifiedModule = Com.Zoho.Crm.API.Modules.MinifiedModule;
using ModulesOperations = Com.Zoho.Crm.API.Modules.ModulesOperations;
using ResponseHandler = Com.Zoho.Crm.API.Modules.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Modules.ResponseWrapper;
using GetModulesHeader = Com.Zoho.Crm.API.Modules.ModulesOperations.GetModulesHeader;
using MinifiedProfile = Com.Zoho.Crm.API.Profiles.MinifiedProfile;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Modules
{
    public class GetModules
	{
		public static void GetModules_1()
		{
			ModulesOperations moduleOperations = new ModulesOperations();
			HeaderMap headerInstance = new HeaderMap();
			DateTimeOffset ifmodifiedsince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
			headerInstance.Add (GetModulesHeader.IF_MODIFIED_SINCE, ifmodifiedsince);
			APIResponse<ResponseHandler> response = moduleOperations.GetModules(headerInstance);
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
						List<Com.Zoho.Crm.API.Modules.Modules> modules = responseWrapper.Modules;
						foreach (Com.Zoho.Crm.API.Modules.Modules module in modules)
						{
							Console.WriteLine ("Module GlobalSearchSupported: " + module.GlobalSearchSupported);
							Console.WriteLine ("Module Deletable: " + module.Deletable);
							Console.WriteLine ("Module Description: " + module.Description);
							Console.WriteLine ("Module Creatable: " + module.Creatable);
							Console.WriteLine ("Module InventoryTemplateSupported: " + module.InventoryTemplateSupported);
							if (module.ModifiedTime != null)
							{
								Console.WriteLine ("Module ModifiedTime: " + module.ModifiedTime);
							}
							Console.WriteLine ("Module PluralLabel: " + module.PluralLabel);
							Console.WriteLine ("Module PresenceSubMenu: " + module.PresenceSubMenu);
							Console.WriteLine ("Module TriggersSupported: " + module.TriggersSupported);
							Console.WriteLine ("Module Id: " + module.Id);
							Console.WriteLine ("Module IsBlueprintSupported: " + module.Isblueprintsupported);
							Console.WriteLine ("Module visible: " + module.Visible);
							Console.WriteLine ("Module Visibility: " + module.Visibility);
							Console.WriteLine ("Module Convertable: " + module.Convertable);
							Console.WriteLine ("Module Editable: " + module.Editable);
							Console.WriteLine ("Module EmailtemplateSupport: " + module.EmailtemplateSupport);
							List<MinifiedProfile> profiles = module.Profiles;
							if (profiles != null)
							{
								foreach (MinifiedProfile profile in profiles)
								{
									Console.WriteLine ("Module Profile Name: " + profile.Name);
									Console.WriteLine ("Module Profile Id: " + profile.Id);
								}
							}
							Console.WriteLine ("Module FilterSupported: " + module.FilterSupported);
							Console.WriteLine ("Module ShowAsTab: " + module.ShowAsTab);
							Console.WriteLine ("Module WebLink: " + module.WebLink);
							Console.WriteLine ("Module SequenceNumber: " + module.SequenceNumber);
							Console.WriteLine ("Module SingularLabel: " + module.SingularLabel);
							Console.WriteLine ("Module Viewable: " + module.Viewable);
							Console.WriteLine ("Module APISupported: " + module.APISupported);
							Console.WriteLine ("Module APIName: " + module.APIName);
							Console.WriteLine ("Module QuickCreate: " + module.QuickCreate);
							Com.Zoho.Crm.API.Users.MinifiedUser modifiedBy =  module.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("Module Modified By User-Name: " + modifiedBy.Name);
								Console.WriteLine ("Module Modified By User-ID: " + modifiedBy.Id);
							}
							Console.WriteLine ("Module GeneratedType: " + module.GeneratedType.Value);
							Console.WriteLine ("Module FeedsRequired: " + module.FeedsRequired);
							Console.WriteLine ("Module ScoringSupported: " + module.ScoringSupported);
							Console.WriteLine ("Module WebformSupported: " + module.WebformSupported);
							List<Argument> arguments = module.Arguments;
							if (arguments != null)
							{
								foreach (Argument argument in arguments)
								{
									Console.WriteLine ("Module Argument Name: " + argument.Name);
									Console.WriteLine ("Module Argument Value: " + argument.Value);
								}
							}
							Console.WriteLine ("Module ModuleName: " + module.ModuleName);
							Console.WriteLine ("Module BusinessCardFieldLimit: " + module.BusinessCardFieldLimit);
							MinifiedModule parentModule = module.ParentModule;
							if (parentModule != null)
							{
								Console.WriteLine ("Module Parent Module Name: " + parentModule.APIName);
								Console.WriteLine ("Module Parent Module Id: " + parentModule.Id);
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
                GetModules_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}