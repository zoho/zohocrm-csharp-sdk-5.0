using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using FileBodyWrapper = Com.Zoho.Crm.API.Record.FileBodyWrapper;
using FileHandler = Com.Zoho.Crm.API.Record.FileHandler;
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Record
{
	public class UploadPhoto
	{
		public static void UploadPhoto_1(string moduleAPIName, long recordId, string absoluteFilePath)
		{
			RecordOperations recordOperations = new RecordOperations();
			FileBodyWrapper fileBodyWrapper = new FileBodyWrapper();
			StreamWrapper streamWrapper = new StreamWrapper(absoluteFilePath);
			fileBodyWrapper.File = streamWrapper;
			ParameterMap paramInstance = new ParameterMap();
	//		paramInstance.Add (UploadPhotoParam.RESTRICT_TRIGGERS, "workflow");
			APIResponse<FileHandler> response = recordOperations.UploadPhoto(recordId, moduleAPIName, fileBodyWrapper, paramInstance);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					FileHandler fileHandler = response.Object;
					if (fileHandler is SuccessResponse)
					{
						SuccessResponse successResponse = (SuccessResponse) fileHandler;
						Console.WriteLine ("Status: " + successResponse.Status.Value);
						Console.WriteLine ("Code: " + successResponse.Code.Value);
						Console.WriteLine ("Details: ");
						foreach (KeyValuePair<string, object> entry in successResponse.Details)
						{
							Console.WriteLine (entry.Key + ": " + entry.Value);
						}
						Console.WriteLine ("Message: " + successResponse.Message.Value);
					}
					else if (fileHandler is APIException)
					{
						APIException exception = (APIException) fileHandler;
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
				long recordId = 34770615177002L;
				string absoluteFilePath = "/Users/zohocrm-java-sdk-sample/file/download.png";
                UploadPhoto_1(moduleAPIName, recordId, absoluteFilePath);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}