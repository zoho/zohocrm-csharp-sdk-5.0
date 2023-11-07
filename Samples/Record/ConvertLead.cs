using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Record.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;
using ConvertBodyWrapper = Com.Zoho.Crm.API.Record.ConvertBodyWrapper;
using LeadConverter = Com.Zoho.Crm.API.Record.LeadConverter;
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;
using MinifiedUser = Com.Zoho.Crm.API.Users.MinifiedUser;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Record
{
	public class ConvertLead
	{
		public static void ConvertLead_1(long leadId)
		{
			RecordOperations recordOperations = new RecordOperations();
			ConvertBodyWrapper request = new ConvertBodyWrapper();
			List<LeadConverter> data = new List<LeadConverter>();
			LeadConverter record1 = new LeadConverter();
			record1.Overwrite = true;
			record1.NotifyLeadOwner = true;
			record1.NotifyNewEntityOwner = true;
			Com.Zoho.Crm.API.Record.Record accounts =  new Com.Zoho.Crm.API.Record.Record();
			accounts.Id = 347706119024001;
			record1.Accounts = accounts;
			Com.Zoho.Crm.API.Record.Record contacts =  new Com.Zoho.Crm.API.Record.Record();
			contacts.Id = 347706119024004;
			record1.Contacts = contacts;
			MinifiedUser assignTo = new MinifiedUser();
			assignTo.Id = 34770615791024;
			record1.AssignTo = assignTo;
			Com.Zoho.Crm.API.Record.Record deals =  new Com.Zoho.Crm.API.Record.Record();
			/*
			 * Call addFieldValue method that takes two arguments 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list. 2 -> Value
			 */
			deals.AddFieldValue(Com.Zoho.Crm.API.Record.Deals.DEAL_NAME, "deal_name");
			deals.AddFieldValue(Com.Zoho.Crm.API.Record.Deals.DESCRIPTION, "deals description");
			deals.AddFieldValue(Com.Zoho.Crm.API.Record.Deals.CLOSING_DATE, new DateTime(2021, 1, 13).Date);
			deals.AddFieldValue(Com.Zoho.Crm.API.Record.Deals.STAGE, new Choice<string>("Closed Won"));
			deals.AddFieldValue(Com.Zoho.Crm.API.Record.Deals.AMOUNT, 50.7);
			/*
			 * Call addKeyValue method that takes two arguments 1 -> A string that is the Field's API Name 2 -> Value
			 */
			deals.AddKeyValue("Custom_field", "Value");
			deals.AddKeyValue("Pipeline", new Choice<string>("Qualification"));
			record1.Deals = deals;
			data.Add (record1);
			request.Data = data;
			APIResponse<ActionHandler> response = recordOperations.ConvertLead(leadId, request);
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
								Console.WriteLine ("Message: " + successResponse.Message.Value);
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
								Console.WriteLine ("Message: " + exception.Message.Value);
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
				long leadId = 34770616603276;
                ConvertLead_1(leadId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}