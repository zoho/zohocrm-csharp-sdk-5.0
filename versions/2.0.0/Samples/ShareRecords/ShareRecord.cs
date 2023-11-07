using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.ShareRecords.APIException;
using ActionHandler = Com.Zoho.Crm.API.ShareRecords.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.ShareRecords.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.ShareRecords.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.ShareRecords.BodyWrapper;
using ShareRecordsOperations = Com.Zoho.Crm.API.ShareRecords.ShareRecordsOperations;
using SuccessResponse = Com.Zoho.Crm.API.ShareRecords.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Sharerecords
{
	public class ShareRecord
	{
		public static void ShareRecord_1(string moduleAPIName, long recordId)
		{
			ShareRecordsOperations shareRecordsOperations = new ShareRecordsOperations(recordId, moduleAPIName);
			BodyWrapper request = new BodyWrapper();
			List<Com.Zoho.Crm.API.ShareRecords.ShareRecord> shareList = new List<Com.Zoho.Crm.API.ShareRecords.ShareRecord>();
			Com.Zoho.Crm.API.ShareRecords.ShareRecord share1 =  new Com.Zoho.Crm.API.ShareRecords.ShareRecord();
			for (int i = 0; i < 9; i++)
			{
				share1 =  new Com.Zoho.Crm.API.ShareRecords.ShareRecord();
				share1.ShareRelatedRecords = true;
				share1.Permission = new Choice<string>("read_write");
                Com.Zoho.Crm.API.Users.Users user_1 = new Com.Zoho.Crm.API.Users.Users();
                user_1.Id = 3477021;
				share1.User = user_1;
                Com.Zoho.Crm.API.Users.Users sharedWith_1 = new Com.Zoho.Crm.API.Users.Users();
                sharedWith_1.Id = 3477061091024;
                sharedWith_1.AddKeyValue("type", "users");
				share1.SharedWith = sharedWith_1;
				share1.Type = new Choice<string>("private");
				shareList.Add (share1);
			}
			share1 =  new Com.Zoho.Crm.API.ShareRecords.ShareRecord();
			share1.ShareRelatedRecords = true;
			share1.Permission = new Choice<string>("read_write");
            Com.Zoho.Crm.API.Users.Users user = new Com.Zoho.Crm.API.Users.Users();
			user.Id = 34773021;
			share1.User = user;
            Com.Zoho.Crm.API.Users.Users sharedWith = new Com.Zoho.Crm.API.Users.Users();
			sharedWith.Id = 347791024;
			sharedWith.AddKeyValue("type", "users");
			share1.SharedWith = sharedWith;
			share1.Type = new Choice<string>("private");
			shareList.Add (share1);
			request.Notify = true;
			request.Share = shareList;
			APIResponse<ActionHandler> response = shareRecordsOperations.ShareRecord(request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Share;
						foreach (ActionResponse actionResponse in actionResponses)
						{
							if (actionResponse is SuccessResponse)
							{
								SuccessResponse successResponse = (SuccessResponse) actionResponse;
								Console.WriteLine ("Status: " + successResponse.Status.Value);
								Console.WriteLine ("Code: " + successResponse.Code.Value);
								Console.WriteLine ("Details: ");
								foreach (KeyValuePair<string, object> entry in successResponse.Details)
								{
									Console.WriteLine (entry.Key + ": " + entry.Value);
								}
								Console.WriteLine ("Message: " + successResponse.Message.Value);
							}
							else if (actionResponse is APIException)
							{
								APIException exception = (APIException) actionResponse;
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
					}
					else if (actionHandler is APIException)
					{
						APIException exception = (APIException) actionHandler;
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
				long recordId = 347706114963002l;
				string moduleAPIName = "Leads";
                ShareRecord_1(moduleAPIName, recordId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}