using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.EmailRelatedRecords.APIException;
using Email = Com.Zoho.Crm.API.EmailRelatedRecords.Email;
using EmailRelatedRecordsOperations = Com.Zoho.Crm.API.EmailRelatedRecords.EmailRelatedRecordsOperations;
using Info = Com.Zoho.Crm.API.EmailRelatedRecords.Info;
using LinkedRecord = Com.Zoho.Crm.API.EmailRelatedRecords.LinkedRecord;
using ResponseHandler = Com.Zoho.Crm.API.EmailRelatedRecords.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.EmailRelatedRecords.ResponseWrapper;
using Status = Com.Zoho.Crm.API.EmailRelatedRecords.Status;
using UserDetails = Com.Zoho.Crm.API.EmailRelatedRecords.UserDetails;
using MinifiedUser = Com.Zoho.Crm.API.Users.MinifiedUser;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Emailrelatedrecords
{
    public class GetRelatedEmail
	{
		public static void GetRelatedEmail_1(string moduleAPIName, long id, string messageId)
		{
			EmailRelatedRecordsOperations emailTemplatesOperations = new EmailRelatedRecordsOperations(id, moduleAPIName);
			APIResponse<ResponseHandler> response = emailTemplatesOperations.GetEmailsRelatedRecord(messageId);
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
						List<Email> emailTemplates = responseWrapper.Emails;
						foreach (Email emailTemplate in emailTemplates)
						{
							List<UserDetails> userDetails = emailTemplate.Cc;
							if (userDetails != null)
							{
								foreach (UserDetails userDetail in userDetails)
								{
									Console.WriteLine ("EmailRelatedRecords User Email: " + userDetail.Email);
									Console.WriteLine ("EmailRelatedRecords User Name: " + userDetail.UserName);
								}
							}
							Console.WriteLine ("EmailRelatedRecords Summary: " + emailTemplate.Summary);
							MinifiedUser owner = emailTemplate.Owner;
							if (owner != null)
							{
								Console.WriteLine ("EmailRelatedRecords User ID: " + owner.Id);
								Console.WriteLine ("EmailRelatedRecords User Name: " + owner.Name);
							}
							Console.WriteLine ("EmailRelatedRecords Read: " + emailTemplate.Read);
							Console.WriteLine ("EmailRelatedRecords Sent: " + emailTemplate.Sent);
							Console.WriteLine ("EmailRelatedRecords Subject: " + emailTemplate.Subject);
							Console.WriteLine ("EmailRelatedRecords Intent: " + emailTemplate.Intent);
							Console.WriteLine ("EmailRelatedRecords Content: " + emailTemplate.Content);
							Console.WriteLine ("EmailRelatedRecords SentimentInfo: " + emailTemplate.SentimentInfo);
							Console.WriteLine ("EmailRelatedRecords MessageId: " + emailTemplate.MessageId);
							Console.WriteLine ("EmailRelatedRecords Source: " + emailTemplate.Source);
							LinkedRecord linkedRecord = emailTemplate.LinkedRecord;
							if (linkedRecord != null)
							{
								Console.WriteLine ("EmailRelatedRecords LinkedRecord id : " + linkedRecord.Id);
								Com.Zoho.Crm.API.EmailRelatedRecords.Module module =  linkedRecord.Module;
								if (module != null)
								{
									Console.WriteLine ("EmailRelatedRecords LinkedRecord Module APIName : " + module.APIName);
									Console.WriteLine ("EmailRelatedRecords LinkedRecord Module Id : " + module.Id);
								}
							}
							List<Com.Zoho.Crm.API.EmailRelatedRecords.Attachments> attachments = emailTemplate.Attachments;
							if (attachments != null)
							{
								foreach (Com.Zoho.Crm.API.EmailRelatedRecords.Attachments attachment in attachments)
								{
									Console.WriteLine ("EmailRelatedRecords Attachmnet Size :" + attachment.Size);
									Console.WriteLine ("EmailRelatedRecords Attachmnet Name :" + attachment.Name);
									Console.WriteLine ("EmailRelatedRecords Attachmnet Id :" + attachment.Id);
								}
							}
							Console.WriteLine ("EmailRelatedRecords Emotion: " + emailTemplate.Emotion);
							UserDetails from = emailTemplate.From;
							if (from != null)
							{
								Console.WriteLine ("EmailRelatedRecords From User Email: " + from.Email);
								Console.WriteLine ("EmailRelatedRecords From User Name: " + from.UserName);
							}
							List<UserDetails> toUserDetails = emailTemplate.To;
							if (toUserDetails != null)
							{
								foreach (UserDetails userDetail in toUserDetails)
								{
									Console.WriteLine ("EmailRelatedRecords User Email: " + userDetail.Email);
									Console.WriteLine ("EmailRelatedRecords User Name: " + userDetail.UserName);
								}
							}
							Console.WriteLine ("EmailRelatedRecords Time: " + emailTemplate.Time);
							List<Status> status = emailTemplate.Status;
							if (status != null)
							{
								foreach (Status status1 in status)
								{
									Console.WriteLine ("EmailRelatedRecords Status Type: " + status1.Type);
									Console.WriteLine ("EmailRelatedRecords Status BouncedTime: " + status1.BouncedTime);
									Console.WriteLine ("EmailRelatedRecords Status BouncedReason: " + status1.BouncedReason);
								}
							}
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.Count != null)
							{
								Console.WriteLine ("Record Info Count: " + info.Count);
							}
							if (info.NextIndex != null)
							{
								Console.WriteLine ("Record Info NextIndex: " + info.NextIndex);
							}
							if (info.PrevIndex != null)
							{
								Console.WriteLine ("Record Info PrevIndex: " + info.PrevIndex);
							}
							if (info.PerPage != null)
							{
								Console.WriteLine ("Record Info PerPage: " + info.PerPage);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("Record Info MoreRecords: " + info.MoreRecords);
							}
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
				long Id = 4402480774074l;
				string messageId = "c6085fae06cbd7b75001d80ffefab46b7d76f8540fc65c6bc779dfe4aab8d727";
                GetRelatedEmail_1(moduleAPIName, Id, messageId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}