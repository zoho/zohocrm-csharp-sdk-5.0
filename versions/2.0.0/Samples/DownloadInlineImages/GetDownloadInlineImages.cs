using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.DownloadInlineImages.APIException;
using DownloadInlineImagesOperations = Com.Zoho.Crm.API.DownloadInlineImages.DownloadInlineImagesOperations;
using GetDownloadInlineImagesParam = Com.Zoho.Crm.API.DownloadInlineImages.DownloadInlineImagesOperations.GetDownloadInlineImagesParam;
using FileBodyWrapper = Com.Zoho.Crm.API.DownloadInlineImages.FileBodyWrapper;
using ResponseHandler = Com.Zoho.Crm.API.DownloadInlineImages.ResponseHandler;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Downloadinlineimages
{
	public class GetDownloadInlineImages
	{
		public static void GetDownloadInlineImages_1(string module, long recordId, long userId, string messageId, string id, string destinationFolder)
		{
			DownloadInlineImagesOperations downloadInlineImagesOperations = new DownloadInlineImagesOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetDownloadInlineImagesParam.MESSAGE_ID, messageId);
			paramInstance.Add (GetDownloadInlineImagesParam.USER_ID, userId);
			paramInstance.Add (GetDownloadInlineImagesParam.ID, id);
			APIResponse<ResponseHandler> response = downloadInlineImagesOperations.GetDownloadInlineImages(recordId, module, paramInstance);
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
							Console.WriteLine (entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
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
				string module = "Leads";
				long recordId = 4402480774074l;
				long userId = 4402480254001l;
				string id = "645baab5ea43dd528292d648b400db2c36658d5845edad86fda3ac084b6a2f91823f8f602de784c26c619667564d7d1017304deeb964d78a3321";
				string messageId = "c6085fae06cbd7b75001fefab46bf0f413bb368a1d6902b140927cfe9e18";
				string destinationFolder = "/users/zohocrm-java-sdk-sample/file";
                GetDownloadInlineImages_1(module, recordId, userId, messageId, id, destinationFolder);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}