using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.Currencies.APIException;
using ActionHandler = Com.Zoho.Crm.API.Currencies.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Currencies.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Currencies.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Currencies.BodyWrapper;
using CurrenciesOperations = Com.Zoho.Crm.API.Currencies.CurrenciesOperations;
using CurrencyFormat = Com.Zoho.Crm.API.Currencies.CurrencyFormat;
using SuccessResponse = Com.Zoho.Crm.API.Currencies.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Currencies
{
	public class UpdateCurrencies
	{
		public static void UpdateCurrencies_1()
		{
			CurrenciesOperations currenciesOperations = new CurrenciesOperations();
			BodyWrapper bodyWrapper = new BodyWrapper();
			List<Com.Zoho.Crm.API.Currencies.Currency> currencies = new List<Com.Zoho.Crm.API.Currencies.Currency>();
			Com.Zoho.Crm.API.Currencies.Currency currency =  new Com.Zoho.Crm.API.Currencies.Currency();
	//		currency.PrefixSymbol = true;
			currency.Id = 34770617368016l;
			currency.ExchangeRate = "5.0";
			currency.IsActive = true;
			CurrencyFormat format = new CurrencyFormat();
			format.DecimalSeparator = new Choice<string>("Period");
			format.ThousandSeparator = new Choice<string>("Comma");
			format.DecimalPlaces = new Choice<string>("2");
			currency.Format = format;
			currencies.Add (currency);
			bodyWrapper.Currencies = currencies;
			APIResponse<ActionHandler> response = currenciesOperations.UpdateCurrencies(bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Currencies;
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
                UpdateCurrencies_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}