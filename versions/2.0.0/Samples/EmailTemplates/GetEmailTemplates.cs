using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.EmailTemplates.APIException;
using Attachment = Com.Zoho.Crm.API.EmailTemplates.Attachment;
using EmailTemplate = Com.Zoho.Crm.API.EmailTemplates.EmailTemplate;
using EmailTemplatesOperations = Com.Zoho.Crm.API.EmailTemplates.EmailTemplatesOperations;
using LastVersionStatistics = Com.Zoho.Crm.API.EmailTemplates.LastVersionStatistics;
using ResponseHandler = Com.Zoho.Crm.API.EmailTemplates.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.EmailTemplates.ResponseWrapper;
using GetEmailTemplatesParam = Com.Zoho.Crm.API.EmailTemplates.EmailTemplatesOperations.GetEmailTemplatesParam;
using Info = Com.Zoho.Crm.API.EmailTemplates.Info;
using Folder = Com.Zoho.Crm.API.InventoryTemplates.Folder;
using MinifiedModule = Com.Zoho.Crm.API.Modules.MinifiedModule;
using MinifiedUser = Com.Zoho.Crm.API.Users.MinifiedUser;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Emailtemplates
{
	public class GetEmailTemplates
	{
		public static void GetEmailTemplates_1(string moduleAPIName)
		{
			EmailTemplatesOperations emailTemplatesOperations = new EmailTemplatesOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetEmailTemplatesParam.MODULE, moduleAPIName);
			APIResponse<ResponseHandler> response = emailTemplatesOperations.GetEmailTemplates(paramInstance);
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
						List<EmailTemplate> emailTemplates = responseWrapper.EmailTemplates;
						foreach (EmailTemplate emailTemplate in emailTemplates)
						{
							Console.WriteLine ("EmailTemplate CreatedTime: " + emailTemplate.CreatedTime);
							List<Attachment> attachments = emailTemplate.Attachments;
							if (attachments != null)
							{
								foreach (Attachment attachment in attachments)
								{
									Console.WriteLine ("EmailTemplate Attachment File ID: " + attachment.FileId);
									Console.WriteLine ("EmailTemplate Attachment File Name: " + attachment.FileName);
									Console.WriteLine ("EmailTemplate Attachment Size: " + attachment.Size);
									Console.WriteLine ("EmailTemplate Attachment ID: " + attachment.Id);
								}
							}
							Console.WriteLine ("EmailTemplate Subject: " + emailTemplate.Subject);
							MinifiedModule module = emailTemplate.Module;
							if (module != null)
							{
								Console.WriteLine ("EmailTemplate Module ID: " + module.Id);
								Console.WriteLine ("EmailTemplate Module apiName: " + module.APIName);
							}
							LastVersionStatistics lastversionstatistics = emailTemplate.LastVersionStatistics;
							if (lastversionstatistics != null)
							{
								Console.WriteLine ("EmailTemplate Module Tracked: " + lastversionstatistics.Tracked);
								Console.WriteLine ("EmailTemplate Module Delivered: " + lastversionstatistics.Delivered);
								Console.WriteLine ("EmailTemplate Module Opened: " + lastversionstatistics.Opened);
								Console.WriteLine ("EmailTemplate Module Bounced: " + lastversionstatistics.Bounced);
								Console.WriteLine ("EmailTemplate Module Sent: " + lastversionstatistics.Sent);
								Console.WriteLine ("EmailTemplate Module Clicked: " + lastversionstatistics.Clicked);
							}
							Console.WriteLine ("EmailTemplate Type: " + emailTemplate.Type);
							MinifiedUser createdBy = emailTemplate.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("EmailTemplate Created By Name : " + createdBy.Name);
								Console.WriteLine ("EmailTemplate Created By id : " + createdBy.Id);
								Console.WriteLine ("EmailTemplate Created By Email : " + createdBy.Email);
							}
							Console.WriteLine ("EmailTemplate ModifiedTime: " + emailTemplate.ModifiedTime);
							Folder folder = emailTemplate.Folder;
							if (folder != null)
							{
								Console.WriteLine ("EmailTemplate Folder  id : " + folder.Id);
								Console.WriteLine ("EmailTemplate Folder  Name : " + folder.Name);
							}
							Console.WriteLine ("EmailTemplate Last Usage Time: " + emailTemplate.LastUsageTime);
							Console.WriteLine ("EmailTemplate Associated: " + emailTemplate.Associated);
							Console.WriteLine ("EmailTemplate Name: " + emailTemplate.Name);
							Console.WriteLine ("EmailTemplate Consent Linked: " + emailTemplate.ConsentLinked);
							MinifiedUser modifiedBy = emailTemplate.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("EmailTemplate Modified By Name : " + modifiedBy.Name);
								Console.WriteLine ("EmailTemplate Modified By id : " + modifiedBy.Id);
								Console.WriteLine ("EmailTemplate Modified By Email : " + modifiedBy.Email);
							}
							Console.WriteLine ("EmailTemplate ID: " + emailTemplate.Id);
							Console.WriteLine ("EmailTemplate Content: " + emailTemplate.Content);
							Console.WriteLine ("EmailTemplate Description: " + emailTemplate.Description);
							Console.WriteLine ("EmailTemplate EditorMode: " + emailTemplate.EditorMode);
							Console.WriteLine ("EmailTemplate Category: " + emailTemplate.Category);
							Console.WriteLine ("EmailTemplate Favourite: " + emailTemplate.Favorite);
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.PerPage != null)
							{
								Console.WriteLine ("Record Info PerPage: " + info.PerPage);
							}
							if (info.Count != null)
							{
								Console.WriteLine ("Record Info Count: " + info.Count);
							}
							if (info.Page != null)
							{
								Console.WriteLine ("Record Info Page: " + info.Page);
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
                GetEmailTemplates_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}