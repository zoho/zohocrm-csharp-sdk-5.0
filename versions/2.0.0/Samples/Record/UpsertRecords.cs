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
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Record
{
	public class UpsertRecords
	{
		public static void UpsertRecords_1(string moduleAPIName)
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
			record1.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.COMPANY, "Company1");
			/*
			 * Call addKeyValue method that takes two arguments 1 -> A string that is the Field's API Name 2 -> Value
			 */
			record1.AddKeyValue("Custom_field", "Value");
			record1.AddKeyValue("Custom_field_2", "value");
			// Add Record instance to the list
			records.Add (record1);
			Com.Zoho.Crm.API.Record.Record record2 =  new Com.Zoho.Crm.API.Record.Record();
			/*
			 * Call addFieldValue method that takes two arguments 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list. 2 -> Value
			 */
			record2.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.CITY, "City");
			record2.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.LAST_NAME, "Last Name");
			record2.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.FIRST_NAME, "First Name");
			record2.AddFieldValue(Com.Zoho.Crm.API.Record.Leads.COMPANY, "Company12");
			/*
			 * Call addKeyValue method that takes two arguments 1 -> A string that is the Field's API Name 2 -> Value
			 */
			record2.AddKeyValue("Custom_field", "Value");
			record2.AddKeyValue("Custom_field_2", "value");
			// Add Record instance to the list
			records.Add (record2);
			List<string> duplicateCheckFields = new List<string>() {"City", "First_Name" } ;
			request.DuplicateCheckFields = duplicateCheckFields;
			request.Data = records;
			HeaderMap headerInstance = new HeaderMap();
	//		headerInstance.Add (UpsertRecordsHeader.X_EXTERNAL, "Leads.External");
			APIResponse<ActionHandler> response = recordOperations.UpsertRecords(moduleAPIName, request, headerInstance);
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
                UpsertRecords_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}