using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.RescheduleHistory.APIException;
using ActionHandler = Com.Zoho.Crm.API.RescheduleHistory.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.RescheduleHistory.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.RescheduleHistory.ActionWrapper;
using AppointmentName = Com.Zoho.Crm.API.RescheduleHistory.AppointmentName;
using BodyWrapper = Com.Zoho.Crm.API.RescheduleHistory.BodyWrapper;
using RescheduleHistory = Com.Zoho.Crm.API.RescheduleHistory.RescheduleHistory;
using RescheduleHistoryOperations = Com.Zoho.Crm.API.RescheduleHistory.RescheduleHistoryOperations;
using SuccessResponse = Com.Zoho.Crm.API.RescheduleHistory.SuccessResponse;
using User = Com.Zoho.Crm.API.RescheduleHistory.User;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Reschedulehistory
{
	public class UpdateAppointmentRescheduledHistory
	{
		public static void UpdateAppointmentRescheduledHistory_1(long id)
		{
			RescheduleHistoryOperations rescheduleHistoryOperations = new RescheduleHistoryOperations();
			BodyWrapper request = new BodyWrapper();
			List<RescheduleHistory> data = new List<RescheduleHistory>();
			RescheduleHistory rescheduleHistory = new RescheduleHistory();
			AppointmentName appointmentName = new AppointmentName();
			appointmentName.Name = "Name";
			appointmentName.Id = 34770415007;
			rescheduleHistory.AppointmentName = appointmentName;
			User rescheduledBy = new User();
			rescheduledBy.Id = 3477063021;
			rescheduledBy.Name = "UserName";
			rescheduleHistory.RescheduledBy = rescheduledBy;
			rescheduleHistory.RescheduledTo = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
			rescheduleHistory.RescheduledFrom = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
			rescheduleHistory.RescheduledTime = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));
			rescheduleHistory.RescheduleNote = "Customer unavailable";
			rescheduleHistory.RescheduleReason = "By Customer";
			data.Add (rescheduleHistory);
			request.Data = data;
			APIResponse<ActionHandler> response = rescheduleHistoryOperations.UpdateAppointmentRescheduledHistory(id, request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Data;
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
								Console.WriteLine ("Message: " + successResponse.Message);
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
				long id = 35687346127843l;
                UpdateAppointmentRescheduledHistory_1(id);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}