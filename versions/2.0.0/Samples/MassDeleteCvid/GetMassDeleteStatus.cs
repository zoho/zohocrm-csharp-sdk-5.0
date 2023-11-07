using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.MassDeleteCvid.APIException;
using MassDeleteCvidOperations = Com.Zoho.Crm.API.MassDeleteCvid.MassDeleteCvidOperations;
using GetMassDeleteStatusParam = Com.Zoho.Crm.API.MassDeleteCvid.MassDeleteCvidOperations.GetMassDeleteStatusParam;
using ResponseHandler = Com.Zoho.Crm.API.MassDeleteCvid.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.MassDeleteCvid.ResponseWrapper;
using Status = Com.Zoho.Crm.API.MassDeleteCvid.Status;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Massdeletecvid
{
	public class GetMassDeleteStatus
	{
		public static void GetMassDeleteStatus_1(long jobId, string moduleAPIName)
		{
			MassDeleteCvidOperations massDeleteCvidOperations = new MassDeleteCvidOperations(moduleAPIName);
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetMassDeleteStatusParam.JOB_ID, jobId);
			APIResponse<ResponseHandler> response = massDeleteCvidOperations.GetMassDeleteStatus(paramInstance);
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
						List<Status> status = responseWrapper.Data;
						foreach (Status status1 in status)
						{
							Console.WriteLine ("MassDelete TotalCount: " + status1.TotalCount);
							Console.WriteLine ("MassDelete ConvertedCount: " + status1.DeletedCount);
							Console.WriteLine ("MassDelete FailedCount: " + status1.FailedCount);
							Console.WriteLine ("MassDelete Status: " + status1.Status_1);
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
				string moduleAPIName = "DOT";
				long jobId = 347706116634118l;
                GetMassDeleteStatus_1(jobId, moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}