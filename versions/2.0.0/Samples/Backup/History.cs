using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.Backup.APIException;
using BackupOperations = Com.Zoho.Crm.API.Backup.BackupOperations;
using HistoryWrapper = Com.Zoho.Crm.API.Backup.HistoryWrapper;
using Info = Com.Zoho.Crm.API.Backup.Info;
using Requester = Com.Zoho.Crm.API.Backup.Requester;
using ResponseHandler = Com.Zoho.Crm.API.Backup.ResponseHandler;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Backup
{
	public class History
	{
		public static void History_1()
		{
			BackupOperations backupOperations = new BackupOperations();
			ParameterMap paramInstance = new ParameterMap();
			APIResponse<ResponseHandler> response = backupOperations.History(paramInstance);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ResponseHandler responseHandler = response.Object;
					if (responseHandler is HistoryWrapper)
					{
						HistoryWrapper historyWrapper = (HistoryWrapper) responseHandler;
						List<Com.Zoho.Crm.API.Backup.History> history = historyWrapper.History;
						foreach (Com.Zoho.Crm.API.Backup.History history1 in  history)
						{
							Console.WriteLine ("History Id: " + history1.Id);
							Requester doneBy = history1.DoneBy;
							if (doneBy != null)
							{
								Console.WriteLine ("History DoneBy Id: " + doneBy.Id);
								Console.WriteLine ("History DoneBy Name: " + doneBy.Name);
								Console.WriteLine ("History DoneBy Zuid: " + doneBy.Zuid);
							}
							Console.WriteLine ("History LogTime: " + history1.LogTime);
							Console.WriteLine ("History State: " + history1.State);
							Console.WriteLine ("History Action: " + history1.Action);
							Console.WriteLine ("History RepeatType: " + history1.RepeatType);
							Console.WriteLine ("History FileName: " + history1.FileName);
							Console.WriteLine ("History Count: " + history1.Count);
						}
						Info info = historyWrapper.Info;
						if (info != null)
						{
							if (info.PerPage != null)
							{
								Console.WriteLine ("History Info PerPage: " + info.PerPage);
							}
							if (info.Count != null)
							{
								Console.WriteLine ("History Info Count: " + info.Count);
							}
							if (info.Page != null)
							{
								Console.WriteLine ("History Info Page: " + info.Page);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("History Info MoreRecords: " + info.MoreRecords);
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
                History_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}