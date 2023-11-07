using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.MassChangeOwner.APIException;
using MassChangeOwnerOperations = Com.Zoho.Crm.API.MassChangeOwner.MassChangeOwnerOperations;
using CheckStatusParam = Com.Zoho.Crm.API.MassChangeOwner.MassChangeOwnerOperations.CheckStatusParam;
using ResponseHandler = Com.Zoho.Crm.API.MassChangeOwner.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.MassChangeOwner.ResponseWrapper;
using Status = Com.Zoho.Crm.API.MassChangeOwner.Status;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Masschangeowner
{
	public class CheckStatus
	{
		public static void CheckStatus_1(long jobId, string module)
		{
			MassChangeOwnerOperations massChangeOwnerOperations = new MassChangeOwnerOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (CheckStatusParam.JOB_ID, jobId);
			APIResponse<ResponseHandler> response = massChangeOwnerOperations.CheckStatus(module, paramInstance);
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
							Console.WriteLine ("MassChangeOwner TotalCount: " + status1.TotalCount);
							Console.WriteLine ("MassChangeOwner UpdatedCount: " + status1.UpdatedCount);
							Console.WriteLine ("MassChangeOwner NotUpdatedCount: " + status1.NotUpdatedCount);
							Console.WriteLine ("MassChangeOwner FailedCount: " + status1.FailedCount);
							Console.WriteLine ("MassChangeOwner Status: " + status1.Status_1);
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
				string moduleAPIName = "Leads";
				long jobId = 347706117101007l;
                CheckStatus_1(jobId, moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}