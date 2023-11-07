using System;
using System.Reflection;
using System.Collections.Generic;
using EmailTemplatesOperations = Com.Zoho.Crm.API.EmailTemplates.EmailTemplatesOperations;
using ResponseHandler = Com.Zoho.Crm.API.EmailTemplates.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.EmailTemplates.ResponseWrapper;
using Folder = Com.Zoho.Crm.API.InventoryTemplates.Folder;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.EmailTemplates.APIException;
using Attachment = Com.Zoho.Crm.API.EmailTemplates.Attachment;
using EmailTemplate = Com.Zoho.Crm.API.EmailTemplates.EmailTemplate;
using MinifiedModule = Com.Zoho.Crm.API.Modules.MinifiedModule;
using MinifiedUser = Com.Zoho.Crm.API.Users.MinifiedUser;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Emailtemplates
{
	public class GetEmailTemplateById
	{
		public static void GetEmailTemplateById_1(long emailTemplateID)
		{
			EmailTemplatesOperations emailTemplatesOperations = new EmailTemplatesOperations();
			APIResponse<ResponseHandler> response = emailTemplatesOperations.GetEmailTemplate(emailTemplateID);
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
							Console.WriteLine ("EmailTemplate Type: " + emailTemplate.Type);
							MinifiedUser createdBy = emailTemplate.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("EmailTemplate Created By Name : " + createdBy.Name);
								Console.WriteLine ("EmailTemplate Created By id : " + createdBy.Id);
								Console.WriteLine ("EmailTemplate Created By Name : " + createdBy.Email);
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
								Console.WriteLine ("EmailTemplate Modified By Name : " + modifiedBy.Email);
							}
							Console.WriteLine ("EmailTemplate ID: " + emailTemplate.Id);
							Console.WriteLine ("EmailTemplate : " + emailTemplate.EditorMode);
							Console.WriteLine ("EmailTemplate Content: " + emailTemplate.Content);
							Console.WriteLine ("EmailTemplate Description: " + emailTemplate.Description);
							Console.WriteLine ("EmailTemplate EditorMode: " + emailTemplate.EditorMode);
							Console.WriteLine ("EmailTemplate Favourite: " + emailTemplate.Favorite);
							Console.WriteLine ("EmailTemplate Subject: " + emailTemplate.Subject);
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
				long emailTemplateID = 4402480627040l;
                GetEmailTemplateById_1(emailTemplateID);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}