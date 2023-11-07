using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using HeaderMap = Com.Zoho.Crm.API.HeaderMap;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Record.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Record.BodyWrapper;
using Consent = Com.Zoho.Crm.API.Record.Consent;
using FileDetails = Com.Zoho.Crm.API.Record.FileDetails;
using LineItemProduct = Com.Zoho.Crm.API.Record.LineItemProduct;
using LineTax = Com.Zoho.Crm.API.Record.LineTax;
using Participants = Com.Zoho.Crm.API.Record.Participants;
using PricingDetails = Com.Zoho.Crm.API.Record.PricingDetails;
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using RecurringActivity = Com.Zoho.Crm.API.Record.RecurringActivity;
using RemindAt = Com.Zoho.Crm.API.Record.RemindAt;
using Reminder = Com.Zoho.Crm.API.Record.Reminder;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;
using Tag = Com.Zoho.Crm.API.Tags.Tag;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Record
{
	public class CreateRecords1
	{
		public static void CreateRecords_1(string moduleAPIName)
		{
			RecordOperations recordOperations = new RecordOperations();
			BodyWrapper bodyWrapper = new BodyWrapper();
			List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();
			Com.Zoho.Crm.API.Record.Record record1 =  new Com.Zoho.Crm.API.Record.Record();
			/*
			 * Call addFieldValue method that takes two arguments 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list. 2 -> Value
			 */
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.LAST_NAME, "Last Name");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.FIRST_NAME, "First Name");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.COMPANY, "Company Name");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.ANNUAL_REVENUE, 1221.2);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.LEAD_STATUS, new Choice<string>("Not Contacted"));
			/** Following methods are being used only by Inventory modules */
			Com.Zoho.Crm.API.Record.Record dealName =  new Com.Zoho.Crm.API.Record.Record();
			dealName.AddFieldValue(Com.Zoho.Crm.API.Record.Deals.ID, 347706111383007l);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Sales_Orders.DEAL_NAME, dealName);
			Com.Zoho.Crm.API.Record.Record contactName =  new Com.Zoho.Crm.API.Record.Record();
			contactName.AddFieldValue(Com.Zoho.Crm.API.Record.Contacts.ID, 347706111853001l);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Purchase_Orders.CONTACT_NAME, contactName);
			Com.Zoho.Crm.API.Record.Record accountName =  new Com.Zoho.Crm.API.Record.Record();
			accountName.AddKeyValue("name", "automatedAccount");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Quotes.ACCOUNT_NAME, accountName);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Sales_Orders.ACCOUNT_NAME, null);
			List<Com.Zoho.Crm.API.Record.Record> inventoryLineItemList = new List<Com.Zoho.Crm.API.Record.Record>();
			Com.Zoho.Crm.API.Record.Record inventoryLineItem =  new Com.Zoho.Crm.API.Record.Record();
			LineItemProduct lineItemProduct = new LineItemProduct();
			lineItemProduct.Id = 440248954344L;
			lineItemProduct.AddKeyValue("Products_External", "ProductExternal");
			inventoryLineItem.AddKeyValue("Description", "asd");
			inventoryLineItem.AddKeyValue("Discount", "5");
			record1.AddKeyValue("Singleline", "customField");
			inventoryLineItem.AddKeyValue("Desc", "customField");
			Com.Zoho.Crm.API.Record.Record parentId =  new Com.Zoho.Crm.API.Record.Record();
			parentId.Id = 35240337331017L;
			inventoryLineItem.AddKeyValue("Parent_Id", parentId);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Quotes.SUBJECT, "new Quote");
			LineItemProduct lineitemProduct = new LineItemProduct();
			lineitemProduct.Id = 440248954344L;
			inventoryLineItem.AddKeyValue("Product_Name", lineitemProduct);
			inventoryLineItem.AddKeyValue("Sequence_Number", 2l);
			inventoryLineItem.AddKeyValue("Quantity", 123.2);
			inventoryLineItem.AddKeyValue("Tax", 123.2);
			inventoryLineItemList.Add (inventoryLineItem);
			List<LineTax> productLineTaxes = new List<LineTax>();
			LineTax productLineTax = new LineTax();
			productLineTax.Name = "MyTax1134";
			productLineTax.Value = 0.0;
			productLineTax.Id = 347706110743003l;
			productLineTax.Percentage = 15.0;
			productLineTaxes.Add (productLineTax);
			inventoryLineItem.AddKeyValue("Line_Tax", productLineTaxes);
			inventoryLineItemList.Add (inventoryLineItem);
			record1.AddKeyValue("Quoted_Items", inventoryLineItemList);
			List<LineTax> lineTaxes = new List<LineTax>();
			LineTax lineTax = new LineTax();
			lineTax.Name = "MyTax1134";
			lineTax.Percentage = 20.0;
			lineTaxes.Add (lineTax);
			record1.AddKeyValue("$line_tax", lineTaxes);
			/** End Inventory **/
			/** Following methods are being used only by Activity modules */
			//Tasks
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Tasks.DESCRIPTION, "Test Task");
			record1.AddKeyValue("Currency", new Choice<string>("INR"));
			RemindAt remindAt = new RemindAt();
			remindAt.Alarm = "ACTION=EMAILANDPOPUP;TRIGGER=DATE-TIME:2020-07-03T12:30:00+05:30";
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Tasks.REMIND_AT, remindAt);
			Com.Zoho.Crm.API.Record.Record whoId =  new Com.Zoho.Crm.API.Record.Record();
			whoId.Id = 34770614977055L;
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Tasks.WHO_ID, whoId);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Tasks.STATUS, new Choice<string>("Waiting on someone else"));
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Tasks.DUE_DATE, new DateTime(2021, 1, 13).Date);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Tasks.PRIORITY, new Choice<string>("High"));
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Tasks.SUBJECT, "Email1");
			record1.AddKeyValue("$se_module", "Accounts");
			Com.Zoho.Crm.API.Record.Record whatId =  new Com.Zoho.Crm.API.Record.Record();
			whatId.Id = 3477061207118L;
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Tasks.WHAT_ID, whatId);
			/** Recurring Activity can be provided in any activity module */
			RecurringActivity recurringActivity = new RecurringActivity();
			recurringActivity.Rrule = "FREQ=DAILY;INTERVAL=10;UNTIL=2023-08-14;DTSTART=2020-07-03";
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Events.RECURRING_ACTIVITY, recurringActivity);
			//
			// Events
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Events.DESCRIPTION, "Test Events");
			DateTimeOffset startDateTime = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Events.START_DATETIME, startDateTime);
			List<Participants> participants = new List<Participants>();
			Participants participant1 = new Participants();
			participant1.Email = "abc@zoho.com";
			participant1.Type = "email";
			participant1.Id = 34770615902017L;
			participants.Add (participant1);
			Participants participant2 = new Participants();
			participant2.AddKeyValue("participant", "34770617425");
			participant2.AddKeyValue("type", "lead");
			participants.Add (participant2);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Events.PARTICIPANTS, participants);
			record1.AddKeyValue("$send_notification", true);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Events.EVENT_TITLE, "New Automated Event");
			DateTimeOffset enddatetime = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Events.END_DATETIME, enddatetime);
			List<Reminder> reminderList = new List<Reminder>();
			Reminder remindAt1 = new Reminder();
			remindAt1.Period = "minutes";
			remindAt1.Unit = 15;
			reminderList.Add (remindAt1);
			remindAt1 = new Reminder();
			remindAt1.Period = "days";
			remindAt1.Unit = 1;
			remindAt1.Time = "10.30";
			reminderList.Add (remindAt1);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Events.REMIND_AT, reminderList);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Events.CHECK_IN_STATUS, "PLANNED");
			record1.AddKeyValue("$se_module", "Leads");
			Com.Zoho.Crm.API.Record.Record whatId1 =  new Com.Zoho.Crm.API.Record.Record();
			whatId1.Id = 34770614381002L;
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Events.WHAT_ID, whatId1);
			record1.AddKeyValue("Currency", new Choice<string>("USD"));
			/** End Activity **/
			/** Following methods are being used only by Price_Books modules */
			List<PricingDetails> pricingDetails = new List<PricingDetails>();
			PricingDetails pricingDetail1 = new PricingDetails();
			pricingDetail1.FromRange = 1.0;
			pricingDetail1.ToRange = 5.0;
			pricingDetail1.Discount = 2.0;
			pricingDetails.Add (pricingDetail1);
			PricingDetails pricingDetail2 = new PricingDetails();
			pricingDetail2.AddKeyValue("from_range", 6.0);
			pricingDetail2.AddKeyValue("to_range", 11.0);
			pricingDetail2.AddKeyValue("discount", 3.0);
			pricingDetails.Add (pricingDetail2);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Price_Books.PRICING_DETAILS, pricingDetails);
			record1.AddKeyValue("Email", "abc.d@zoho.com");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Price_Books.DESCRIPTION, "TEST");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Price_Books.PRICE_BOOK_NAME, "book_name");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Price_Books.PRICING_MODEL, new Choice<string>("Flat"));
			
			// Used when GDPR is enabled
			Consent dataConsent = new Consent();
			dataConsent.ConsentRemarks = "Approved.";
			dataConsent.ConsentThrough = "Email";
			dataConsent.ContactThroughEmail = true;
			dataConsent.ContactThroughSocial = false;
			dataConsent.ContactThroughPhone = true;
			dataConsent.ContactThroughSurvey = true;
			dataConsent.ConsentDate = new DateTime(2023, 10, 11).Date;
			dataConsent.DataProcessingBasis = "Obtained";
			record1.AddKeyValue("Data_Processing_Basis_Details", dataConsent);
			/*
			 * Call addKeyValue method that takes two arguments 1 -> A string that is the Field's API Name 2 -> Value
			 */
			record1.AddKeyValue("External", "Value12345");
			record1.AddKeyValue("Custom_field", "Value");
			record1.AddKeyValue("Date_Time_2", new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local)));
			record1.AddKeyValue("Date_1", new DateTime(2021, 1, 13).Date);
			record1.AddKeyValue("Subject", "AutomatedSDK");
			record1.AddKeyValue("Product_Name", "AutomatedSDK");
			List<FileDetails> fileDetails = new List<FileDetails>();
			FileDetails fileDetail1 = new FileDetails();
			fileDetail1.FileIdS = "ae9c7cefa418aec1d6a5cc2d9ab35c32a6ae23bd44183d280";
			fileDetails.Add (fileDetail1);
			FileDetails fileDetail2 = new FileDetails();
			fileDetail2.FileIdS = "ae9c7cefa418aec1d6a5cc27321b5b4ca878a934519e6cdb2";
			fileDetails.Add (fileDetail2);
			record1.AddKeyValue("File_Upload", fileDetails);
			// for Custom User LookUp
			Com.Zoho.Crm.API.Users.MinifiedUser user =  new Com.Zoho.Crm.API.Users.MinifiedUser();
			user.Id = 440248254001L;
			record1.AddKeyValue("User_1", user);
			record1.AddKeyValue("new", DateTimeOffset.Now);
			// for Custom LookUp
			Com.Zoho.Crm.API.Record.Record data =  new Com.Zoho.Crm.API.Record.Record();
			data.Id = 440248774074L;
			record1.AddKeyValue("Lookup_2", data);
			// for Custom pickList
			record1.AddKeyValue("pick", new Choice<string>("true"));
			// for Custom MultiSelect
			record1.AddKeyValue("Multiselect", new List<Choice<string>>() {new Choice<string>("Option 1"), new Choice<string>("Option 2") });
			// Subform sample code
			List<Com.Zoho.Crm.API.Record.Record> subformList = new List<Com.Zoho.Crm.API.Record.Record>();
			Com.Zoho.Crm.API.Record.Record subform =  new Com.Zoho.Crm.API.Record.Record();
			subform.AddKeyValue("customfield", "customvalue");
            subform.AddKeyValue("_delete", true);
            Com.Zoho.Crm.API.Users.MinifiedUser user1 =  new Com.Zoho.Crm.API.Users.MinifiedUser();
			user1.Id = 440248254001L;
			subform.AddKeyValue("Userfield", user1);
			subformList.Add (subform);
			record1.AddKeyValue("Subform_2", subformList);
			// sample code for MultiSelectLookUp Field
			List<Com.Zoho.Crm.API.Record.Record> Multirecords = new List<Com.Zoho.Crm.API.Record.Record>();
			Com.Zoho.Crm.API.Record.Record record =  new Com.Zoho.Crm.API.Record.Record();
			Com.Zoho.Crm.API.Record.Record linkingRecord =  new Com.Zoho.Crm.API.Record.Record();
			record.AddKeyValue("id", 440248884001L);
			linkingRecord.AddKeyValue("Msl", record);
			Multirecords.Add (linkingRecord);
			record1.AddKeyValue("Msl", Multirecords);
			List<Tag> tagList = new List<Tag>();
			Tag tag = new Tag();
			tag.Name = "Testtask";
			tagList.Add (tag);
			record1.Tag = tagList;
			// Add Record instance to the list
			records.Add (record1);
			bodyWrapper.Data = records;
			List<string> trigger = new List<string>();
			trigger.Add ("approval");
			trigger.Add ("workflow");
			trigger.Add ("blueprint");
			bodyWrapper.Trigger = trigger;
			 bodyWrapper.LarId = "3477061087515";
			HeaderMap headerInstance = new HeaderMap();
			//headerInstance.Add (CreateRecordsHeader.X_EXTERNAL, "Quotes.Quoted_Items.Product_Name.Products_External");
			APIResponse<ActionHandler> response = recordOperations.CreateRecords(moduleAPIName, bodyWrapper, headerInstance);
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
				string moduleAPIName = "Leads";
                CreateRecords_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}