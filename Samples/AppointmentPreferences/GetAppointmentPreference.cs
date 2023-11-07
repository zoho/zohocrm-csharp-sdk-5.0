using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.AppointmentPreference.APIException;
using AppointmentPreference = Com.Zoho.Crm.API.AppointmentPreference.AppointmentPreference;
using AppointmentPreferenceOperations = Com.Zoho.Crm.API.AppointmentPreference.AppointmentPreferenceOperations;
using ResponseHandler = Com.Zoho.Crm.API.AppointmentPreference.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.AppointmentPreference.ResponseWrapper;
using GetAppointmentPreferenceParam = Com.Zoho.Crm.API.AppointmentPreference.AppointmentPreferenceOperations.GetAppointmentPreferenceParam;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using FieldMappings = Com.Zoho.Crm.API.AppointmentPreference.FieldMappings;
using Layout = Com.Zoho.Crm.API.AppointmentPreference.Layout;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Appointmentpreferences
{
	public class GetAppointmentPreference
	{
		public static void GetAppointmentPreference_1()
		{
			AppointmentPreferenceOperations appointmentPreferenceOperations = new AppointmentPreferenceOperations();
			ParameterMap paraminstance = new ParameterMap();
			paraminstance.Add(GetAppointmentPreferenceParam.INCLUDE, "");
			APIResponse<ResponseHandler> response = appointmentPreferenceOperations.GetAppointmentPreference(paraminstance);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.StatusCode == 204)
				{
					Console.WriteLine ("No Content");
					return;
				}
				if (response.IsExpected)
				{
					ResponseHandler responseHandler = response.Object;
					if (responseHandler is ResponseWrapper)
					{
						ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;
						AppointmentPreference appointmentPreferences = responseWrapper.AppointmentPreferences;
						if (appointmentPreferences != null)
						{
							Console.WriteLine ("AppointmentPreference showJobSheet : " + appointmentPreferences.ShowJobSheet);
							Console.WriteLine ("AppointmentPreference whenDurationExceeds : " + appointmentPreferences.WhenDurationExceeds.Value);
							Console.WriteLine ("AppointmentPreference allowBookingOutsideServiceAvailability : " + appointmentPreferences.AllowBookingOutsideServiceAvailability);
							Console.WriteLine ("AppointmentPreference whenAppointmentCompleted : " + appointmentPreferences.WhenAppointmentCompleted.Value);
							Console.WriteLine ("AppointmentPreference allowBookingOutsideBusinesshours : " + appointmentPreferences.AllowBookingOutsideBusinesshours);
							Dictionary<string, object> dealRecordConfiguration = appointmentPreferences.DealRecordConfiguration;
							if (dealRecordConfiguration != null)
							{
								foreach (KeyValuePair<string, object> entry in dealRecordConfiguration)
								{
									if (entry.Key.Equals("layout",System.StringComparison.OrdinalIgnoreCase))
									{
										Com.Zoho.Crm.API.AppointmentPreference.Layout layout =  (Layout) entry.Value;
										Console.WriteLine ("Layout Id :" + layout.Id);
										Console.WriteLine ("LayoutName : " + layout.APIName);
									}
									if (entry.Key.Equals("field_mappings",System.StringComparison.OrdinalIgnoreCase))
									{
										List<Com.Zoho.Crm.API.AppointmentPreference.FieldMappings> fieldMappings = (List<FieldMappings>) entry.Value;
										if (fieldMappings != null)
										{
											foreach (FieldMappings fieldMapping in fieldMappings)
											{
												Console.WriteLine ("FieldMAppings Type: " + fieldMapping.Type.Value);
												Console.WriteLine ("FieldMappings Value: " + fieldMapping.Value);
												Com.Zoho.Crm.API.AppointmentPreference.Field field =  fieldMapping.Field;
												if (field != null)
												{
													Console.WriteLine ("Field APIName : " + field.APIName);
													Console.WriteLine ("Field Id : " + field.Id);
												}
											}
										}
									}
								}
							}
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
				GetAppointmentPreference_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}