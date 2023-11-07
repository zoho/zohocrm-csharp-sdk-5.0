using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.Backup.APIException;
using BackupOperations = Com.Zoho.Crm.API.Backup.BackupOperations;
using BodyWrapper = Com.Zoho.Crm.API.Backup.BodyWrapper;
using Requester = Com.Zoho.Crm.API.Backup.Requester;
using ResponseHandler = Com.Zoho.Crm.API.Backup.ResponseHandler;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Backup
{
	public class GetDetails
	{
		public static void GetDetails_1()
		{
			BackupOperations backupOperations = new BackupOperations();
			APIResponse<ResponseHandler> response = backupOperations.GetDetails();
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
						Com.Zoho.Crm.API.Backup.Backup backup =  responseWrapper.Backup;
						if (backup != null)
						{
							Console.WriteLine ("Backup Rrule: " + backup.Rrule);
							Console.WriteLine ("Backup Id: " + backup.Id);
							Console.WriteLine ("Backup StartDate: " + backup.StartDate);
							Console.WriteLine ("Backup ScheduledDate: " + backup.ScheduledDate);
							Console.WriteLine ("Backup Status: " + backup.Status);
							Requester requester = backup.Requester;
							if (requester != null)
							{
								Console.WriteLine ("Backup Requester Id: " + requester.Id);
								Console.WriteLine ("Backup Requester Name: " + requester.Name);
								Console.WriteLine ("Backup Requester Zuid: " + requester.Zuid);
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
				else if (response.StatusCode != 204)
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
                GetDetails_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}