using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Holidays.APIException;
using ActionHandler = Com.Zoho.Crm.API.Holidays.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Holidays.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Holidays.ActionWrapper;
using Holiday = Com.Zoho.Crm.API.Holidays.Holiday;
using Holidays = Com.Zoho.Crm.API.Holidays.Holidays;
using HolidaysOperations = Com.Zoho.Crm.API.Holidays.HolidaysOperations;
using ShiftHour = Com.Zoho.Crm.API.Holidays.ShiftHour;
using SuccessResponse = Com.Zoho.Crm.API.Holidays.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Holidays
{
	public class UpdateHolidays
	{
		public static void UpdateHolidays_1()
		{
			HolidaysOperations holidaysoperations = new HolidaysOperations("4402480020813");
			Com.Zoho.Crm.API.Holidays.Holidays request = new Com.Zoho.Crm.API.Holidays.Holidays();
			List<Holiday> holidays = new List<Holiday>();
			Holiday holiday = new Holiday();
			holiday.Id = 347706120341;
			holiday.Name = "holiday 1";
			holiday.Date = new DateTime(2023, 8, 12).Date;
			holiday.Type = "shift_holiday";
			// when type is Shift_holiday
			ShiftHour shifthour = new ShiftHour();
			shifthour.Name = "shift hour for T";
			shifthour.Id = 440248001286017l;
			holiday.ShiftHour = shifthour;
			holiday.Year = 2023;
			holidays.Add (holiday);
			request.Holidays_1 = holidays;
			APIResponse<ActionHandler> response = holidaysoperations.UpdateHolidays(request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Holidays;
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
                UpdateHolidays_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}