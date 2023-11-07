using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.ShareRecords.APIException;
using DeleteActionHandler = Com.Zoho.Crm.API.ShareRecords.DeleteActionHandler;
using DeleteActionResponse = Com.Zoho.Crm.API.ShareRecords.DeleteActionResponse;
using DeleteActionWrapper = Com.Zoho.Crm.API.ShareRecords.DeleteActionWrapper;
using ShareRecordsOperations = Com.Zoho.Crm.API.ShareRecords.ShareRecordsOperations;
using SuccessResponse = Com.Zoho.Crm.API.ShareRecords.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Sharerecords
{
	public class RevokeSharedRecord
	{
		public static void RevokeSharedRecord_1(string moduleAPIName, long recordId)
		{
			ShareRecordsOperations shareRecordsOperations = new ShareRecordsOperations(recordId, moduleAPIName);
			APIResponse<DeleteActionHandler> response = shareRecordsOperations.RevokeSharedRecord();
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					DeleteActionHandler deleteActionHandler = response.Object;
					if (deleteActionHandler is DeleteActionWrapper)
					{
						DeleteActionWrapper deleteActionWrapper = (DeleteActionWrapper) deleteActionHandler;
						DeleteActionResponse deleteActionResponse = deleteActionWrapper.Share;
						if (deleteActionResponse is SuccessResponse)
						{
							SuccessResponse successResponse = (SuccessResponse) deleteActionResponse;
							Console.WriteLine ("Status: " + successResponse.Status.Value);
							Console.WriteLine ("Code: " + successResponse.Code.Value);
							Console.WriteLine ("Details: ");
							foreach (KeyValuePair<string, object> entry in successResponse.Details)
							{
								Console.WriteLine (entry.Key + ": " + entry.Value);
							}
							Console.WriteLine ("Message: " + successResponse.Message.Value);
						}
						else if (deleteActionResponse is APIException)
						{
							APIException exception = (APIException) deleteActionResponse;
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
					else if (deleteActionHandler is APIException)
					{
						APIException exception = (APIException) deleteActionHandler;
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
                RevokeSharedRecord_1(moduleAPIName, recordId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}