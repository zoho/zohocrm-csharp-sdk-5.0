using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using BusinessHours = Com.Zoho.Crm.API.BusinessHours.BusinessHours;
using BusinessHoursOperations = Com.Zoho.Crm.API.BusinessHours.BusinessHoursOperations;
using ResponseHandler = Com.Zoho.Crm.API.BusinessHours.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.BusinessHours.ResponseWrapper;
using APIException = Com.Zoho.Crm.API.BusinessHours.APIException;
using BreakHoursCustomTiming = Com.Zoho.Crm.API.BusinessHours.BreakHoursCustomTiming;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Businesshours
{
	public class GetBusinessHours
	{
		public static void GetBusinessHours_1()
		{
			BusinessHoursOperations businessHoursOperations = new BusinessHoursOperations("4402480020813");
			APIResponse<ResponseHandler> response = businessHoursOperations.GetBusinessHours();
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
					ResponseHandler responseObject = (ResponseHandler) response.Object;
					if (responseObject is ResponseWrapper)
					{
						ResponseWrapper responseWrapper = (ResponseWrapper)responseObject;
						BusinessHours businessHours = responseWrapper.BusinessHours;
						List<Choice<string>> businessdays = businessHours.BusinessDays;
						if (businessdays != null)
						{
							Console.WriteLine ("businessdays :");
							foreach (Choice<string> businessday in businessdays)
							{
								Console.WriteLine (businessday.Value);
							}
						}
						else
						{
							Console.WriteLine ("businessdays : null");
						}
						List<BreakHoursCustomTiming> customtiming = businessHours.CustomTiming;
						if (customtiming != null)
						{
							Console.WriteLine ("custom_timing :");
							foreach (BreakHoursCustomTiming bhct in customtiming)
							{
								Console.WriteLine ("days : " + bhct.Days.Value);
								List<string> businessTimings = bhct.BusinessTiming;
								foreach (string businessTiming in businessTimings)
								{
									Console.WriteLine ("businesstimings : " + businessTiming);
								}
							}
						}
						else
						{
							Console.WriteLine ("customtiming : null");
						}
						List<string> dailyTimings = businessHours.DailyTiming;
						if (dailyTimings != null)
						{
							Console.WriteLine ("daily_timings : ");
							foreach (string dailyTiming in dailyTimings)
							{
								Console.WriteLine (dailyTiming);
							}
						}
						else
						{
							Console.WriteLine ("daily_timings : null");
						}
						Console.WriteLine ("week_starts_on : " + businessHours.WeekStartsOn.Value);
						Console.WriteLine ("same_as_everyday : " + businessHours.SameAsEveryday);
						Console.WriteLine ("businesshours_id : " + businessHours.Id);
						Console.WriteLine ("businesshours_type : " + businessHours.Type.Value);
					}
					else if (responseObject is APIException)
					{
						APIException exception = (APIException)responseObject;
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
                GetBusinessHours_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}