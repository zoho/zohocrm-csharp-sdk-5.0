using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.MassConvert.APIException;
using MassConvertOperations = Com.Zoho.Crm.API.MassConvert.MassConvertOperations;
using ResponseHandler = Com.Zoho.Crm.API.MassConvert.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.MassConvert.ResponseWrapper;
using Status = Com.Zoho.Crm.API.MassConvert.Status;
using GetJobStatusParam = Com.Zoho.Crm.API.MassConvert.MassConvertOperations.GetJobStatusParam;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Massconvert
{
	public class GetJobStatus
	{
		public static void GetJobStatus_1(long jobId)
		{
			MassConvertOperations massConvertOperations = new MassConvertOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetJobStatusParam.JOB_ID, jobId);
			APIResponse<ResponseHandler> response = massConvertOperations.GetJobStatus(paramInstance);
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
							Console.WriteLine ("MassConvert TotalCount: " + status1.TotalCount);
							Console.WriteLine ("MassConvert ConvertedCount: " + status1.ConvertedCount);
							Console.WriteLine ("MassConvert NotConvertedCount: " + status1.NotConvertedCount);
							Console.WriteLine ("MassConvert FailedCount: " + status1.FailedCount);
							Console.WriteLine ("MassConvert Status: " + status1.Status_1);
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
				long jobId = 347706116704007;
                GetJobStatus_1(jobId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}