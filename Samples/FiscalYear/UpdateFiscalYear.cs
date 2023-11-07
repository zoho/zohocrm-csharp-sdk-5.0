using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.FiscalYear.APIException;
using ActionHandler = Com.Zoho.Crm.API.FiscalYear.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.FiscalYear.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.FiscalYear.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.FiscalYear.BodyWrapper;
using FiscalYearOperations = Com.Zoho.Crm.API.FiscalYear.FiscalYearOperations;
using SuccessResponse = Com.Zoho.Crm.API.FiscalYear.SuccessResponse;
using Year = Com.Zoho.Crm.API.FiscalYear.Year;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Fiscalyear
{
	public class UpdateFiscalYear
	{
		public static void UpdateFiscalYear_1()
		{
			FiscalYearOperations fiscalyearoperations = new FiscalYearOperations();
			BodyWrapper request = new BodyWrapper();
			Year fiscalYear = new Year();
			fiscalYear.StartMonth = new Choice<string>("July");
			fiscalYear.DisplayBasedOn = new Choice<String>("start_month");
			request.FiscalYear = fiscalYear;
			APIResponse<ActionHandler> response = fiscalyearoperations.UpdateFiscalYear(request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						ActionResponse actionResponse = actionWrapper.FiscalYear;
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
						if (exception.Details != null)
						{
							foreach (KeyValuePair<string, object> entry in exception.Details)
							{
								Console.WriteLine (entry.Key + ": " + entry.Value);
							}
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
                UpdateFiscalYear_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}