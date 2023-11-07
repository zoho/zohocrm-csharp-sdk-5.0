using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.DownloadAttachments.APIException;
using DownloadAttachmentsOperations = Com.Zoho.Crm.API.DownloadAttachments.DownloadAttachmentsOperations;
using GetDownloadAttachmentsDetailsParam = Com.Zoho.Crm.API.DownloadAttachments.DownloadAttachmentsOperations.GetDownloadAttachmentsDetailsParam;
using FileBodyWrapper = Com.Zoho.Crm.API.DownloadAttachments.FileBodyWrapper;
using ResponseHandler = Com.Zoho.Crm.API.DownloadAttachments.ResponseHandler;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.DownloadAttachments
{
	public class GetDownloadAttachmentsDetails
	{
		public static void GetDownloadAttchmentDetails_1(string module, long recordId, long userId, string messageId, string destinationFolder)
		{
			DownloadAttachmentsOperations downloadAttachmentsOperations = new DownloadAttachmentsOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetDownloadAttachmentsDetailsParam.MESSAGE_ID, messageId);
			paramInstance.Add (GetDownloadAttachmentsDetailsParam.USER_ID, userId);
			APIResponse<ResponseHandler> response = downloadAttachmentsOperations.GetDownloadAttachmentsDetails(recordId, module, paramInstance);
			if (response != null)
			{
				Console.WriteLine ("Status Code : " + response.StatusCode);
				if (response.StatusCode == 204)
				{
					Console.WriteLine ("No Content");
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
				string module = "Leads";
				long recordId = 4402480774074l;
				long userId = 4402480254001l;
				string messageId = "c6085fcbd6902b140927cfe9e18";
				string destinationFolder = "/users/zohocrm-java-sdk-sample/file";
                GetDownloadAttchmentDetails_1(module, recordId, userId, messageId, destinationFolder);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}