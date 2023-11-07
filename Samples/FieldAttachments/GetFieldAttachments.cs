using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.FieldAttachments.APIException;
using FieldAttachmentsOperations = Com.Zoho.Crm.API.FieldAttachments.FieldAttachmentsOperations;
using FileBodyWrapper = Com.Zoho.Crm.API.FieldAttachments.FileBodyWrapper;
using ResponseHandler = Com.Zoho.Crm.API.FieldAttachments.ResponseHandler;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Fieldattachments
{
	public class GetFieldAttachments
	{
		public static void GetFieldAttachments_1(string destinationFolder, string moduleAPIName, long recordId, long attachmentID)
		{
			FieldAttachmentsOperations fieldAttachmentsOperations = new FieldAttachmentsOperations(moduleAPIName, recordId, attachmentID);
			APIResponse<ResponseHandler> response = fieldAttachmentsOperations.GetFieldAttachments();
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
					if (responseHandler is FileBodyWrapper)
					{
						FileBodyWrapper fileBodyWrapper = (FileBodyWrapper) responseHandler;
                        StreamWrapper streamWrapper = fileBodyWrapper.File;
                        Stream file = streamWrapper.Stream;
                        string fullFilePath = Path.Combine(destinationFolder, streamWrapper.Name);
                        using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            file.CopyTo(outputFileStream);
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
				string destinationFolder = "/users/zohocrm-java-sdk-sample/file";
				string moduleAPIName = "Leads";
				long recordId = 4402480774074l;
				long attachmentID = 440248001286011l;
                GetFieldAttachments_1(destinationFolder, moduleAPIName, recordId, attachmentID);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}