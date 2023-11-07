using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.Currencies.APIException;
using BodyWrapper = Com.Zoho.Crm.API.Currencies.BodyWrapper;
using CurrenciesOperations = Com.Zoho.Crm.API.Currencies.CurrenciesOperations;
using CurrencyFormat = Com.Zoho.Crm.API.Currencies.CurrencyFormat;
using ResponseHandler = Com.Zoho.Crm.API.Currencies.ResponseHandler;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Currencies
{
	public class GetCurrency
	{
		public static void GetCurrency_1(long currencyId)
		{
			CurrenciesOperations currenciesOperations = new CurrenciesOperations();
			APIResponse<ResponseHandler> response = currenciesOperations.GetCurrency(currencyId);
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
					if (responseHandler is BodyWrapper)
					{
						BodyWrapper responseWrapper = (BodyWrapper) responseHandler;
						List<Com.Zoho.Crm.API.Currencies.Currency> currenciesList = responseWrapper.Currencies;
						foreach (Com.Zoho.Crm.API.Currencies.Currency currency in currenciesList)
						{
							Console.WriteLine ("Currency Symbol: " + currency.Symbol);
							Console.WriteLine ("Currency CreatedTime: " + currency.CreatedTime);
							Console.WriteLine ("Currency IsActive: " + currency.IsActive);
							Console.WriteLine ("Currency ExchangeRate: " + currency.ExchangeRate);
							CurrencyFormat format = currency.Format;
							if (format != null)
							{
								Console.WriteLine ("Currency Format DecimalSeparator: " + format.DecimalSeparator.Value);
								Console.WriteLine ("Currency Format ThousandSeparator: " + format.ThousandSeparator.Value);
								Console.WriteLine ("Currency Format DecimalPlaces: " + format.DecimalPlaces.Value);
							}
							Com.Zoho.Crm.API.Users.MinifiedUser createdBy =  currency.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("Currency CreatedBy User-Name: " + createdBy.Name);
								Console.WriteLine ("Currency CreatedBy User-ID: " + createdBy.Id);
							}
							Console.WriteLine ("Currency PrefixSymbol: " + currency.PrefixSymbol);
							Console.WriteLine ("Currency IsBase: " + currency.IsBase);
							Console.WriteLine ("Currency ModifiedTime: " + currency.ModifiedTime);
							Console.WriteLine ("Currency Name: " + currency.Name);
							Com.Zoho.Crm.API.Users.MinifiedUser modifiedBy =  currency.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("Currency ModifiedBy User-Name: " + modifiedBy.Name);
								Console.WriteLine ("Currency ModifiedBy User-ID: " + modifiedBy.Id);
							}
							Console.WriteLine ("Currency Id: " + currency.Id);
							Console.WriteLine ("Currency IsoCode: " + currency.IsoCode);
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
				long currencyId = 3477061005657003l;
                GetCurrency_1(currencyId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}