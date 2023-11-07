using System;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.UsersUnavailability.APIException;
using Info = Com.Zoho.Crm.API.UsersUnavailability.Info;
using ResponseHandler = Com.Zoho.Crm.API.UsersUnavailability.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.UsersUnavailability.ResponseWrapper;
using User = Com.Zoho.Crm.API.UsersUnavailability.User;
using UsersUnavailabilityOperations = Com.Zoho.Crm.API.UsersUnavailability.UsersUnavailabilityOperations;
using GetUsersUnavailabilityParam = Com.Zoho.Crm.API.UsersUnavailability.UsersUnavailabilityOperations.GetUsersUnavailabilityParam;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Usersunavailability
{
	public class GetUsersUnavailabilites
	{
		public static void GetUsersUnavailabilites_1()
		{
			UsersUnavailabilityOperations usersUnavailabilityOperations = new UsersUnavailabilityOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add(GetUsersUnavailabilityParam.GROUP_IDS, "34770619,3477061912");
			paramInstance.Add(GetUsersUnavailabilityParam.INCLUDE_INNER_DETAILS, "56xxx8");
			paramInstance.Add(GetUsersUnavailabilityParam.ROLE_IDS, "343370619,3403706191");
			paramInstance.Add (GetUsersUnavailabilityParam.TERRITORY_IDS, "343370619,3403706191");
			JObject filters = new JObject();
			filters.Add ("group_operator", "or");
			JArray group = new JArray();
            JObject criteria1 = new JObject
            {
                { "comparator", "between" }
            };
            JObject criteria1Field = new JObject
            {
                { "api_name", "from" }
            };
            criteria1.Add ("field", criteria1Field);
            JArray criteria1Value = new JArray
            {
                "2021-02-18T19:00:00+05:30",
                "2021-02-19T19:00:00+05:30"
            };
            criteria1.Add ("value", criteria1Value);
			group.Add (criteria1);
            JObject criteria2 = new JObject
            {
                { "comparator", "between" }
            };
            JObject criteria2Field = new JObject
            {
                { "api_name", "to" }
            };
            criteria2.Add ("field", criteria2Field);
            JArray criteria2Value = new JArray
            {
                "2021-02-18T20:00:00+05:30",
                "2021-02-19T20:00:00+05:30"
            };
            criteria2.Add ("value", criteria2Value);
			group.Add (criteria2);
			filters.Add ("group", group);
			paramInstance.Add (GetUsersUnavailabilityParam.FILTERS, filters.ToString());
			APIResponse<ResponseHandler> response = usersUnavailabilityOperations.GetUsersUnavailability(paramInstance);
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
						List<Com.Zoho.Crm.API.UsersUnavailability.UsersUnavailability> users = responseWrapper.UsersUnavailability;
						foreach (Com.Zoho.Crm.API.UsersUnavailability.UsersUnavailability usersUnavailability in users)
						{
							Console.WriteLine ("UsersUnavailability Comments: " + usersUnavailability.Comments);
							Console.WriteLine ("UsersUnavailability From: " + usersUnavailability.From);
							Console.WriteLine ("UsersUnavailability Id: " + usersUnavailability.Id);
							Console.WriteLine ("UsersUnavailability to: " + usersUnavailability.To);
							User user = usersUnavailability.User;
							if (user != null)
							{
								Console.WriteLine ("UsersUnavailability User-Name: " + user.Name);
								Console.WriteLine ("UsersUnavailability User-Id: " + user.Id);
								Console.WriteLine ("UsersUnavailability User-ZuId: " + user.Zuid);
							}
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.PerPage != null)
							{
								Console.WriteLine ("User Info PerPage: " + info.PerPage);
							}
							if (info.Count != null)
							{
								Console.WriteLine ("User Info Count: " + info.Count);
							}
							if (info.Page != null)
							{
								Console.WriteLine ("User Info Page: " + info.Page);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("User Info MoreRecords: " + info.MoreRecords);
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
                GetUsersUnavailabilites_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}