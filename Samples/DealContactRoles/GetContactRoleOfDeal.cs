using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.DealContactRoles.APIException;
using BodyWrapper = Com.Zoho.Crm.API.DealContactRoles.BodyWrapper;
using Data = Com.Zoho.Crm.API.DealContactRoles.Data;
using DealContactRolesOperations = Com.Zoho.Crm.API.DealContactRoles.DealContactRolesOperations;
using Info = Com.Zoho.Crm.API.DealContactRoles.Info;
using ResponseHandler = Com.Zoho.Crm.API.DealContactRoles.ResponseHandler;
using MinifiedUser = Com.Zoho.Crm.API.Users.MinifiedUser;
using Com.Zoho.Crm.API.Util;
using ContactRole = Com.Zoho.Crm.API.DealContactRoles.ContactRole;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using System.Collections;

namespace Samples.Dealcontactroles
{
    public class GetContactRoleOfDeal
	{
		public static void GetContactRoleOfDeal_1(long contactId, long dealId)
		{
			DealContactRolesOperations contactRolesOperations = new DealContactRolesOperations();
			APIResponse<ResponseHandler> response = contactRolesOperations.GetAssociatedContactRolesSpecificToContact(contactId, dealId);
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
					if (responseHandler is BodyWrapper)
					{
						BodyWrapper responseWrapper = (BodyWrapper) responseHandler;
						List<Data> records = responseWrapper.Data;
						foreach (Data record in records)
						{
							Console.WriteLine ("Record ID: " + record.Id);
							MinifiedUser createdBy = record.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("Record Created By User-ID: " + createdBy.Id);
								Console.WriteLine ("Record Created By User-Name: " + createdBy.Name);
								Console.WriteLine ("Record Created By User-Email: " + createdBy.Email);
							}
							Console.WriteLine ("Record CreatedTime: " + record.CreatedTime);
							MinifiedUser modifiedBy = record.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("Record Modified By User-ID: " + modifiedBy.Id);
								Console.WriteLine ("Record Modified By User-Name: " + modifiedBy.Name);
								Console.WriteLine ("Record Modified By User-Email: " + modifiedBy.Email);
							}
							Console.WriteLine ("Record CreatedTime: " + record.ModifiedTime);
							// To get particular field value
							Console.WriteLine ("Record Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName
							Console.WriteLine ("Record KeyValues:");
							foreach (KeyValuePair<string, object> entry in record.GetKeyValues())
							{
								string keyName = entry.Key;
								object value = entry.Value;
                                if (value is IList)
                                {
                                    Console.WriteLine("Record KeyName : " + keyName);
                                    IList dataList = (IList)value;
                                    foreach (object data in dataList)
                                    {
                                        if (data is IDictionary)
                                        {
											Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
											foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) data))
											{
												Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
											}
										}
										else
										{
											Console.WriteLine (data);
										}
									}
								}
								else if (value is ContactRole)
								{
									ContactRole contactrole = (ContactRole) value;
									if (contactrole != null)
									{
										Console.WriteLine ("Record ContactRole Name : " + contactrole.Name);
										Console.WriteLine ("Record ContactRole Id : " + contactrole.Id);
									}
								}
								else if (value is IDictionary)
								{
									Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
									foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) value))
									{
										Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
									}
								}
								else
								{
									Console.WriteLine ("Record KeyName : " + keyName + " - Value : " + value);
								}
							}
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.Count != null)
							{
								Console.WriteLine ("Record Info Count: " + info.Count);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("Record Info MoreRecords: " + info.MoreRecords);
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
				long dealId = 440248001177050l;
				long contactId = 440248001030088l;
                GetContactRoleOfDeal_1(contactId, dealId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}