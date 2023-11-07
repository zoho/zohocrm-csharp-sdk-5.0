using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Holidays.APIException;
using Holiday = Com.Zoho.Crm.API.Holidays.Holiday;
using HolidaysOperations = Com.Zoho.Crm.API.Holidays.HolidaysOperations;
using GetHolidaysParam = Com.Zoho.Crm.API.Holidays.HolidaysOperations.GetHolidaysParam;
using Info = Com.Zoho.Crm.API.Holidays.Info;
using ResponseHandler = Com.Zoho.Crm.API.Holidays.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Holidays.ResponseWrapper;
using ShiftHour = Com.Zoho.Crm.API.Holidays.ShiftHour;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Holidays
{
	public class GetHolidays
	{
		public static void GetHolidays_1()
		{
			HolidaysOperations holidaysoperations = new HolidaysOperations("4402480020813");
			ParameterMap paraminstance = new ParameterMap();
	//		paraminstance.Add(GetHolidaysParam.YEAR, 2023);
			paraminstance.Add(GetHolidaysParam.SHIFT_ID, 440248001286017l);
			APIResponse<ResponseHandler> response = holidaysoperations.GetHolidays(paraminstance);
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
						List<Holiday> holidays = responseWrapper.Holidays;
						if (holidays != null)
						{
							Console.WriteLine ("holidays : ");
							foreach (Holiday holiday in holidays)
							{
								Console.WriteLine ("Hoilday ID: " + holiday.Id);
								Console.WriteLine ("Name: " + holiday.Name);
								Console.WriteLine ("date: " + holiday.Date);
								Console.WriteLine ("year: " + holiday.Year);
								Console.WriteLine ("type: " + holiday.Type);
								ShiftHour shifthour = holiday.ShiftHour;
								if (shifthour != null)
								{
									Console.WriteLine ("shifthour: ");
									Console.WriteLine ("name : " + shifthour.Name);
									Console.WriteLine ("Shifthour id : " + shifthour.Id);
								}
							}
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							Console.WriteLine ("info : ");
							Console.WriteLine ("perpage : " + info.PerPage);
							Console.WriteLine ("count : " + info.Count);
							Console.WriteLine ("page : " + info.Page);
							Console.WriteLine ("more_records : " + info.MoreRecords);
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
				else if (response.StatusCode != 204)
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
                GetHolidays_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}