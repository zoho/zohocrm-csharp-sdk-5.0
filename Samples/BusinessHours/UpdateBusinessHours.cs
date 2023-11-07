using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.BusinessHours.APIException;
using ActionHandler = Com.Zoho.Crm.API.BusinessHours.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.BusinessHours.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.BusinessHours.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.BusinessHours.BodyWrapper;
using BreakHoursCustomTiming = Com.Zoho.Crm.API.BusinessHours.BreakHoursCustomTiming;
using BusinessHours = Com.Zoho.Crm.API.BusinessHours.BusinessHours;
using BusinessHoursCreated = Com.Zoho.Crm.API.BusinessHours.BusinessHoursCreated;
using BusinessHoursOperations = Com.Zoho.Crm.API.BusinessHours.BusinessHoursOperations;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Businesshours
{
	public class UpdateBusinessHours
	{
		public static void UpdateBusinessHours_1()
		{
			BusinessHoursOperations businessHoursOperations = new BusinessHoursOperations("4402480020813");
			BodyWrapper request = new BodyWrapper();
			BusinessHours businessHours = new BusinessHours();
			businessHours.Id = 4402481017425L;
			businessHours.SameAsEveryday = false;
			List<Choice<string>> businessdays = new List<Choice<string>>();
			businessdays.Add (new Choice<string>("Monday"));
			businessdays.Add (new Choice<string>("Tuesday"));
			businessdays.Add (new Choice<string>("Wednesday"));
			businessHours.BusinessDays = businessdays;
			businessHours.WeekStartsOn = new Choice<string>("Monday");
			BreakHoursCustomTiming bhct = new BreakHoursCustomTiming();
			bhct.Days = new Choice<string>("Monday");
			List<string> businessTiming = new List<string>();
			businessTiming.Add ("09:00");
			businessTiming.Add ("17:00");
			bhct.BusinessTiming = businessTiming;
			//
			BreakHoursCustomTiming bhct1 = new BreakHoursCustomTiming();
			bhct1.Days = new Choice<string>("Tuesday");
			List<string> businessTiming1 = new List<string>();
			businessTiming1.Add ("10:30");
			businessTiming1.Add ("17:00");
			bhct1.BusinessTiming = businessTiming1;
			//
			BreakHoursCustomTiming bhct2 = new BreakHoursCustomTiming();
			bhct2.Days = new Choice<string>("Wednesday");
			List<string> businessTiming2 = new List<string>();
			businessTiming2.Add ("11:00");
			businessTiming2.Add ("17:00");
			bhct2.BusinessTiming = businessTiming2;
			//
			List<BreakHoursCustomTiming> customTiming = new List<BreakHoursCustomTiming>();
			customTiming.Add (bhct);
			customTiming.Add (bhct1);
			customTiming.Add (bhct2);
			businessHours.CustomTiming = customTiming;
			// when sameasEveryday is true
			List<string> dailyTiming = new List<string>();
			dailyTiming.Add ("10:00");
			dailyTiming.Add ("19:00");
			businessHours.DailyTiming = dailyTiming;
			businessHours.Type = new Choice<string>("custom");
			request.BusinessHours = businessHours;
			APIResponse<ActionHandler> response = businessHoursOperations.UpdateBusinessHours(request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						ActionResponse actionResponse = actionWrapper.BusinessHours;
						if (actionResponse is BusinessHoursCreated)
						{
							BusinessHoursCreated businesshourscreated = (BusinessHoursCreated) actionResponse;
							Console.WriteLine ("Status: " + businesshourscreated.Status.Value);
							Console.WriteLine ("Code: " + businesshourscreated.Code.Value);
							Console.WriteLine ("Details: ");
							foreach (KeyValuePair<string, object> entry in businesshourscreated.Details)
							{
								Console.WriteLine (entry.Key + ": " + entry.Value);
							}
							Console.WriteLine ("Message: " + businesshourscreated.Message.Value);
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
                UpdateBusinessHours_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}