using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.Backup.APIException;
using ActionHandler = Com.Zoho.Crm.API.Backup.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Backup.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Backup.ActionWrapper;
using BackupOperations = Com.Zoho.Crm.API.Backup.BackupOperations;
using SuccessResponse = Com.Zoho.Crm.API.Backup.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Backup
{
	public class Cancel
	{
		public static void Cancel_1(long Id)
		{
			BackupOperations backupOperations = new BackupOperations();
			APIResponse<ActionHandler> response = backupOperations.Cancel(Id);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						ActionResponse actionResponse = actionWrapper.Backup;
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
							Console.WriteLine ("Message: " + successResponse.Message);
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
							Console.WriteLine ("Message: " + exception.Message);
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
				long Id = 347706116801009l;
                Cancel_1(Id);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}