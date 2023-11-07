using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using CustomView = Com.Zoho.Crm.API.CustomViews.CustomView;
using Owner = Com.Zoho.Crm.API.CustomViews.Owner;
using SharedTo = Com.Zoho.Crm.API.CustomViews.SharedTo;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Modules.APIException;
using Argument = Com.Zoho.Crm.API.Modules.Argument;
using Criteria = Com.Zoho.Crm.API.CustomViews.Criteria;
using MinifiedModule = Com.Zoho.Crm.API.Modules.MinifiedModule;
using ModulesOperations = Com.Zoho.Crm.API.Modules.ModulesOperations;
using RelatedListProperties = Com.Zoho.Crm.API.Modules.RelatedListProperties;
using ResponseHandler = Com.Zoho.Crm.API.Modules.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Modules.ResponseWrapper;
using Territory = Com.Zoho.Crm.API.Modules.Territory;
using MinifiedProfile = Com.Zoho.Crm.API.Profiles.MinifiedProfile;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Modules
{
    public class GetModuleByAPIName
	{
		public static void GetModuleByAPIName_1(string moduleAPIName)
		{
			ModulesOperations moduleOperations = new ModulesOperations();
			APIResponse<ResponseHandler> response = moduleOperations.GetModuleByAPIName(moduleAPIName);
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
							if (module.KanbanView != null)
							{
								Console.WriteLine ("Module KanbanView: " + module.KanbanView);
							}
							Console.WriteLine ("Module Deletable: " + module.Deletable);
							Console.WriteLine ("Module Description: " + module.Description);
							Console.WriteLine ("Module Creatable: " + module.Creatable);
							if (module.FilterStatus != null)
							{
								Console.WriteLine ("Module FilterStatus: " + module.FilterStatus);
							}
							Console.WriteLine ("Module InventoryTemplateSupported: " + module.InventoryTemplateSupported);
							if (module.ModifiedTime != null)
							{
								Console.WriteLine ("Module ModifiedTime: " + module.ModifiedTime);
							}
							Console.WriteLine ("Module PluralLabel: " + module.PluralLabel);
							Console.WriteLine ("Module PresenceSubMenu: " + module.PresenceSubMenu);
							Console.WriteLine ("Module TriggersSupported: " + module.TriggersSupported);
							Console.WriteLine ("Module Id: " + module.Id);
							Console.WriteLine ("Module IsBlueprintSupported : " + module.Isblueprintsupported);
							RelatedListProperties relatedListProperties = module.RelatedListProperties;
							if (relatedListProperties != null)
							{
								Console.WriteLine ("Module RelatedListProperties SortBy: " + relatedListProperties.SortBy);
								List<string> fields = relatedListProperties.Fields;
								if (fields != null)
								{
									foreach (object fieldName in fields)
									{
										Console.WriteLine ("Module RelatedListProperties Fields: " + fieldName);
									}
								}
								Console.WriteLine ("Module RelatedListProperties SortOrder: " + relatedListProperties.SortOrder);
							}
							Console.WriteLine ("Module PerPage: " + module.PerPage);
							List<string> properties = module.Properties;
							if (properties != null)
							{
								foreach (object fieldName in properties)
								{
									Console.WriteLine ("Module Properties Fields: " + fieldName);
								}
							}
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
							List<string> onDemandProperties = module.OnDemandProperties;
							if (onDemandProperties != null)
							{
								foreach (object fieldName in onDemandProperties)
								{
									Console.WriteLine ("Module onDemandProperties Fields: " + fieldName);
								}
							}
							Console.WriteLine ("Module DisplayField: " + module.DisplayField);
							List<string> searchLayoutFields = module.SearchLayoutFields;
							if (searchLayoutFields != null)
							{
								foreach (object fieldName in searchLayoutFields)
								{
									Console.WriteLine ("Module SearchLayoutFields Fields: " + fieldName);
								}
							}
							if (module.KanbanViewSupported != null)
							{
								Console.WriteLine ("Module KanbanViewSupported: " + module.KanbanViewSupported);
							}
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
							CustomView customView = module.CustomView;
							if (customView != null)
							{
								printCustomView(customView);
							}
							MinifiedModule parentModule = module.ParentModule;
							if (parentModule != null)
							{
								Console.WriteLine ("Module Parent Module Name: " + parentModule.APIName);
								Console.WriteLine ("Module Parent Module Id: " + parentModule.Id);
							}
							Territory territory = module.Territory;
							if (territory != null)
							{
								Console.WriteLine ("Module Territory Name: " + territory.Name);
								Console.WriteLine ("Module Territory Id: " + territory.Id);
								Console.WriteLine ("Module Territory Subordinates: " + territory.Subordinates);
							}
						}
					}
					else if (responseHandler is APIException)
					{
						APIException exception = (APIException) responseHandler;
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
		private static void printCustomView(CustomView customView)
		{
			Console.WriteLine ("Module CustomView DisplayValue: " + customView.DisplayValue);
			if (customView.CreatedTime != null)
			{
				Console.WriteLine ("Module CustomView CreatedTime: " + customView.CreatedTime);
			}
			Console.WriteLine ("Module CustomView AccessType: " + customView.AccessType);
			Criteria criteria = customView.Criteria;
			if (criteria != null)
			{
				printCriteria(criteria);
			}
			Console.WriteLine ("Module CustomView SystemName: " + customView.SystemName);
			Console.WriteLine ("Module CustomView SortBy: " + customView.SortBy);
			Owner createdBy = customView.CreatedBy;
			if (createdBy != null)
			{
				Console.WriteLine ("Module Created By User-Name: " + createdBy.Name);
				Console.WriteLine ("Module Created By User-ID: " + createdBy.Id);
			}
			List<SharedTo> sharedToDetails = customView.SharedTo;
			if (sharedToDetails != null)
			{
				foreach (SharedTo sharedTo in sharedToDetails)
				{
					Console.WriteLine ("SharedDetails Name: " + sharedTo.Name);
					Console.WriteLine ("SharedDetails ID: " + sharedTo.Id);
					Console.WriteLine ("SharedDetails Type: " + sharedTo.Type);
					Console.WriteLine ("SharedDetails Subordinates: " + sharedTo.Subordinates);
				}
			}
			Console.WriteLine ("Module CustomView Default: " + customView.Default);
			if (customView.ModifiedTime != null)
			{
				Console.WriteLine ("Module CustomView ModifiedTime: " + customView.ModifiedTime);
			}
			Console.WriteLine ("Module CustomView Name: " + customView.Name);
			Console.WriteLine ("Module CustomView SystemDefined: " + customView.SystemDefined);
			Owner modifiedBy = customView.ModifiedBy;
			if (modifiedBy != null)
			{
				Console.WriteLine ("Module Modified By User-Name: " + modifiedBy.Name);
				Console.WriteLine ("Module Modified By User-ID: " + modifiedBy.Id);
			}
			Console.WriteLine ("Module CustomView ID: " + customView.Id);
			List<Com.Zoho.Crm.API.CustomViews.Fields> fields = customView.Fields;
			if (fields != null)
			{
				foreach (Com.Zoho.Crm.API.CustomViews.Fields field in fields)
				{
					Console.WriteLine ("Module CustomView Field Id: " + field.Id);
					Console.WriteLine ("Module CustomView Field APIName: " + field.APIName);
				}
			}
			Console.WriteLine ("Module CustomView Category: " + customView.Category);
			if (customView.LastAccessedTime != null)
			{
				Console.WriteLine ("Module CustomView LastAccessedTime: " + customView.LastAccessedTime);
			}
			if (customView.Favorite != null)
			{
				Console.WriteLine ("Module CustomView Favorite: " + customView.Favorite);
			}
			if (customView.SortOrder != null)
			{
				Console.WriteLine ("Module CustomView SortOrder: " + customView.SortOrder);
			}
		}
		private static void printCriteria(Criteria criteria)
		{
			if (criteria.Comparator != null)
			{
				Console.WriteLine ("CustomView Criteria Comparator: " + criteria.Comparator);
			}
			if (criteria.Field != null)
			{
				Console.WriteLine ("CustomView Criteria field name: " + criteria.Field.APIName);
			}
			if (criteria.Value != null)
			{
				Console.WriteLine ("CustomView Criteria Value: " + criteria.Value);
			}
			List<Criteria> criteriaGroup = criteria.Group;
			if (criteriaGroup != null)
			{
				foreach (Criteria criteria1 in criteriaGroup)
				{
					printCriteria(criteria1);
				}
			}
			if (criteria.GroupOperator != null)
			{
				Console.WriteLine ("CustomView Criteria Group Operator: " + criteria.GroupOperator);
			}
		}
		public static void Call()
		{
			try
			{
				Environment environment = USDataCenter.PRODUCTION;
				IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL" ).Build();
				new Initializer.Builder().Environment(environment).Token(token).Initialize();
				string moduleAPIName = "Leads";
                GetModuleByAPIName_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}