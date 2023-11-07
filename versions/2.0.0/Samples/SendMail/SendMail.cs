using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Attachment = Com.Zoho.Crm.API.Attachments.Attachment;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using EmailTemplate = Com.Zoho.Crm.API.EmailTemplates.EmailTemplate;
using APIException = Com.Zoho.Crm.API.SendMail.APIException;
using ActionHandler = Com.Zoho.Crm.API.SendMail.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.SendMail.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.SendMail.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.SendMail.BodyWrapper;
using Data = Com.Zoho.Crm.API.SendMail.Data;
using From = Com.Zoho.Crm.API.SendMail.From;
using SendMailOperations = Com.Zoho.Crm.API.SendMail.SendMailOperations;
using SuccessResponse = Com.Zoho.Crm.API.SendMail.SuccessResponse;
using To = Com.Zoho.Crm.API.SendMail.To;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Sendmail
{
	public class SendMail
	{
		public static void SendMail_1(long recordID, string moduleAPIName)
		{
			SendMailOperations sendMailOperations = new SendMailOperations(recordID, moduleAPIName);
			BodyWrapper bodyWrapper = new BodyWrapper();
			List<Data> mails = new List<Data>();
			for (int i = 1; i <= 1; i++)
			{
				Data mail = new Data();
				From userAddressFrom = new From();
				To userAddressTo = new To();
				To userAddressCc = new To();
				To userAddressBcc = new To();
				To userAddressReplyTo = new To();
				Attachment attachment = new Attachment();
				attachment.FileId = "2cceafa194d2181dd81864b4812b1f8b0b4fe0949a982de89fa75a";
				EmailTemplate template = new EmailTemplate();
				template.Id = 4402480627040l;
				mail.Template = template;
				userAddressFrom.UserName = "username";
				userAddressFrom.Email = "abc@zoho.com";
				userAddressTo.UserName = "username1";
				userAddressTo.Email = "abc1@zoho.com";
				userAddressCc.UserName = "username2";
				userAddressCc.Email = "abc2@zoho.com";
				userAddressBcc.UserName = "username3";
				userAddressBcc.Email = "abc3@zoho.com";
				userAddressReplyTo.UserName = "username4";
				userAddressReplyTo.Email = "abc4@zoho.com";
				mail.From = userAddressFrom;
				List<To> userAddressesTo = new List<To>();
				userAddressesTo.Add (userAddressTo);
				mail.To = userAddressesTo;
				List<To> userAddressesBcc = new List<To>();
				userAddressesBcc.Add (userAddressBcc);
				mail.Bcc = userAddressesBcc;
				List<To> userAddressesCc = new List<To>();
				userAddressesCc.Add (userAddressCc);
				mail.Cc = userAddressesCc;
				mail.ReplyTo = userAddressReplyTo;
				mail.OrgEmail = false;
				mail.InReplyTo = "2cceafa194d037b6dd8186486f1eb0360aee76d802b6d376dea97e7";
				DateTimeOffset scheduledTIme = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
				mail.ScheduledTime = scheduledTIme;
				mail.Subject = "Testing Send Mail API";
				mail.Content = "\"<br><a href=\\\\\\\"${ConsentForm.en_US}\\\\\\\" id=\\\\\\\"ConsentForm\\\\\\\" class=\\\\\\\"en_US\\\\\\\" target=\\\\\\\"_blank\\\\\\\">Consent form link<\\/a><br><br><br><br><br><h3><span style=\\\\\\\"background-color: rgb(254, 255, 102)\\\\\\\">REGARDS,<\\/span><\\/h3><div><span style=\\\\\\\"background-color: rgb(254, 255, 102)\\\\\\\">AZ<\\/span><\\/div><div><span style=\\\\\\\"background-color: rgb(254, 255, 102)\\\\\\\">ADMIN<\\/span><\\/div> <div><\\/div>\"";
				mail.MailFormat = new Choice<string>("html");
				mails.Add (mail);
			}
			bodyWrapper.Data = mails;
			APIResponse<ActionHandler> response = sendMailOperations.SendMail(bodyWrapper);
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
				long recordId = 4402480774074l;
				string moduleAPIName = "Leads";
                SendMail_1(recordId, moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}