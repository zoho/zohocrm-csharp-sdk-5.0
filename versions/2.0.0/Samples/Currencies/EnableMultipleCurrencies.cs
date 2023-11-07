using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.Currencies.APIException;
using ActionHandler = Com.Zoho.Crm.API.Currencies.ActionHandler;
using BaseCurrencyActionResponse = Com.Zoho.Crm.API.Currencies.BaseCurrencyActionResponse;
using BaseCurrencyActionWrapper = Com.Zoho.Crm.API.Currencies.BaseCurrencyActionWrapper;
using BaseCurrencyWrapper = Com.Zoho.Crm.API.Currencies.BaseCurrencyWrapper;
using CurrenciesOperations = Com.Zoho.Crm.API.Currencies.CurrenciesOperations;
using Format = Com.Zoho.Crm.API.Currencies.Format;
using SuccessResponse = Com.Zoho.Crm.API.Currencies.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Currencies
{
	public class EnableMultipleCurrencies
	{
		public static void EnableMultipleCurrencies_1()
		{
			CurrenciesOperations currenciesOperations = new CurrenciesOperations();
			BaseCurrencyWrapper bodyWrapper = new BaseCurrencyWrapper();
			Com.Zoho.Crm.API.Currencies.BaseCurrency currency =  new Com.Zoho.Crm.API.Currencies.BaseCurrency();
			currency.PrefixSymbol = true;
			currency.Name = "Algerian Dinar - DZD";
			currency.IsoCode = "DZD";
			currency.Symbol = "DA";
			currency.ExchangeRate = "2.0";
			Format format = new Format();
			format.DecimalSeparator = new Choice<string>("Period");
			format.ThousandSeparator = new Choice<string>("Comma");
			format.DecimalPlaces = new Choice<string>("3");
			currency.Format = format;
			bodyWrapper.BaseCurrency = currency;
			APIResponse<ActionHandler> response = currenciesOperations.EnableCurrency(bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler baseCurrencyActionHandler = response.Object;
					if (baseCurrencyActionHandler is BaseCurrencyActionWrapper)
					{
						BaseCurrencyActionWrapper baseCurrencyActionWrapper = (BaseCurrencyActionWrapper) baseCurrencyActionHandler;
						BaseCurrencyActionResponse actionResponse = baseCurrencyActionWrapper.BaseCurrency;
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
							Console.WriteLine ("Message: " + successResponse.Message);
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
							Console.WriteLine ("Message: " + exception.Message);
						}
					}
					else if (baseCurrencyActionHandler is APIException)
					{
						APIException exception = (APIException) baseCurrencyActionHandler;
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
                EnableMultipleCurrencies_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}