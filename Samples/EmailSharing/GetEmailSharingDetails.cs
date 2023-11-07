using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.EmailSharing.APIException;
using EmailSharingOperations = Com.Zoho.Crm.API.EmailSharing.EmailSharingOperations;
using GetEmailSharing = Com.Zoho.Crm.API.EmailSharing.GetEmailSharing;
using ResponseHandler = Com.Zoho.Crm.API.EmailSharing.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.EmailSharing.ResponseWrapper;
using ShareFromUser = Com.Zoho.Crm.API.EmailSharing.ShareFromUser;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Emailsharing
{
	public class GetEmailSharingDetails
	{
		public static void GetEmailSharingDetails_1(long recordId, string module)
		{
			EmailSharingOperations emailsharingoperations = new EmailSharingOperations(recordId, module);
			APIResponse<ResponseHandler> response = emailsharingoperations.GetEmailSharingDetails();
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
						List<GetEmailSharing> emailSharingdetails = responseWrapper.Emailssharingdetails;
						if (emailSharingdetails != null)
						{
							foreach (GetEmailSharing getemailsharing in emailSharingdetails)
							{
								Console.WriteLine ("Email_Sharing_Details : ");
								List<ShareFromUser> sharefromUsers = getemailsharing.ShareFromUsers;
								if (sharefromUsers != null)
								{
									Console.WriteLine ("ShareFromUsers : ");
									foreach (ShareFromUser sharefromuser in sharefromUsers)
									{
										Console.WriteLine ("id : " + sharefromuser.Id);
										Console.WriteLine ("name : " + sharefromuser.Name);
										Console.WriteLine ("type : " + sharefromuser.Type);
									}
								}
								List<string> availableTypes = getemailsharing.AvailableTypes;
								if (availableTypes != null)
								{
									Console.WriteLine ("AvailableTypes : ");
									foreach (string availabletype in availableTypes)
									{
										Console.WriteLine (availabletype);
									}
								}
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
				string module = "Leads";
				long recordId = 4402480774074l;
                GetEmailSharingDetails_1(recordId, module);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}