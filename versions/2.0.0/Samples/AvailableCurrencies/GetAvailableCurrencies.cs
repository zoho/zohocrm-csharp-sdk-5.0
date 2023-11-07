using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.AvailableCurrencies.APIException;
using AvailableCurrenciesOperations = Com.Zoho.Crm.API.AvailableCurrencies.AvailableCurrenciesOperations;
using ResponseHandler = Com.Zoho.Crm.API.AvailableCurrencies.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.AvailableCurrencies.ResponseWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Availablecurrencies
{
	public class GetAvailableCurrencies
	{
		public static void GetAvailableCurrencies_1()
		{
			AvailableCurrenciesOperations currenciesOperations = new AvailableCurrenciesOperations();
			APIResponse<ResponseHandler> response = currenciesOperations.GetAvailableCurrencies();
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
						List<Com.Zoho.Crm.API.AvailableCurrencies.Currency> currenciesList = responseWrapper.AvailableCurrencies;
						foreach (Com.Zoho.Crm.API.AvailableCurrencies.Currency currency in  currenciesList)
						{
							Console.WriteLine ("Currency DisplayValue: " + currency.DisplayValue);
							Console.WriteLine ("Currency DecimalSeparator: " + currency.DecimalSeparator);
							Console.WriteLine ("Currency Symbol: " + currency.Symbol);
							Console.WriteLine ("Currency ThousandSeparator: " + currency.ThousandSeparator);
							Console.WriteLine ("Currency IsoCode: " + currency.IsoCode);
							Console.WriteLine ("Currency DisplayName: " + currency.DisplayName);
							Console.WriteLine ("Currency DecimalPlaces: " + currency.DecimalPlaces);
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
                GetAvailableCurrencies_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}