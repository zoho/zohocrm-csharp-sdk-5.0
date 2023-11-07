using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Org.APIException;
using ActionHandler = Com.Zoho.Crm.API.Org.ActionHandler;
using OrgOperations = Com.Zoho.Crm.API.Org.OrgOperations;
using SuccessResponse = Com.Zoho.Crm.API.Org.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Organization
{
	public class UploadOrganizationPhoto
	{
		public static void UploadOrganizationPhoto_1(string absoluteFilePath)
		{
			OrgOperations orgOperations = new OrgOperations();
			Com.Zoho.Crm.API.Org.FileBodyWrapper fileBodyWrapper =  new Com.Zoho.Crm.API.Org.FileBodyWrapper();
			StreamWrapper streamWrapper = new StreamWrapper(absoluteFilePath);
			fileBodyWrapper.File = streamWrapper;
			APIResponse<ActionHandler> response = orgOperations.UploadOrganizationPhoto(fileBodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionResponse = response.Object;
					if (actionResponse is SuccessResponse)
					{
						SuccessResponse successResponse = (SuccessResponse) actionResponse;
						Console.WriteLine ("Status: " + successResponse.Status.Value);
						Console.WriteLine ("Code: " + successResponse.Code.Value);
						Console.WriteLine ("Details: ");
						if (successResponse.Details != null)
						{
							foreach (KeyValuePair<string, object> entry in successResponse.Details)
							{
								Console.WriteLine (entry.Key + ": " + entry.Value);
							}
						}
						Console.WriteLine ("Message: " + successResponse.Message);
					}
					else if (actionResponse is APIException)
					{
						APIException exception = (APIException) actionResponse;
						Console.WriteLine ("Status: " + exception.Status.Value);
						Console.WriteLine ("Code: " + exception.Code.Value);
						Console.WriteLine ("Details: ");
						if (exception.Details != null)
						{
							foreach (KeyValuePair<string, object> entry in exception.Details)
							{
								Console.WriteLine (entry.Key + ": " + entry.Value);
							}
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
				string absoluteFilePath = "/Users/zohocrm-java-sdk-sample/file/download.png";
                UploadOrganizationPhoto_1(absoluteFilePath);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}