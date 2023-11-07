using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Profiles.APIException;
using Category = Com.Zoho.Crm.API.Profiles.Category;
using CategoryModule = Com.Zoho.Crm.API.Profiles.CategoryModule;
using CategoryOthers = Com.Zoho.Crm.API.Profiles.CategoryOthers;
using DefaultView = Com.Zoho.Crm.API.Profiles.DefaultView;
using PermissionDetail = Com.Zoho.Crm.API.Profiles.PermissionDetail;
using ProfileWrapper = Com.Zoho.Crm.API.Profiles.ProfileWrapper;
using ProfilesOperations = Com.Zoho.Crm.API.Profiles.ProfilesOperations;
using ResponseHandler = Com.Zoho.Crm.API.Profiles.ResponseHandler;
using Section = Com.Zoho.Crm.API.Profiles.Section;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Profile
{
	public class GetProfile
	{
		public static void GetProfile_1(long profileId)
		{
			ProfilesOperations profilesOperations = new ProfilesOperations();
			APIResponse<ResponseHandler> response = profilesOperations.GetProfile(profileId);
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
							Console.WriteLine ("is custom profile?  " + profile.Custom);
							List<PermissionDetail> permissionsDetails = profile.PermissionsDetails;
							if (permissionsDetails != null)
							{
								foreach (PermissionDetail permissionsDetail in permissionsDetails)
								{
									Console.WriteLine ("Profile PermissionDetail DisplayLabel: " + permissionsDetail.DisplayLabel);
									Console.WriteLine ("Profile PermissionDetail Module: " + permissionsDetail.Module);
									Console.WriteLine ("Profile PermissionDetail Name: " + permissionsDetail.Name);
									Console.WriteLine ("Profile PermissionDetail ID: " + permissionsDetail.Id);
									Console.WriteLine ("Profile PermissionDetail Enabled: " + permissionsDetail.Enabled.Value);
                                    Console.WriteLine("Profile PermissionDetail ParentPermissions: " + JsonConvert.SerializeObject(permissionsDetail.ParentPermissions));

                                }
							}
							DefaultView defaultView = profile.Defaultview;
							if (defaultView != null)
							{
								Console.WriteLine ("Default view Name : " + defaultView.Name);
								Console.WriteLine ("Default view id : " + defaultView.Id);
								Console.WriteLine ("Default view type : " + defaultView.Type);
							}
							Console.WriteLine ("permission type   " + profile.PermissionType);
							Console.WriteLine ("Profile Name: " + profile.Name);
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
							List<Section> sections = profile.Sections;
							if (sections != null)
							{
								foreach (Section section in sections)
								{
									Console.WriteLine ("Profile Section Name: " + section.Name);
									List<Category> categories = section.Categories;
									foreach (Category category1 in categories)
									{
										if (category1 is CategoryOthers)
										{
											CategoryOthers category = (CategoryOthers) category1;
											Console.WriteLine ("Profile Section Category DisplayLabel: " + category.DisplayLabel);
											List<string> categoryPermissionsDetails = category.PermissionsDetails;
											if (categoryPermissionsDetails != null)
											{
												foreach (object permissionsDetailID in categoryPermissionsDetails)
												{
													Console.WriteLine ("Profile Section Category permissionsDetailID: " + permissionsDetailID);
												}
											}
											Console.WriteLine ("Profile Section Category Name: " + category.Name);
										}
										else if (category1 is CategoryModule)
										{
											CategoryModule category = (CategoryModule) category1;
											Console.WriteLine ("Profile Section Category DisplayLabel: " + category.DisplayLabel);
											List<string> categoryPermissionsDetails = category.PermissionsDetails;
											if (categoryPermissionsDetails != null)
											{
												foreach (object permissionsDetailID in categoryPermissionsDetails)
												{
													Console.WriteLine ("Profile Section Category permissionsDetailID: " + permissionsDetailID);
												}
											}
											Console.WriteLine ("Profile Section Category Module: " + category.Module);
											Console.WriteLine ("Profile Section Category Name: " + category.Name);
										}
									}
								}
							}
							if (profile.Delete != null)
							{
								Console.WriteLine ("Profile Delete: " + profile.Delete);
							}
							if (profile.Default != null)
							{
								Console.WriteLine ("Profile Default: " + profile.Default);
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
				long profileId = 440248031157L;
                GetProfile_1(profileId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}