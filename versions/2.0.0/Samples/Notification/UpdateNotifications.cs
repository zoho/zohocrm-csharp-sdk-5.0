using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Notifications.APIException;
using ActionHandler = Com.Zoho.Crm.API.Notifications.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Notifications.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Notifications.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Notifications.BodyWrapper;
using NotificationsOperations = Com.Zoho.Crm.API.Notifications.NotificationsOperations;
using SuccessResponse = Com.Zoho.Crm.API.Notifications.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using System.Collections;

namespace Samples.Notification
{
	public class UpdateNotifications
	{
		public static void UpdateNotifications_1()
		{
			NotificationsOperations notificationOperations = new NotificationsOperations();
			BodyWrapper bodyWrapper = new BodyWrapper();
			List<Com.Zoho.Crm.API.Notifications.Notification> notificationList = new List<Com.Zoho.Crm.API.Notifications.Notification>();
			Com.Zoho.Crm.API.Notifications.Notification notification =  new Com.Zoho.Crm.API.Notifications.Notification();
	//		notification.ChannelId = 106800211l;
			notification.NotifyOnRelatedAction = false;
			List<string> events = new List<string>();
			events.Add ("Accounts.all");
			notification.Events = events;
			notification.ChannelId = "106800211";
			notification.ChannelExpiry = DateTimeOffset.Now;
			notification.Token = "TOKEN_FOR_VERIFICATION_OF_1068002";
			notification.NotifyUrl = "https://www.zohoapis.com";
			notificationList.Add (notification);
			bodyWrapper.Watch = notificationList;
			APIResponse<ActionHandler> response = notificationOperations.UpdateNotifications(bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Watch;
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
									if (entry.Value is IList)
									{
                                        IList dataList = (IList) entry.Value;
										if (dataList.Count > 0 && dataList[0] is Com.Zoho.Crm.API.Notifications.Event)
										{
											List<Com.Zoho.Crm.API.Notifications.Event> eventList = (List<Com.Zoho.Crm.API.Notifications.Event>) dataList;
											foreach (Com.Zoho.Crm.API.Notifications.Event event1 in eventList)
											{
												Console.WriteLine ("Notification ChannelExpiry: " + event1.ChannelExpiry);
												Console.WriteLine ("Notification ResourceUri: " + event1.ResourceUri);
												Console.WriteLine ("Notification ResourceId: " + event1.ResourceId);
												Console.WriteLine ("Notification ResourceName: " + event1.ResourceName);
												Console.WriteLine ("Notification ChannelId: " + event1.ChannelId);
											}
										}
									}
									else
									{
										Console.WriteLine (entry.Key + ": " + entry.Value);
									}
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
				UpdateNotifications_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}