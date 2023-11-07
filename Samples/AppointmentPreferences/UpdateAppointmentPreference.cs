using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.AppointmentPreference.APIException;
using ActionHandler = Com.Zoho.Crm.API.AppointmentPreference.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.AppointmentPreference.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.AppointmentPreference.ActionWrapper;
using AppointmentPreference = Com.Zoho.Crm.API.AppointmentPreference.AppointmentPreference;
using AppointmentPreferenceOperations = Com.Zoho.Crm.API.AppointmentPreference.AppointmentPreferenceOperations;
using BodyWrapper = Com.Zoho.Crm.API.AppointmentPreference.BodyWrapper;
using Field = Com.Zoho.Crm.API.AppointmentPreference.Field;
using FieldMappings = Com.Zoho.Crm.API.AppointmentPreference.FieldMappings;
using Layout = Com.Zoho.Crm.API.AppointmentPreference.Layout;
using SuccessResponse = Com.Zoho.Crm.API.AppointmentPreference.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Appointmentpreferences
{
	public class UpdateAppointmentPreference
	{
		public static void UpdateAppointmentPreference_1()
		{
			AppointmentPreferenceOperations appointmentPreferenceOperations = new AppointmentPreferenceOperations();
			BodyWrapper request = new BodyWrapper();
			AppointmentPreference appointmentPreferences = new AppointmentPreference();
			appointmentPreferences.AllowBookingOutsideBusinesshours = false;
			appointmentPreferences.AllowBookingOutsideServiceAvailability = true;
			appointmentPreferences.WhenAppointmentCompleted = new Choice<string>("create_deal");
			appointmentPreferences.WhenDurationExceeds = new Choice<string>("ask_appointment_provider_to_complete");
			appointmentPreferences.ShowJobSheet = true;
			Dictionary<string, object> dealRecordConfiguration = new Dictionary<string, object>();
			Layout layout = new Layout();
			layout.APIName = "Standard";
			layout.Id = 4402480173l;
			dealRecordConfiguration.Add ("layout", layout);
			List<FieldMappings> mappings = new List<FieldMappings>();
			FieldMappings fieldMappings = new FieldMappings();
			fieldMappings.Type = new Choice<string>("static");
			fieldMappings.Value = "Closed Won";
			Field field = new Field();
			field.APIName = "Stage";
			field.Id = 440248001308017l;
			fieldMappings.Field = field;
			mappings.Add (fieldMappings);
			dealRecordConfiguration.Add ("field_mappings", mappings);
			appointmentPreferences.DealRecordConfiguration = dealRecordConfiguration;
			request.AppointmentPreferences = appointmentPreferences;
			APIResponse<ActionHandler> response = appointmentPreferenceOperations.UpdateAppointmentPreference(request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						ActionResponse actionResponse = actionWrapper.AppointmentPreferences;
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
                UpdateAppointmentPreference_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}