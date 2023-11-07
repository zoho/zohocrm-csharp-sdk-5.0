using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using HeaderMap = Com.Zoho.Crm.API.HeaderMap;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Users.APIException;
using CustomizeInfo = Com.Zoho.Crm.API.Users.CustomizeInfo;
using ResponseHandler = Com.Zoho.Crm.API.Users.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Users.ResponseWrapper;
using Tab = Com.Zoho.Crm.API.Users.Tab;
using Theme = Com.Zoho.Crm.API.Users.Theme;
using UsersOperations = Com.Zoho.Crm.API.Users.UsersOperations;
using GetUsersHeader = Com.Zoho.Crm.API.Users.UsersOperations.GetUsersHeader;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Users
{
    public class GetUser
	{
		public static void GetUser_1(long userId)
		{
			UsersOperations usersOperations = new UsersOperations();
			HeaderMap headerInstance = new HeaderMap();
			DateTimeOffset ifmodifiedsince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
			headerInstance.Add (GetUsersHeader.IF_MODIFIED_SINCE, ifmodifiedsince);
			APIResponse<ResponseHandler> response = usersOperations.GetUser(userId, headerInstance);
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
						List<Com.Zoho.Crm.API.Users.Users> users = responseWrapper.Users;
						foreach (Com.Zoho.Crm.API.Users.Users user in users)
						{
							Console.WriteLine ("User Country: " + user.Country);
							CustomizeInfo customizeInfo = user.CustomizeInfo;
							if (customizeInfo != null)
							{
								if (customizeInfo.NotesDesc != null)
								{
									Console.WriteLine ("User CustomizeInfo NotesDesc: " + customizeInfo.NotesDesc);
								}
								if (customizeInfo.ShowRightPanel != null)
								{
									Console.WriteLine ("User CustomizeInfo ShowRightPanel: " + customizeInfo.ShowRightPanel);
								}
								if (customizeInfo.BcView != null)
								{
									Console.WriteLine ("User CustomizeInfo BcView: " + customizeInfo.BcView);
								}
								if (customizeInfo.ShowHome != null)
								{
									Console.WriteLine ("User CustomizeInfo ShowHome: " + customizeInfo.ShowHome);
								}
								if (customizeInfo.ShowDetailView != null)
								{
									Console.WriteLine ("User CustomizeInfo ShowDetailView: " + customizeInfo.ShowDetailView);
								}
								if (customizeInfo.UnpinRecentItem != null)
								{
									Console.WriteLine ("User CustomizeInfo UnpinRecentItem: " + customizeInfo.UnpinRecentItem);
								}
							}
                            Com.Zoho.Crm.API.Users.Role role = user.Role;
							if (role != null)
							{
								Console.WriteLine ("User Role Name: " + role.Name);
								Console.WriteLine ("User Role ID: " + role.Id);
							}
							Console.WriteLine ("User Signature: " + user.Signature);
							Console.WriteLine ("User SortOrderPreference: " + user.SortOrderPreference);
							Console.WriteLine ("User City: " + user.City);
							Console.WriteLine ("User Isonline: " + user.Isonline);
							Console.WriteLine ("User SandboxDeveloper: " + user.Sandboxdeveloper);
							Console.WriteLine ("User Zip: " + user.Zip);
							Console.WriteLine ("User NameFormat: " + user.NameFormatS);
							Console.WriteLine ("User Language: " + user.Language);
							Console.WriteLine ("User Locale: " + user.Locale);
							Console.WriteLine ("User Microsoft: " + user.Microsoft);
							if (user.PersonalAccount != null)
							{
								Console.WriteLine ("User PersonalAccount: " + user.PersonalAccount);
							}
							Console.WriteLine ("User DefaultTabGroup: " + user.DefaultTabGroup);
							Console.WriteLine ("User Isonline: " + user.Isonline);
							Com.Zoho.Crm.API.Users.MinifiedUser modifiedBy =  user.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("User Modified By User-Name: " + modifiedBy.Name);
								Console.WriteLine ("User Modified By User-ID: " + modifiedBy.Id);
							}
							Console.WriteLine ("User Street: " + user.Street);
							Console.WriteLine ("User Currency: " + user.Currency);
							Console.WriteLine ("User Alias: " + user.Alias);
							Theme theme = user.Theme;
							if (theme != null)
							{
								Tab normalTab = theme.NormalTab;
								if (normalTab != null)
								{
									Console.WriteLine ("User Theme NormalTab FontColor: " + normalTab.FontColor);
									Console.WriteLine ("User Theme NormalTab Name: " + normalTab.Background);
								}
								Tab selectedTab = theme.SelectedTab;
								if (selectedTab != null)
								{
									Console.WriteLine ("User Theme SelectedTab FontColor: " + selectedTab.FontColor);
									Console.WriteLine ("User Theme SelectedTab Name: " + selectedTab.Background);
								}
								Console.WriteLine ("User Theme NewBackground: " + theme.NewBackground);
								Console.WriteLine ("User Theme Background: " + theme.Background);
								Console.WriteLine ("User Theme Screen: " + theme.Screen);
								Console.WriteLine ("User Theme Type: " + theme.Type);
							}
							Console.WriteLine ("User ID: " + user.Id);
							Console.WriteLine ("User State: " + user.State);
							Console.WriteLine ("User Fax: " + user.Fax);
							Console.WriteLine ("User CountryLocale: " + user.CountryLocale);
							Console.WriteLine ("User FirstName: " + user.FirstName);
							Console.WriteLine ("User Email: " + user.Email);
							Com.Zoho.Crm.API.Users.MinifiedUser reportingTo =  user.ReportingTo;
							if (reportingTo != null)
							{
								Console.WriteLine ("User ReportingTo Name: " + reportingTo.Name);
								Console.WriteLine ("User ReportingTo ID: " + reportingTo.Id);
							}
							Console.WriteLine ("User DecimalSeparator: " + user.DecimalSeparator);
							Console.WriteLine ("User Zip: " + user.Zip);
							Console.WriteLine ("User CreatedTime: " + user.CreatedTime);
							Console.WriteLine ("User Website: " + user.Website);
							Console.WriteLine ("User ModifiedTime: " + user.ModifiedTime);
							Console.WriteLine ("User TimeFormat: " + user.TimeFormat);
							Console.WriteLine ("User Offset: " + user.Offset);
                            Com.Zoho.Crm.API.Users.Profile profile = user.Profile;
							if (profile != null)
							{
								Console.WriteLine ("User Profile Name: " + profile.Name);
								Console.WriteLine ("User Profile ID: " + profile.Id);
							}
							Console.WriteLine ("User Mobile: " + user.Mobile);
							Console.WriteLine ("User LastName: " + user.LastName);
							Console.WriteLine ("User TimeZone: " + user.TimeZone.Id);
							Com.Zoho.Crm.API.Users.MinifiedUser createdBy =  user.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("User Created By User-Name: " + createdBy.Name);
								Console.WriteLine ("User Created By User-ID: " + createdBy.Id);
							}
							Console.WriteLine ("User Zuid: " + user.Zuid);
							Console.WriteLine ("User Confirm: " + user.Confirm);
							Console.WriteLine ("User FullName: " + user.FullName);
							Console.WriteLine ("User Phone: " + user.Phone);
							Console.WriteLine ("User DOB: " + user.Dob);
							Console.WriteLine ("User DateFormat: " + user.DateFormat);
							Console.WriteLine ("User Status: " + user.Status);
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
				long userId = 440248254001;
                GetUser_1(userId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}