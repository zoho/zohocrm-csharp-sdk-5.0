using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.RecordLocking.APIException;
using Info = Com.Zoho.Crm.API.RecordLocking.Info;
using LockedForS = Com.Zoho.Crm.API.RecordLocking.LockedForS;
using RecordLockingOperations = Com.Zoho.Crm.API.RecordLocking.RecordLockingOperations;
using ResponseHandler = Com.Zoho.Crm.API.RecordLocking.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.RecordLocking.ResponseWrapper;
using getrecordlockinformationParam = Com.Zoho.Crm.API.RecordLocking.RecordLockingOperations.getrecordlockinformationParam;
using MinifiedUser = Com.Zoho.Crm.API.Users.MinifiedUser;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using System.Collections;

namespace Samples.Recordlocking
{
	public class GetRecordLockInformationbyid
	{
		public static void GetRecordLockInformationbyid_1(string moduleAPIName, long recordId, long lockId)
		{
			RecordLockingOperations recordLockingOperations = new RecordLockingOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (getrecordlockinformationParam.FIELDS, "Lock_Source__s");
			APIResponse<ResponseHandler> response = recordLockingOperations.GetRecordLockInformationbyid(lockId, recordId, moduleAPIName, paramInstance);
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
						List<Com.Zoho.Crm.API.RecordLocking.RecordLock> recordLocks = responseWrapper.Data;
						foreach (Com.Zoho.Crm.API.RecordLocking.RecordLock recordLock in recordLocks)
						{
							foreach (KeyValuePair<string, object> entry in recordLock.GetKeyValues())
							{
								string keyName = entry.Key;
								Object value = entry.Value;
								if(value is MinifiedUser)
								{
									Com.Zoho.Crm.API.Users.MinifiedUser lockedByS =  (MinifiedUser) value;
									if (lockedByS != null)
									{
										Console.WriteLine ("RecordLocking LockedByS User-ID: " + lockedByS.Id);
										Console.WriteLine ("RecordLocking LockedByS User-Name: " + lockedByS.Name);
										Console.WriteLine ("RecordLocking LockedByS User-Email: " + lockedByS.Email);
									}
								}
								if(value is LockedForS)
								{
									LockedForS lockedForS = (LockedForS) value;
									if (lockedForS != null)
									{
										Console.WriteLine ("RecordLocking LockedForS By User-ID: " + lockedForS.Id);
										Console.WriteLine ("RecordLocking LockedForS By User-Name: " + lockedForS.Name);
										Console.WriteLine ("RecordLocking LockedForS Module KeyName : " + keyName + " - Value : ");
										foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) lockedForS.Module))
										{
											Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
										}
									}
								}
								if (value is IList)
								{
									Console.WriteLine ("RecordLocking KeyName : " + keyName);
                                    IList dataList = (IList)value;
									foreach (object data in dataList)
									{
										if (data is IDictionary)
										{
											Console.WriteLine ("RecordLocking KeyName : " + keyName + " - Value : ");
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
								else if (value is IDictionary)
								{
									Console.WriteLine ("RecordLocking KeyName : " + keyName + " - Value : ");
									foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) value))
									{
										Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
									}
								}
								else
								{
									Console.WriteLine ("RecordLocking KeyName : " + keyName + " - Value : " + value);
								}
							}
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.Count != null)
							{
								Console.WriteLine ("RecordLocking Info Count: " + info.Count);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("RecordLocking Info MoreRecords: " + info.MoreRecords);
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
				string moduleAPIName = "Leads";
				long recordId = 34771l;
				long lockId = 347779001l;
                GetRecordLockInformationbyid_1(moduleAPIName, recordId,lockId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}