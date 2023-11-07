using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Users.APIException;
using AssociatedGroup = Com.Zoho.Crm.API.Users.AssociatedGroup;
using AssociatedGroupsWrapper = Com.Zoho.Crm.API.Users.AssociatedGroupsWrapper;
using MinifiedUser = Com.Zoho.Crm.API.Users.MinifiedUser;
using ResponseHandler = Com.Zoho.Crm.API.Users.ResponseHandler;
using UsersOperations = Com.Zoho.Crm.API.Users.UsersOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Users
{
	public class GetAssociatedGroups
	{
		public static void GetAssociatedGroups_1(long userId)
		{
			UsersOperations usersOperations = new UsersOperations();
			APIResponse<ResponseHandler> response = usersOperations.GetAssociatedGroups(userId);
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
					if (responseHandler is AssociatedGroupsWrapper)
					{
						AssociatedGroupsWrapper associatedGroupsWrapper = (AssociatedGroupsWrapper) responseHandler;
						List<AssociatedGroup> userGroups = associatedGroupsWrapper.UserGroups;
						if (userGroups != null)
						{
							foreach (AssociatedGroup userGroup in userGroups)
							{
								Console.WriteLine ("AssociateGroup ID : " + userGroup.Id);
								Console.WriteLine ("AssociateGroup Name : " + userGroup.Name);
								Console.WriteLine ("AssociateGroup Description : " + userGroup.Description);
								Console.WriteLine ("AssociateGroup CreatedTime : " + userGroup.CreatedTime);
								Console.WriteLine ("AssociateGroup ModifiedTime : " + userGroup.ModifiedTime);
								MinifiedUser createdBy = userGroup.CreatedBy;
								if (createdBy != null)
								{
									Console.WriteLine ("AssociateGroup CreatedBy ID : " + createdBy.Id);
									Console.WriteLine ("AssociateGroup CreatedBy Name : " + createdBy.Name);
									Console.WriteLine ("AssociateGroup CreatedBy Email : " + createdBy.Email);
								}
								MinifiedUser modifiedBy = userGroup.ModifiedBy;
								if (modifiedBy != null)
								{
									Console.WriteLine ("AssociateGroup modifiedBy ID : " + modifiedBy.Id);
									Console.WriteLine ("AssociateGroup modifiedBy Name : " + modifiedBy.Name);
									Console.WriteLine ("AssociateGroup modifiedBy Email : " + modifiedBy.Email);
								}
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
				long userId = 4402480254001;
                GetAssociatedGroups_1(userId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}