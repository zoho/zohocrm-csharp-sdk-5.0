using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.Attachments.APIException;
using ActionHandler = Com.Zoho.Crm.API.Attachments.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Attachments.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Attachments.ActionWrapper;
using AttachmentsOperations = Com.Zoho.Crm.API.Attachments.AttachmentsOperations;
using UploadUrlAttachmentsParam = Com.Zoho.Crm.API.Attachments.AttachmentsOperations.UploadUrlAttachmentsParam;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using SuccessResponse = Com.Zoho.Crm.API.Attachments.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Attachments
{
	public class UploadLinkAttachments
	{
		public static void UploadLinkAttachments_1(string moduleAPIName, long recordId, string attachmentURL)
		{
			AttachmentsOperations attachmentsOperations = new AttachmentsOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (UploadUrlAttachmentsParam.ATTACHMENTURL, attachmentURL);
			APIResponse<ActionHandler> response = attachmentsOperations.UploadUrlAttachments(recordId, moduleAPIName, paramInstance);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Data;
						foreach (ActionResponse actionResponse in actionResponses)
						{
							if (actionResponse is SuccessResponse)
							{
								SuccessResponse successResponse = (SuccessResponse) actionResponse;
								Console.WriteLine ("Status: " + successResponse.Status.Value);
								Console.WriteLine ("Code: " + successResponse.Code.Value);
								Console.WriteLine ("Details: ");
								foreach (KeyValuePair<string, object> entry in successResponse.Details)
								{
									Console.WriteLine (entry.Key + ": " + entry.Value);
								}
								Console.WriteLine ("Message: " + successResponse.Message);
							}
							else if (actionResponse is APIException)
							{
								APIException exception = (APIException) actionResponse;
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
					}
					else if (actionHandler is APIException)
					{
						APIException exception = (APIException) actionHandler;
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
				long recordId = 34770615177002l;
				string attachmentURL = "https://5.imimg.com/data5/KJ/UP/MY-860/zoho-crm-500x500.png";
                UploadLinkAttachments_1(moduleAPIName, recordId, attachmentURL);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}