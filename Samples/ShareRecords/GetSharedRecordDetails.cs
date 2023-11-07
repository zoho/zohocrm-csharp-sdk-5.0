using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.ShareRecords.APIException;
using ResponseHandler = Com.Zoho.Crm.API.ShareRecords.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.ShareRecords.ResponseWrapper;
using ShareRecordsOperations = Com.Zoho.Crm.API.ShareRecords.ShareRecordsOperations;
using SharedThrough = Com.Zoho.Crm.API.ShareRecords.SharedThrough;
using GetSharedRecordDetailsParam = Com.Zoho.Crm.API.ShareRecords.ShareRecordsOperations.GetSharedRecordDetailsParam;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Sharerecords
{
    public class GetSharedRecordDetails
	{
		public static void GetSharedRecordDetails_1(string moduleAPIName, long recordId)
		{
			ShareRecordsOperations shareRecordsOperations = new ShareRecordsOperations(recordId, moduleAPIName);
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetSharedRecordDetailsParam.VIEW, "summary");
	//		paramInstance.Add (GetSharedRecordDetailsParam.SHAREDTO, 3477061173021l);
			APIResponse<ResponseHandler> response = shareRecordsOperations.GetSharedRecordDetails(paramInstance);
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
						List<Com.Zoho.Crm.API.ShareRecords.ShareRecord> shareRecords = responseWrapper.Share;
						foreach (Com.Zoho.Crm.API.ShareRecords.ShareRecord shareRecord in shareRecords)
						{
                            Com.Zoho.Crm.API.Users.Users sharedWith = shareRecord.SharedWith;
							if (sharedWith != null)
							{
								Console.WriteLine ("ShareRecord sharedWith Name: " + sharedWith.Name);
								Console.WriteLine ("ShareRecord sharedWith Id: " + sharedWith.Id);
								Console.WriteLine ("ShareRecord sharedWith Type: " + sharedWith.GetKeyValue("type"));
								Console.WriteLine ("ShareRecord sharedWith Zuid: " + sharedWith.Zuid);
							}
							Console.WriteLine ("ShareRecord ShareRelatedRecords: " + shareRecord.ShareRelatedRecords);
							SharedThrough sharedThrough = shareRecord.SharedThrough;
							if (sharedThrough != null)
							{
								Com.Zoho.Crm.API.ShareRecords.Module module =  sharedThrough.Module;
								if (module != null)
								{
									Console.WriteLine ("ShareRecord SharedThrough Module ID: " + module.Id);
									Console.WriteLine ("ShareRecord SharedThrough Module Name: " + module.Name);
								}
								Console.WriteLine ("ShareRecord SharedThrough Name: " + sharedThrough.Name);
								Console.WriteLine ("ShareRecord SharedThrough ID: " + sharedThrough.Id);
							}
							Console.WriteLine ("ShareRecord SharedTime: " + shareRecord.SharedTime);
							Console.WriteLine ("ShareRecord Permission: " + shareRecord.Permission.Value);
							Com.Zoho.Crm.API.Users.Users sharedBy =  shareRecord.SharedBy;
							if (sharedBy != null)
							{
								Console.WriteLine ("ShareRecord SharedBy-ID: " + sharedBy.Id);
								Console.WriteLine ("ShareRecord SharedBy-Name: " + sharedBy.Name);
								Console.WriteLine ("ShareRecord SharedBy-Zuid: " + sharedBy.Zuid);
							}
							Com.Zoho.Crm.API.Users.Users user =  shareRecord.User;
							if (user != null)
							{
								Console.WriteLine ("ShareRecord User-ID: " + user.Id);
								Console.WriteLine ("ShareRecord User-Name: " + user.Name);
								Console.WriteLine ("ShareRecord User-Zuid: " + user.Zuid);
							}
							Console.WriteLine ("ShareRecord Type: " + shareRecord.Type.Value);
						}
						if (responseWrapper.ShareableUser != null)
						{
							List<Com.Zoho.Crm.API.Users.Users> shareableUsers = responseWrapper.ShareableUser;
							foreach (Com.Zoho.Crm.API.Users.Users shareableUser in shareableUsers)
							{
								Console.WriteLine ("ShareRecord User-ID: " + shareableUser.Id);
								Console.WriteLine ("ShareRecord User-FullName: " + shareableUser.FullName);
								Console.WriteLine ("ShareRecord User-Zuid: " + shareableUser.Zuid);
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
						Console.WriteLine ("Message: " + exception.Message.Value);
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
				long recordId = 347706114963002;
				string moduleAPIName = "Leads";
                GetSharedRecordDetails_1(moduleAPIName, recordId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}