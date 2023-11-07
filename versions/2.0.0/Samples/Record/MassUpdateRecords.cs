using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using MassUpdateActionHandler = Com.Zoho.Crm.API.Record.MassUpdateActionHandler;
using MassUpdateActionResponse = Com.Zoho.Crm.API.Record.MassUpdateActionResponse;
using MassUpdateActionWrapper = Com.Zoho.Crm.API.Record.MassUpdateActionWrapper;
using MassUpdateBodyWrapper = Com.Zoho.Crm.API.Record.MassUpdateBodyWrapper;
using MassUpdateSuccessResponse = Com.Zoho.Crm.API.Record.MassUpdateSuccessResponse;
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Record
{
	public class MassUpdateRecords
	{
		public static void MassUpdateRecords_1(string moduleAPIName)
		{
			RecordOperations recordOperations = new RecordOperations();
			MassUpdateBodyWrapper request = new MassUpdateBodyWrapper();
			List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();
			Com.Zoho.Crm.API.Record.Record record1 =  new Com.Zoho.Crm.API.Record.Record();
			/*
			 * Call addKeyValue method that takes two arguments 1 -> A string that is the Field's API Name 2 -> Value
			 */
			record1.AddKeyValue("City", "Value");
	//		record1.AddKeyValue("Company", "Value");
			records.Add (record1);
			request.Data = records;
			request.Cvid = "347629003";
			List<string> ids = new List<string>() { "347767008" } ;
			request.Ids = ids;
	//		Territory territory = new Territory();
	//		territory.Id = "";
	//		territory.IncludeChild = true;
	//		request.Territory = territory;
			request.OverWrite = true;
			APIResponse<MassUpdateActionHandler> response = recordOperations.MassUpdateRecords(moduleAPIName, request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					MassUpdateActionHandler massUpdateActionHandler = response.Object;
					if (massUpdateActionHandler is MassUpdateActionWrapper)
					{
						MassUpdateActionWrapper massUpdateActionWrapper = (MassUpdateActionWrapper) massUpdateActionHandler;
						List<MassUpdateActionResponse> massUpdateActionResponses = massUpdateActionWrapper.Data;
						foreach (MassUpdateActionResponse massUpdateActionResponse in massUpdateActionResponses)
						{
							if (massUpdateActionResponse is MassUpdateSuccessResponse)
							{
								MassUpdateSuccessResponse massUpdateSuccessResponse = (MassUpdateSuccessResponse) massUpdateActionResponse;
								Console.WriteLine ("Status: " + massUpdateSuccessResponse.Status.Value);
								Console.WriteLine ("Code: " + massUpdateSuccessResponse.Code.Value);
								Console.WriteLine ("Details: ");
								foreach (KeyValuePair<string, object> entry in massUpdateSuccessResponse.Details)
								{
									Console.WriteLine (entry.Key + ": " + entry.Value);
								}
								Console.WriteLine ("Message: " + massUpdateSuccessResponse.Message.Value);
							}
							else if (massUpdateActionResponse is APIException)
							{
								APIException exception = (APIException) massUpdateActionResponse;
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
					else if (massUpdateActionHandler is APIException)
					{
						APIException exception = (APIException) massUpdateActionHandler;
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
                MassUpdateRecords_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}