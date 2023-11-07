using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.RescheduleHistory.APIException;
using AppointmentName = Com.Zoho.Crm.API.RescheduleHistory.AppointmentName;
using Approval = Com.Zoho.Crm.API.RescheduleHistory.Approval;
using Info = Com.Zoho.Crm.API.RescheduleHistory.Info;
using RescheduleHistory = Com.Zoho.Crm.API.RescheduleHistory.RescheduleHistory;
using RescheduleHistoryOperations = Com.Zoho.Crm.API.RescheduleHistory.RescheduleHistoryOperations;
using ResponseHandler = Com.Zoho.Crm.API.RescheduleHistory.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.RescheduleHistory.ResponseWrapper;
using User = Com.Zoho.Crm.API.RescheduleHistory.User;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.RescheduleHistory.RescheduleHistoryOperations;

namespace Samples.Reschedulehistory
{
	public class GetAppointmentsRescheduledHistory
	{
		public static void GetAppointmentsRescheduledHistory_1()
		{
			RescheduleHistoryOperations rescheduleHistoryOperations = new RescheduleHistoryOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add(GetAppointmentsRescheduledHistoryParam.FIELDS, "id");
            APIResponse<ResponseHandler> response = rescheduleHistoryOperations.GetAppointmentsRescheduledHistory(paramInstance);
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
						List<RescheduleHistory> data = responseWrapper.Data;
						if (data != null)
						{
							foreach (RescheduleHistory history in data)
							{
								Console.WriteLine ("CurrencySymbol: " + history.CurrencySymbol);
								Console.WriteLine ("reviewProcess: " + history.ReviewProcess);
								Console.WriteLine ("rescheduleReason: " + history.RescheduleReason);
								Console.WriteLine ("sharingPermission: " + history.SharingPermission);
								Console.WriteLine ("Name: " + history.Name);
								Console.WriteLine ("Review: " + history.Review);
								Console.WriteLine ("State: " + history.State);
								Console.WriteLine ("canvasId: " + history.CanvasId);
								Console.WriteLine ("processFlow: " + history.ProcessFlow);
								Console.WriteLine ("Id: " + history.Id);
								Console.WriteLine ("ziaVisions: " + history.ZiaVisions);
								Console.WriteLine ("approved: " + history.Approved);
								Console.WriteLine ("ziaVisions: " + history.ZiaVisions);
								Console.WriteLine ("editable: " + history.Editable);
								Console.WriteLine ("orchestration: " + history.Orchestration);
								Console.WriteLine ("inMerge: " + history.InMerge);
								Console.WriteLine ("approvalState: " + history.ApprovalState);
								Console.WriteLine ("rescheduleNote: " + history.RescheduleNote);
								Console.WriteLine ("rescheduledTo: " + history.RescheduledTo);
								Console.WriteLine ("rescheduledTime: " + history.RescheduledTime);
								Console.WriteLine ("rescheduledFrom: " + history.RescheduledFrom);
								AppointmentName appointmentName = history.AppointmentName;
								if (appointmentName != null)
								{
									Console.WriteLine ("Appointment Name: " + appointmentName.Name);
									Console.WriteLine ("Appointmnet Id: " + appointmentName.Id);
								}
								Approval approval = history.Approval;
								if (approval != null)
								{
									Console.WriteLine ("delegate : " + approval.Delegate);
									Console.WriteLine ("approve : " + approval.Approve);
									Console.WriteLine ("reject : " + approval.Reject);
									Console.WriteLine ("resubmit : " + approval.Resubmit);
								}
								User modifiedBy = history.ModifiedBy;
								if (modifiedBy != null)
								{
									Console.WriteLine ("modifiedBy : " + modifiedBy.Id);
									Console.WriteLine ("modifiedBy : " + modifiedBy.Name);
									Console.WriteLine ("modifiedBy : " + modifiedBy.Email);
								}
								User rescheduledBy = history.RescheduledBy;
								if (rescheduledBy != null)
								{
									Console.WriteLine ("rescheduledBy : " + rescheduledBy.Id);
									Console.WriteLine ("rescheduledBy : " + rescheduledBy.Name);
									Console.WriteLine ("rescheduledBy" + rescheduledBy.Email);
								}
								User createdBy = history.CreatedBy;
								if (createdBy != null)
								{
									Console.WriteLine ("createdBy : " + createdBy.Id);
									Console.WriteLine ("createdBy : " + createdBy.Name);
									Console.WriteLine ("createdBy : " + createdBy.Email);
								}
							}
						}
						Info info = responseWrapper.Info;
                        if (info.PerPage != null)
                        {
                            Console.WriteLine("RelatedRecord Info PerPage: " + info.PerPage);
                        }
                        if (info.Count != null)
                        {
                            Console.WriteLine("RelatedRecord Info Count: " + info.Count);
                        }
                        if (info.Page != null)
                        {
                            Console.WriteLine("RelatedRecord Info Page: " + info.Page);
                        }
                        if (info.MoreRecords != null)
                        {
                            Console.WriteLine("RelatedRecord Info MoreRecords: " + info.MoreRecords);
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
                GetAppointmentsRescheduledHistory_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}