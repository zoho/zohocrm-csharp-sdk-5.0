using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Notifications.APIException;
using Info = Com.Zoho.Crm.API.Notifications.Info;
using NotificationsOperations = Com.Zoho.Crm.API.Notifications.NotificationsOperations;
using ResponseHandler = Com.Zoho.Crm.API.Notifications.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Notifications.ResponseWrapper;
using GetNotificationsParam = Com.Zoho.Crm.API.Notifications.NotificationsOperations.GetNotificationsParam;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Notification
{
	public class GetNotificationDetails
	{
		public static void GetNotificationDetails_1()
		{
			NotificationsOperations notificationOperations = new NotificationsOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetNotificationsParam.CHANNEL_ID, 106800211);
			paramInstance.Add (GetNotificationsParam.MODULE, "Accounts");
			paramInstance.Add (GetNotificationsParam.PAGE, 1);
			paramInstance.Add (GetNotificationsParam.PER_PAGE, 2);
			APIResponse<ResponseHandler> response = notificationOperations.GetNotifications(paramInstance);
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
						List<Com.Zoho.Crm.API.Notifications.Notification> notifications = responseWrapper.Watch;
						foreach (Com.Zoho.Crm.API.Notifications.Notification notification in notifications)
						{
							Console.WriteLine ("Notification NotifyOnRelatedAction: " + notification.NotifyOnRelatedAction);
							Console.WriteLine ("Notification ChannelExpiry: " + notification.ChannelExpiry);
							Console.WriteLine ("Notification ResourceUri: " + notification.ResourceUri);
							Console.WriteLine ("Notification ResourceId: " + notification.ResourceId);
							Console.WriteLine ("Notification NotifyUrl: " + notification.NotifyUrl);
							Console.WriteLine ("Notification ResourceName: " + notification.ResourceName);
							Console.WriteLine ("Notification ChannelId: " + notification.ChannelId);
							List<string> fields = notification.Events;
							if (fields != null)
							{
								foreach (object fieldName in fields)
								{
									Console.WriteLine ("Notification Events: " + fieldName);
								}
							}
							Console.WriteLine ("Notification Token: " + notification.Token);
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.PerPage != null)
							{
								Console.WriteLine ("Record Info PerPage: " + info.PerPage);
							}
							if (info.Count != null)
							{
								Console.WriteLine ("Record Info Count: " + info.Count);
							}
							if (info.Page != null)
							{
								Console.WriteLine ("Record Info Page: " + info.Page);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("Record Info MoreRecords: " + info.MoreRecords);
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
                GetNotificationDetails_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}