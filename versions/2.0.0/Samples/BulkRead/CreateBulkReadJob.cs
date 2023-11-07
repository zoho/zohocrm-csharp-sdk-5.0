using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.BulkRead.APIException;
using ActionHandler = Com.Zoho.Crm.API.BulkRead.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.BulkRead.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.BulkRead.ActionWrapper;
using BulkReadOperations = Com.Zoho.Crm.API.BulkRead.BulkReadOperations;
using CallBack = Com.Zoho.Crm.API.BulkRead.CallBack;
using Criteria = Com.Zoho.Crm.API.BulkRead.Criteria;
using RequestWrapper = Com.Zoho.Crm.API.BulkRead.RequestWrapper;
using SuccessResponse = Com.Zoho.Crm.API.BulkRead.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using MinifiedModule = Com.Zoho.Crm.API.Modules.MinifiedModule;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Bulkread
{
    public class CreateBulkReadJob
	{
		public static void CreateBulkReadJob_1(string moduleAPIName)
		{
			BulkReadOperations bulkReadOperations = new BulkReadOperations();
			RequestWrapper requestWrapper = new RequestWrapper();
			MinifiedModule module = new MinifiedModule();
			module.APIName = moduleAPIName;
			CallBack callback = new CallBack();
			callback.Url = "https://www.example.com/callback";
			callback.Method = new Choice<string>("post");
			requestWrapper.Callback = callback;
            Com.Zoho.Crm.API.BulkRead.Query query = new Com.Zoho.Crm.API.BulkRead.Query();
			query.Module = module;
			query.Cvid = 347706108701l;
			List<string> fieldAPINames = new List<string>();
			fieldAPINames.Add ("Last_Name");
			query.Fields = fieldAPINames;
			query.Page = 1;
			Criteria criteria = new Criteria();
			criteria.GroupOperator = new Choice<string>("or");
			List<Criteria> criteriaList = new List<Criteria>();
			Criteria group11 = new Criteria();
			group11.GroupOperator = new Choice<string>("and");
			List<Criteria> groupList11 = new List<Criteria>();
			Criteria group111 = new Criteria();
			Com.Zoho.Crm.API.Fields.MinifiedField field111 =  new Com.Zoho.Crm.API.Fields.MinifiedField();
			field111.APIName = "Company";
			group111.Field = field111;
			group111.Comparator = new Choice<string>("equal");
			group111.Value = "Zoho";
			groupList11.Add (group111);
			Criteria group112 = new Criteria();
			Com.Zoho.Crm.API.Fields.MinifiedField field112 =  new Com.Zoho.Crm.API.Fields.MinifiedField();
			field112.APIName = "Owner";
			group112.Field = field112;
			group112.Comparator = new Choice<string>("in");
			List<string> owner = new List<string>() {"3477061173021" } ;
			group112.Value = owner;
			groupList11.Add (group112);
			group11.Group = groupList11;
			criteriaList.Add (group11);
			Criteria group12 = new Criteria();
			group12.GroupOperator = new Choice<string>("or");
			List<Criteria> groupList12 = new List<Criteria>();
			Criteria group121 = new Criteria();
			Com.Zoho.Crm.API.Fields.MinifiedField field121 =  new Com.Zoho.Crm.API.Fields.MinifiedField();
			field121.APIName = "Paid";
			group121.Field = field121;
			group121.Comparator = new Choice<string>("equal");
			group121.Value = true;
			groupList12.Add (group121);
			Criteria group122 = new Criteria();
			Com.Zoho.Crm.API.Fields.MinifiedField field122 =  new Com.Zoho.Crm.API.Fields.MinifiedField();
			field122.APIName = "Created_Time";
			group122.Field = field122;
			group122.Comparator = new Choice<string>("between");
			List<string> createdTime = new List<string>() {"2020-06-03T17:31:48+05:30", "2020-06-03T17:31:48+05:30" } ;
			group122.Value = createdTime;
			groupList12.Add (group122);
			group12.Group = groupList12;
			criteriaList.Add (group12);
			criteria.Group = criteriaList;
			query.Criteria = criteria;
			requestWrapper.Query = query;
			// requestWrapper.FileType = new Choice<string>("ics");
			APIResponse<ActionHandler> response = bulkReadOperations.CreateBulkReadJob(requestWrapper);
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
								Console.WriteLine ("Message: " + exception.Message.Value);
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
				string moduleAPIName = "Leads";
                CreateBulkReadJob_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}