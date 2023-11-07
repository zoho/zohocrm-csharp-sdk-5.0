using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.ShiftHours.APIException;
using ActionHandler = Com.Zoho.Crm.API.ShiftHours.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.ShiftHours.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.ShiftHours.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.ShiftHours.BodyWrapper;
using BreakCustomTiming = Com.Zoho.Crm.API.ShiftHours.BreakCustomTiming;
using BreakHours = Com.Zoho.Crm.API.ShiftHours.BreakHours;
using ShiftHours = Com.Zoho.Crm.API.ShiftHours.ShiftHours;
using ShiftHoursOperations = Com.Zoho.Crm.API.ShiftHours.ShiftHoursOperations;
using SuccessResponse = Com.Zoho.Crm.API.ShiftHours.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Shifthours
{
    public class UpdateShiftHours
	{
		public static void UpdateShiftHours_1()
		{
			ShiftHoursOperations shifthoursoperations = new ShiftHoursOperations("440248020813");
			BodyWrapper request = new BodyWrapper();
			List<ShiftHours> shiftHours = new List<ShiftHours>();
			ShiftHours shifthours = new ShiftHours();
			shifthours.Id = 4402481024794L;
			shifthours.Timezone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
			shifthours.Name = "shift hours holiday10";
			shifthours.SameAsEveryday = true;
			List<Com.Zoho.Crm.API.ShiftHours.Holidays> holidays = new List<Com.Zoho.Crm.API.ShiftHours.Holidays>();
            Com.Zoho.Crm.API.ShiftHours.Holidays holiday = new Com.Zoho.Crm.API.ShiftHours.Holidays();
			holiday.Date = new DateTime(2023, 05, 8).Date;
			holiday.Id = 23456L;
			holiday.Name = "Holi10";
			holiday.Year = 2023;
			holidays.Add (holiday);
			shifthours.Holidays = holidays;
			List<BreakHours> breakHours = new List<BreakHours>();
			BreakHours breakhour = new BreakHours();
			breakhour.Id = 4402481024795L;
			List<string> breakDays = new List<string>();
			breakDays.Add ("Monday");
			breakhour.BreakDays = breakDays;
			breakhour.SameAsEveryday = true;
			// when same_as_everday is True
			List<string> dailytimingforBreakHours = new List<string>();
			dailytimingforBreakHours.Add ("12:00");
			dailytimingforBreakHours.Add ("12:15");
			breakhour.DailyTiming = dailytimingforBreakHours;
			breakHours.Add (breakhour);
			shifthours.BreakHours = breakHours;
	//		//when same_as_everyday is false
			List<BreakCustomTiming> customtimingsforBreakHours = new List<BreakCustomTiming>();
			BreakCustomTiming customTimingforBreakHour = new BreakCustomTiming();
			List<string> breakTimings = new List<string>();
			breakTimings.Add ("12:00");
			breakTimings.Add ("12:15");
			customTimingforBreakHour.BreakTiming = breakTimings;
			customTimingforBreakHour.Days = "Monday";
			customtimingsforBreakHours.Add (customTimingforBreakHour);
			breakhour.CustomTiming = customtimingsforBreakHours;
			breakHours.Add (breakhour);
			shifthours.BreakHours = breakHours;
			//
			List<Com.Zoho.Crm.API.ShiftHours.Users> users = new List<Com.Zoho.Crm.API.ShiftHours.Users>();
            Com.Zoho.Crm.API.ShiftHours.Users user = new Com.Zoho.Crm.API.ShiftHours.Users();
			user.Id = 440248254001L;
			user.EffectiveFrom = new DateTime(2023, 10, 12).Date;
			users.Add (user);
			shifthours.Users = users;
			shiftHours.Add (shifthours);
			request.ShiftHours = shiftHours;
			APIResponse<ActionHandler> response = shifthoursoperations.UpdateShiftHours(request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.ShiftHours;
						foreach (ActionResponse actionResponse in actionResponses)
						{
							if (actionResponse is SuccessResponse)
							{
								SuccessResponse successresponse = (SuccessResponse) actionResponse;
								Console.WriteLine ("Status: " + successresponse.Status.Value);
								Console.WriteLine ("Code: " + successresponse.Code.Value);
								Console.WriteLine ("Details: ");
								foreach (KeyValuePair<string, object> entry in successresponse.Details)
								{
									Console.WriteLine (entry.Key + ": " + entry.Value);
								}
								Console.WriteLine ("Message: " + successresponse.Message);
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
                UpdateShiftHours_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}