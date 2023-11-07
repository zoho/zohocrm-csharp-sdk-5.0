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
using LineItemProduct = Com.Zoho.Crm.API.Record.LineItemProduct;
using LineTax = Com.Zoho.Crm.API.Record.LineTax;
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Record
{
	public class UpdateRecords
	{
		public static void UpdateRecords_1(string moduleAPIName)
		{
			RecordOperations recordOperations = new RecordOperations();
			BodyWrapper request = new BodyWrapper();
			List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();
			Com.Zoho.Crm.API.Record.Record record1 =  new Com.Zoho.Crm.API.Record.Record();
			record1.Id = 347706112081001l;
			/*
			 * Call addFieldValue method that takes two arguments 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list. 2 -> Value
			 */
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.CITY, "City");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.LAST_NAME, "Last Name");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.FIRST_NAME, "First Name");
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.COMPANY, "KKRNP");
			/*
			 * Call addKeyValue method that takes two arguments 1 -> A string that is the Field's API Name 2 -> Value
			 */
			record1.AddKeyValue("Custom_field", "Value");
			record1.AddKeyValue("Custom_field_2", "value");
			// Used when GDPR is enabled
			Consent dataConsent = new Consent();
			dataConsent.ConsentRemarks = "Approved.";
			dataConsent.ConsentThrough = "Email";
			dataConsent.ContactThroughEmail = true;
			dataConsent.ContactThroughSocial = false;
			record1.AddKeyValue("Data_Processing_Basis_Details", dataConsent);
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
			List<Com.Zoho.Crm.API.Record.Record> inventoryLineItemList = new List<Com.Zoho.Crm.API.Record.Record>();
			Com.Zoho.Crm.API.Record.Record inventoryLineItem =  new Com.Zoho.Crm.API.Record.Record();
			LineItemProduct lineItemProduct = new LineItemProduct();
			lineItemProduct.Id = 347706111217004l;
	//		lineItemProduct.AddKeyValue("Products_External", "TestExternalLead121");
			inventoryLineItem.AddKeyValue("Description", "asd");
			inventoryLineItem.AddKeyValue("Discount", "5");
			Com.Zoho.Crm.API.Record.Record parentId =  new Com.Zoho.Crm.API.Record.Record();
			parentId.Id = 35240337331017L;
			inventoryLineItem.AddKeyValue("Parent_Id", parentId);
			LineItemProduct lineitemProduct = new LineItemProduct();
			lineitemProduct.Id = 35240333659082L;
			inventoryLineItem.AddKeyValue("Product_Name", lineItemProduct);
			inventoryLineItem.AddKeyValue("Sequence_Number", 1l);
			inventoryLineItem.AddKeyValue("Quantity", 123.2);
			inventoryLineItem.AddKeyValue("Tax", 123.2);
			inventoryLineItemList.Add (inventoryLineItem);
			List<LineTax> productLineTaxes = new List<LineTax>();
			LineTax productLineTax = new LineTax();
			productLineTax.Name = "MyTax1134";
			productLineTax.Percentage = 20.0;
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
			// Add Record instance to the list
			records.Add (record1);
			Com.Zoho.Crm.API.Record.Record record2 =  new Com.Zoho.Crm.API.Record.Record();
			record2.Id = 34770615844005l;
			/*
			 * Call addFieldValue method that takes two arguments 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list. 2 -> Value
			 */
			record2.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.CITY, "City");
			record2.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.LAST_NAME, "Last Name");
			record2.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.FIRST_NAME, "First Name");
			record2.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.COMPANY, "KKRNP");
			/*
			 * Call addKeyValue method that takes two arguments 1 -> A string that is the Field's API Name 2 -> Value
			 */
			record2.AddKeyValue("Custom_field", "Value");
			record2.AddKeyValue("Custom_field_2", "value");
			records.Add (record2);
			request.Data = records;
			List<string> trigger = new List<string>();
			trigger.Add ("approval");
			trigger.Add ("workflow");
			trigger.Add ("blueprint");
			request.Trigger = trigger;
			HeaderMap headerInstance = new HeaderMap();
	//		headerInstance.Add (UpdateRecordsHeader.X_EXTERNAL, "Quotes.Quoted_Items.Product_Name.Products_External");
			APIResponse<ActionHandler> response = recordOperations.UpdateRecords(moduleAPIName, request, headerInstance);
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
                UpdateRecords_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}