using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Taxes.APIException;
using OrgTax = Com.Zoho.Crm.API.Taxes.OrgTax;
using Preference = Com.Zoho.Crm.API.Taxes.Preference;
using ResponseHandler = Com.Zoho.Crm.API.Taxes.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Taxes.ResponseWrapper;
using TaxesOperations = Com.Zoho.Crm.API.Taxes.TaxesOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Taxes
{
	public class GetTaxes
	{
		public static void GetTaxes_1()
		{
			TaxesOperations taxesOperations = new TaxesOperations();
			APIResponse<ResponseHandler> response = taxesOperations.GetTaxes();
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
						OrgTax orgTax = responseWrapper.OrgTaxes;
						List<Com.Zoho.Crm.API.Taxes.Tax> taxes = orgTax.Taxes;
						foreach (Com.Zoho.Crm.API.Taxes.Tax tax in taxes)
						{
							Console.WriteLine ("Tax DisplayLabel: " + tax.DisplayLabel);
							Console.WriteLine ("Tax Name: " + tax.Name);
							Console.WriteLine ("Tax ID: " + tax.Id);
							Console.WriteLine ("Tax Value: " + tax.Value);
						}
						Preference preference = orgTax.Preference;
						if (preference != null)
						{
							Console.WriteLine ("Preference AutoPopulateTax: " + preference.AutoPopulateTax);
							if (preference.ModifyTaxRates != null)
							{
								Console.WriteLine ("Preference ModifyTaxRates: " + preference.ModifyTaxRates);
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
				Environment environment = INDataCenter.PRODUCTION;
				IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL" ).Build();
				new Initializer.Builder().Environment(environment).Token(token).Initialize();
                GetTaxes_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}