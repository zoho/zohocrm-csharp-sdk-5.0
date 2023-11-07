using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using MassUpdate = Com.Zoho.Crm.API.Record.MassUpdate;
using MassUpdateResponse = Com.Zoho.Crm.API.Record.MassUpdateResponse;
using MassUpdateResponseHandler = Com.Zoho.Crm.API.Record.MassUpdateResponseHandler;
using MassUpdateResponseWrapper = Com.Zoho.Crm.API.Record.MassUpdateResponseWrapper;
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using GetMassUpdateStatusParam = Com.Zoho.Crm.API.Record.RecordOperations.GetMassUpdateStatusParam;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Record
{
	public class GetMassUpdateStatus
	{
		public static void GetMassUpdateStatus_1(string moduleAPIName, string jobId)
		{
			RecordOperations recordOperations = new RecordOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetMassUpdateStatusParam.JOB_ID, jobId);
			APIResponse<MassUpdateResponseHandler> response = recordOperations.GetMassUpdateStatus(moduleAPIName, paramInstance);
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
					MassUpdateResponseHandler massUpdateResponseHandler = response.Object;
					if (massUpdateResponseHandler is MassUpdateResponseWrapper)
					{
						MassUpdateResponseWrapper massUpdateResponseWrapper = (MassUpdateResponseWrapper) massUpdateResponseHandler;
						List<MassUpdateResponse> massUpdateResponses = massUpdateResponseWrapper.Data;
						foreach (MassUpdateResponse massUpdateResponse in massUpdateResponses)
						{
							if (massUpdateResponse is MassUpdate)
							{
								MassUpdate massUpdate = (MassUpdate) massUpdateResponse;
								Console.WriteLine ("MassUpdate Status: " + massUpdate.Status.Value);
								Console.WriteLine ("MassUpdate FailedCount: " + massUpdate.FailedCount);
								Console.WriteLine ("MassUpdate UpdatedCount: " + massUpdate.UpdatedCount);
								Console.WriteLine ("MassUpdate NotUpdatedCount: " + massUpdate.NotUpdatedCount);
								Console.WriteLine ("MassUpdate TotalCount: " + massUpdate.TotalCount);
							}
							else if (massUpdateResponse is APIException)
							{
								APIException exception = (APIException) massUpdateResponse;
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
					else if (massUpdateResponseHandler is APIException)
					{
						APIException exception = (APIException) massUpdateResponseHandler;
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
				string jobId = "347706117121011";
                GetMassUpdateStatus_1(moduleAPIName, jobId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}