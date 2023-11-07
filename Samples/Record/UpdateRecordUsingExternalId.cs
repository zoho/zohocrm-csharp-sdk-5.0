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
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;
using UpdateRecordHeader = Com.Zoho.Crm.API.Record.RecordOperations.UpdateRecordHeader;
using Tag = Com.Zoho.Crm.API.Tags.Tag;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Record
{
	public class UpdateRecordUsingExternalId
	{
		public static void UpdateRecordUsingExternalId_1(string moduleAPIName, string externalFieldValue)
		{
			RecordOperations recordOperations = new RecordOperations();
			BodyWrapper request = new BodyWrapper();
			List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();
			Com.Zoho.Crm.API.Record.Record record1 =  new Com.Zoho.Crm.API.Record.Record();
			/*
			 * Call addFieldValue method that takes two arguments 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list. 2 -> Value
			 */
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.CITY, "City");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.LAST_NAME, "Last Name");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.FIRST_NAME, "First Name");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.LAST_NAME, "Last Name");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.COMPANY, "KKRNP");
			/*
			 * Call addKeyValue method that takes two arguments 1 -> A string that is the Field's API Name 2 -> Value
			 */
			record1.AddKeyValue("Custom_field", "Value");
			record1.AddKeyValue("Custom_field_2", "value");
			record1.AddKeyValue("Date_Time_2", new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local)));
			record1.AddKeyValue("Date_1", new DateTime(2017, 1, 13).Date);
			List<FileDetails> fileDetails = new List<FileDetails>();
			FileDetails fileDetail1 = new FileDetails();
			fileDetail1.FileIdS = "ae9c7cefa418a5c326ef21accd646c01e85c34b1b2e7fe45c";
			fileDetail1.Delete = null;
			fileDetails.Add (fileDetail1);
			FileDetails fileDetail2 = new FileDetails();
			fileDetail2.FileIdS = "ae9c7cefa418a5c326ef21accd646c01e85c34b1b2e7fe45c";
			fileDetail2.Delete = null;
			fileDetails.Add (fileDetail2);
			FileDetails fileDetail3 = new FileDetails();
			fileDetail3.FileIdS = "ae9c7cefa418a5c3256b4b32b984bad140a629d9f4d4fc8e2";
			fileDetail3.Delete = null;
			fileDetails.Add (fileDetail3);
			record1.AddKeyValue("File_Upload", fileDetails);
			Com.Zoho.Crm.API.Users.MinifiedUser recordOwner =  new Com.Zoho.Crm.API.Users.MinifiedUser();
			recordOwner.Email = "abc.d@zoho.com";
			record1.AddKeyValue("Owner", recordOwner);
			// Used when GDPR is enabled
			Consent dataConsent = new Consent();
			dataConsent.ConsentRemarks = "Approved.";
			dataConsent.ConsentThrough = "Email";
			dataConsent.ContactThroughEmail = true;
			dataConsent.ContactThroughSocial = false;
			record1.AddKeyValue("Data_Processing_Basis_Details", dataConsent);
			// Subform sample code
			List<Com.Zoho.Crm.API.Record.Record> subformList = new List<Com.Zoho.Crm.API.Record.Record>();
			Com.Zoho.Crm.API.Record.Record subform =  new Com.Zoho.Crm.API.Record.Record();
			subform.AddKeyValue("Subform FieldAPIName", "FieldValue");
			subformList.Add (subform);
			record1.AddKeyValue("Subform Name", subformList);
			/** Following methods are being used only by Inventory modules */
			Com.Zoho.Crm.API.Record.Record dealName =  new Com.Zoho.Crm.API.Record.Record();
			dealName.AddFieldValue(Com.Zoho.Crm.API.Record.Deals.ID, 34770614995070l);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Sales_Orders.DEAL_NAME, dealName);
			Com.Zoho.Crm.API.Record.Record contactName =  new Com.Zoho.Crm.API.Record.Record();
			contactName.AddFieldValue(Com.Zoho.Crm.API.Record.Contacts.ID, 34770614977055l);
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Purchase_Orders.CONTACT_NAME, contactName);
			Com.Zoho.Crm.API.Record.Record accountName =  new Com.Zoho.Crm.API.Record.Record();
			accountName.AddKeyValue("name", "automatedAccount");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Quotes.ACCOUNT_NAME, accountName);
			List<Com.Zoho.Crm.API.Record.Record> inventoryLineItemList = new List<Com.Zoho.Crm.API.Record.Record>();
			Com.Zoho.Crm.API.Record.Record inventoryLineItem =  new Com.Zoho.Crm.API.Record.Record();
			LineItemProduct lineItemProduct = new LineItemProduct();
			lineItemProduct.Id = 34770615356009L;
			inventoryLineItem.AddKeyValue("Description", "asd");
			inventoryLineItem.AddKeyValue("Discount", "5");
			Com.Zoho.Crm.API.Record.Record parentId =  new Com.Zoho.Crm.API.Record.Record();
			parentId.Id = 35240337331017L;
	//		inventoryLineItem.AddKeyValue("Parent_Id", 5);
			inventoryLineItem.AddKeyValue("Sequence_Number", 1L);
			LineItemProduct lineitemProduct = new LineItemProduct();
			lineitemProduct.Id = 35240333659082L;
			inventoryLineItem.AddKeyValue("Product_Name", lineItemProduct);
			inventoryLineItem.AddKeyValue("Sequence_Number", 1l);
			inventoryLineItem.AddKeyValue("Quantity", 123.2);
			inventoryLineItem.AddKeyValue("Tax", 123.2);
			inventoryLineItemList.Add (inventoryLineItem);
			List<LineTax> productLineTaxes = new List<LineTax>();
			LineTax productLineTax = new LineTax();
			productLineTax.Name = "MyT2ax1134";
			productLineTax.Percentage = 20.0;
			productLineTaxes.Add (productLineTax);
			inventoryLineItem.AddKeyValue("line_tax", productLineTaxes);
			inventoryLineItemList.Add (inventoryLineItem);
			record1.AddKeyValue("Quoted_Items", inventoryLineItemList);
			List<LineTax> lineTaxes = new List<LineTax>();
			LineTax lineTax = new LineTax();
			lineTax.Name = "MyT2ax1134";
			lineTax.Percentage = 20.0;
			lineTaxes.Add (lineTax);
			record1.AddKeyValue("$line_tax", lineTaxes);
			/** End Inventory **/
			List<Tag> tagList = new List<Tag>();
			Tag tag = new Tag();
			tag.Name = "Testtask1";
			tagList.Add (tag);
			record1.Tag = tagList;
			// Add Record instance to the list
			records.Add (record1);
			request.Data = records;
			List<string> trigger = new List<string>();
			trigger.Add ("approval");
			trigger.Add ("workflow");
			trigger.Add ("blueprint");
			request.Trigger = trigger;
			HeaderMap headerInstance = new HeaderMap();
			headerInstance.Add (UpdateRecordHeader.X_EXTERNAL, "Leads.External");
			APIResponse<ActionHandler> response = recordOperations.UpdateRecordUsingExternalId(externalFieldValue, moduleAPIName, request, headerInstance);
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
				string externalFieldValue = "TestExternalLead11";
                UpdateRecordUsingExternalId_1(moduleAPIName, externalFieldValue);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}