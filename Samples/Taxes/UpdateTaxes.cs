using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Taxes.APIException;
using ActionHandler = Com.Zoho.Crm.API.Taxes.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Taxes.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Taxes.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Taxes.BodyWrapper;
using OrgTax = Com.Zoho.Crm.API.Taxes.OrgTax;
using Preference = Com.Zoho.Crm.API.Taxes.Preference;
using SuccessResponse = Com.Zoho.Crm.API.Taxes.SuccessResponse;
using TaxesOperations = Com.Zoho.Crm.API.Taxes.TaxesOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Taxes
{
	public class UpdateTaxes
	{
		public static void UpdateTaxes_1()
		{
			TaxesOperations taxesOperations = new TaxesOperations();
			BodyWrapper request = new BodyWrapper();
			OrgTax orgTax = new OrgTax();
			List<Com.Zoho.Crm.API.Taxes.Tax> taxList = new List<Com.Zoho.Crm.API.Taxes.Tax>();
			Com.Zoho.Crm.API.Taxes.Tax tax1 =  new Com.Zoho.Crm.API.Taxes.Tax();
			tax1.Id = 3477061002;
			tax1.Name = "MyTax1134313223";
			tax1.SequenceNumber = 1;
			tax1.Value = 15.04;
	//		tax1.Delete = null;
			taxList.Add (tax1);
			orgTax.Taxes = taxList;
			Preference preference = new Preference();
			preference.AutoPopulateTax = false;
			preference.ModifyTaxRates = false;
			orgTax.Preference = preference;
			request.OrgTaxes = orgTax;
			APIResponse<ActionHandler> response = taxesOperations.UpdateTaxes(request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						ActionResponse actionResponse = actionWrapper.OrgTaxes;
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
                UpdateTaxes_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}