using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using DownloadHandler = Com.Zoho.Crm.API.Record.DownloadHandler;
using FileBodyWrapper = Com.Zoho.Crm.API.Record.FileBodyWrapper;
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Record
{
	public class GetPhoto
	{
		public static void GetPhoto_1(string moduleAPIName, long recordId, string destinationFolder)
		{
			RecordOperations recordOperations = new RecordOperations();
			APIResponse<DownloadHandler> response = recordOperations.GetPhoto(recordId, moduleAPIName);
			if (response != null)
			{
				Console.WriteLine("Status Code: " + response.StatusCode);
				if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
				{
					Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");
					return;
				}
				if (response.IsExpected)
				{
					DownloadHandler downloadHandler = response.Object;
					if (downloadHandler is FileBodyWrapper)
					{
						FileBodyWrapper fileBodyWrapper = (FileBodyWrapper)downloadHandler;
						StreamWrapper streamWrapper = fileBodyWrapper.File;
						Stream file = streamWrapper.Stream;
						string fullFilePath = Path.Combine(destinationFolder, streamWrapper.Name);
						using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
						{
							file.CopyTo(outputFileStream);
						}
					}
					else if (downloadHandler is APIException)
					{
						APIException exception = (APIException)downloadHandler;
						Console.WriteLine("Status: " + exception.Status.Value);
						Console.WriteLine("Code: " + exception.Code.Value);
						Console.WriteLine("Details: ");
						foreach (KeyValuePair<string, object> entry in exception.Details)
						{
							Console.WriteLine(entry.Key + ": " + entry.Value);
						}
						Console.WriteLine("Message: " + exception.Message.Value);
					}
				}
				else
				{// If response is not as expected
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
				string destinationFolder = "/Users/zohocrm-java-sdk-sample/file/";
                GetPhoto_1(moduleAPIName, recordId, destinationFolder);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}