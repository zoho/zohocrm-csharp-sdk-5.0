using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using HeaderMap = Com.Zoho.Crm.API.HeaderMap;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using DeletedRecord = Com.Zoho.Crm.API.Record.DeletedRecord;
using DeletedRecordsHandler = Com.Zoho.Crm.API.Record.DeletedRecordsHandler;
using DeletedRecordsWrapper = Com.Zoho.Crm.API.Record.DeletedRecordsWrapper;
using Info = Com.Zoho.Crm.API.Record.Info;
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using GetDeletedRecordsHeader = Com.Zoho.Crm.API.Record.RecordOperations.GetDeletedRecordsHeader;
using GetDeletedRecordsParam = Com.Zoho.Crm.API.Record.RecordOperations.GetDeletedRecordsParam;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Record
{
	public class GetDeletedRecords
	{
		public static void GetDeletedRecords_1(string moduleAPIName)
		{
			RecordOperations recordOperations = new RecordOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetDeletedRecordsParam.TYPE, "all");// all, recycle, permanent
			paramInstance.Add (GetDeletedRecordsParam.PAGE, 1);
			paramInstance.Add(GetDeletedRecordsParam.PER_PAGE, 2);
			HeaderMap headerInstance = new HeaderMap();
			DateTimeOffset ifModifiedSince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
			headerInstance.Add(GetDeletedRecordsHeader.IF_MODIFIED_SINCE, ifModifiedSince);
			APIResponse<DeletedRecordsHandler> response = recordOperations.GetDeletedRecords(moduleAPIName, paramInstance, headerInstance);
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
					DeletedRecordsHandler deletedRecordsHandler = response.Object;
					if (deletedRecordsHandler is DeletedRecordsWrapper)
					{
						DeletedRecordsWrapper deletedRecordsWrapper = (DeletedRecordsWrapper) deletedRecordsHandler;
						List<DeletedRecord> deletedRecords = deletedRecordsWrapper.Data;
						foreach (DeletedRecord deletedRecord in deletedRecords)
						{
							Com.Zoho.Crm.API.Users.MinifiedUser deletedBy =  deletedRecord.DeletedBy;
							if (deletedBy != null)
							{
								Console.WriteLine ("DeletedRecord Deleted By User-Name: " + deletedBy.Name);
								Console.WriteLine ("DeletedRecord Deleted By User-ID: " + deletedBy.Id);
							}
							Console.WriteLine ("DeletedRecord ID: " + deletedRecord.Id);
							Console.WriteLine ("DeletedRecord DisplayName: " + deletedRecord.DisplayName);
							Console.WriteLine ("DeletedRecord Type: " + deletedRecord.Type);
							Com.Zoho.Crm.API.Users.MinifiedUser createdBy =  deletedRecord.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("DeletedRecord Created By User-Name: " + createdBy.Name);
								Console.WriteLine ("DeletedRecord Created By User-ID: " + createdBy.Id);
							}
							Console.WriteLine ("DeletedRecord DeletedTime: " + deletedRecord.DeletedTime);
						}
						Info info = deletedRecordsWrapper.Info;
						if (info != null)
						{
							if (info.PerPage != null)
							{
								Console.WriteLine ("Record Info PerPage: " + info.PerPage);
							}
							if (info.Count != null)
							{
								Console.WriteLine ("Record Info Count: " + info.Count);
							}
							if (info.Page != null)
							{
								Console.WriteLine ("Record Info Page: " + info.Page);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("Record Info MoreRecords: " + info.MoreRecords);
							}
						}
					}
					else if (deletedRecordsHandler is APIException)
					{
						APIException exception = (APIException) deletedRecordsHandler;
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
                GetDeletedRecords_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}