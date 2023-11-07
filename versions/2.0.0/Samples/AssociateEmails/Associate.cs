using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.AssociateEmail.APIException;
using ActionHandler = Com.Zoho.Crm.API.AssociateEmail.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.AssociateEmail.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.AssociateEmail.ActionWrapper;
using AssociateEmail = Com.Zoho.Crm.API.AssociateEmail.AssociateEmail;
using AssociateEmailOperations = Com.Zoho.Crm.API.AssociateEmail.AssociateEmailOperations;
using BodyWrapper = Com.Zoho.Crm.API.AssociateEmail.BodyWrapper;
using From = Com.Zoho.Crm.API.AssociateEmail.From;
using SuccessResponse = Com.Zoho.Crm.API.AssociateEmail.SuccessResponse;
using To = Com.Zoho.Crm.API.AssociateEmail.To;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Associateemails
{
	public class Associate
	{
		public static void Associate_1(long recordId, string module)
		{
			AssociateEmailOperations associate_email_operations = new AssociateEmailOperations();
			BodyWrapper request = new BodyWrapper();
			List<AssociateEmail> emails = new List<AssociateEmail>();
			for (int i = 0; i < 1; i++)
			{
				AssociateEmail associateEmail = new AssociateEmail();
				From from = new From();
				from.Email = "abc555@zoho.com";
				from.UserName = "username";
				associateEmail.From = from;
				List<To> tos = new List<To>();
				To to = new To();
				to.Email = "abc1@zoho.com";
				to.UserName = "username1";
				tos.Add (to);
				List<To> tos1 = new List<To>();
				To to1 = new To();
				to1.Email = "abc2@zoho.com";
				to1.UserName = "user_name2";
				tos1.Add (to1);
				List<To> tos2 = new List<To>();
				To to2 = new To();
				to2.Email = "abc3@zoho.com";
				to2.UserName = "user_name3";
				tos2.Add (to2);
				associateEmail.To = tos;
				associateEmail.Cc = tos1;
				associateEmail.Bcc = tos2;
				associateEmail.Subject = "Subject";
				associateEmail.OriginalMessageId = "c6085fab75001d80ffefab46b9c6a7521a63e163835aecd3937749712";
				associateEmail.DateTime = DateTimeOffset.Now;
	//			associateEmail.DateTime = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
				associateEmail.Sent = true;
				associateEmail.Content = "<h3><span style=\\\"background-color: rgb = 254, 255, 102)\\\">Mail is of rich text format</span></h3><h3><span style=\\\"background-color: rgb = 254, 255, 102)\\\">REGARDS,</span></h3><div><span style=\\\"background-color: rgb = 254, 255, 102)\\\">AZ</span></div><div><span style=\\\"background-color: rgb = 254, 255, 102)\\\">ADMIN</span></div> <div></div>";
				associateEmail.MailFormat = new Choice<string>("html");
				List<Com.Zoho.Crm.API.AssociateEmail.Attachments> attachments = new List<Com.Zoho.Crm.API.AssociateEmail.Attachments>();
                Com.Zoho.Crm.API.AssociateEmail.Attachments attachment = new Com.Zoho.Crm.API.AssociateEmail.Attachments();
				attachment.Id = "c6085fae06cbd7b75001d806b1061e063a64154e30b905c6e1efa82c6";
				attachments.Add (attachment);
				associateEmail.Attachments = attachments;
				emails.Add (associateEmail);
			}
			request.Emails = emails;
			APIResponse<ActionHandler> response = associate_email_operations.Associate(recordId, module, request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionresponses = actionWrapper.Emails;
						foreach (ActionResponse actionresponse in actionresponses)
						{
							if (actionresponse is SuccessResponse)
							{
								SuccessResponse successresponse = (SuccessResponse) actionresponse;
								Console.WriteLine ("Status: " + successresponse.Status.Value);
								Console.WriteLine ("Code: " + successresponse.Code.Value);
								Console.WriteLine ("Details: ");
								foreach (KeyValuePair<string, object> entry in successresponse.Details)
								{
									Console.WriteLine (entry.Key + ": " + entry.Value);
								}
								Console.WriteLine ("Message: " + successresponse.Message);
							}
							else if (actionresponse is APIException)
							{
								APIException exception = (APIException) actionresponse;
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
				long recordId = 4402401304002l;
				string moduleAPIName = "Leads";
                Associate_1(recordId, moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}