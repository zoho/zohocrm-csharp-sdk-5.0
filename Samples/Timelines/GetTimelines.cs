using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Timelines.APIException;
using AutomationDetail = Com.Zoho.Crm.API.Timelines.AutomationDetail;
using FieldHistory = Com.Zoho.Crm.API.Timelines.FieldHistory;
using FieldHistoryValue = Com.Zoho.Crm.API.Timelines.FieldHistoryValue;
using Module = Com.Zoho.Crm.API.Timelines.Module;
using NameIdStructure = Com.Zoho.Crm.API.Timelines.NameIdStructure;
using PicklistDetail = Com.Zoho.Crm.API.Timelines.PicklistDetail;
using Record = Com.Zoho.Crm.API.Timelines.Record;
using RelatedRecord = Com.Zoho.Crm.API.Timelines.RelatedRecord;
using ResponseHandler = Com.Zoho.Crm.API.Timelines.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Timelines.ResponseWrapper;
using Timeline = Com.Zoho.Crm.API.Timelines.Timeline;
using TimelinesOperations = Com.Zoho.Crm.API.Timelines.TimelinesOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Timelines
{
	public class GetTimelines
	{
		public static void GetTimelines_1(string module, string recordId)
		{
			TimelinesOperations timelinesoperations = new TimelinesOperations();
			ParameterMap paramInstance = new ParameterMap();
			APIResponse<ResponseHandler> response = timelinesoperations.GetTimelines(module, recordId, paramInstance);
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
						List<Timeline> timelines = responseWrapper.Timeline;
						if (timelines != null)
						{
							foreach (Timeline timeline in timelines)
							{
								NameIdStructure doneBy = timeline.DoneBy;
								if (doneBy != null)
								{
									Console.WriteLine ("DoneBy Name: " + doneBy.Name);
									Console.WriteLine ("DoneBy Id: " + doneBy.Id);
								}
								RelatedRecord relatedRecord = timeline.RelatedRecord;
								if (relatedRecord != null)
								{
									Console.WriteLine ("RelatedRecord Id: " + relatedRecord.Id);
									Console.WriteLine ("RelatedRecord Name: " + relatedRecord.Name);
									NameIdStructure module1 = relatedRecord.Module;
									Console.WriteLine ("Module : ");
									if (module1 != null)
									{
										Console.WriteLine ("RelatedRecord Module Name: " + module1.Name);
										Console.WriteLine ("RelatedRecord Module Id: " + module1.Id);
									}
								}
								AutomationDetail automationDetails = timeline.AutomationDetails;
								if (automationDetails != null)
								{
									Console.WriteLine ("automationdetails type: " + automationDetails.Type);
									NameIdStructure rule = automationDetails.Rule;
									if (rule != null)
									{
										Console.WriteLine ("automationDetails Rule Name :" + rule.Name);
										Console.WriteLine ("automationDetails Rule Id :" + rule.Id);
									}
								}
                                Com.Zoho.Crm.API.Timelines.Record record1 = timeline.Record;
								if (record1 != null)
								{
									Console.WriteLine ("Record Id: " + record1.Id);
									Console.WriteLine ("Record Name: " + record1.Name);
									Module module2 = record1.Module;
									Console.WriteLine ("Module : ");
									if (module2 != null)
									{
										Console.WriteLine ("Record Module Name: " + module2.APIName);
										Console.WriteLine ("Record Module Id: " + module2.Id);
									}
								}
								Console.WriteLine ("auditedTime : " + timeline.AuditedTime);
								Console.WriteLine ("action : " + timeline.Action);
								Console.WriteLine ("Timeline Id: " + timeline.Id);
								Console.WriteLine ("source: " + timeline.Source);
								Console.WriteLine ("extension: " + timeline.Extension);
								Console.WriteLine ("Type:: " + timeline.Type);
								List<FieldHistory> fieldHistory = timeline.FieldHistory;
								if (fieldHistory != null)
								{
									foreach (FieldHistory history in fieldHistory)
									{
										Console.WriteLine ("FieldHistory dataType: " + history.DataType);
										Console.WriteLine ("FieldHistory enableColourCode: " + history.EnableColourCode);
										Console.WriteLine ("FieldHistory fieldLabel: " + history.FieldLabel);
										Console.WriteLine ("FieldHistory apiName: " + history.APIName);
										Console.WriteLine ("FieldHistory id: " + history.Id);
										FieldHistoryValue value = history.Value;
										if (value != null)
										{
											Console.WriteLine ("new :" + value.New);
											Console.WriteLine ("old :" + value.Old);
										}
										List<PicklistDetail> pickListValues = history.PickListValues;
										if (pickListValues != null)
										{
											foreach (PicklistDetail pickListValue in pickListValues)
											{
												Console.WriteLine ("picklistvalue DisplayValue : " + pickListValue.DisplayValue);
												Console.WriteLine ("picklistvalue sequenceNumber : " + pickListValue.SequenceNumber);
												Console.WriteLine ("picklistvalue colourCode : " + pickListValue.ColourCode);
												Console.WriteLine ("picklistvalue actualValue : " + pickListValue.ActualValue);
												Console.WriteLine ("picklistvalue id : " + pickListValue.Id);
												Console.WriteLine ("picklistvalue type : " + pickListValue.Type);
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
				string module = "leads";
				string recordId = "440248001310027";
                GetTimelines_1(module, recordId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}