using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Tags.APIException;
using NewTagRequestWrapper = Com.Zoho.Crm.API.Tags.NewTagRequestWrapper;
using RecordActionHandler = Com.Zoho.Crm.API.Tags.RecordActionHandler;
using RecordActionResponse = Com.Zoho.Crm.API.Tags.RecordActionResponse;
using RecordActionWrapper = Com.Zoho.Crm.API.Tags.RecordActionWrapper;
using RecordSuccessResponse = Com.Zoho.Crm.API.Tags.RecordSuccessResponse;
using TagsOperations = Com.Zoho.Crm.API.Tags.TagsOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Tags
{
	public class AddTagsToMultipleRecords
	{
		public static void AddTagsToMultipleRecords_1(string moduleAPIName, List<long?> recordIds)
		{
			TagsOperations tagsOperations = new TagsOperations();
			NewTagRequestWrapper request = new NewTagRequestWrapper();
			List<Com.Zoho.Crm.API.Tags.Tag> tagList = new List<Com.Zoho.Crm.API.Tags.Tag>();
			Com.Zoho.Crm.API.Tags.Tag tag1 =  new Com.Zoho.Crm.API.Tags.Tag();
			tag1.Name = "tagNam3e3e12345";
			tagList.Add (tag1);
			request.Tags = tagList;
			request.OverWrite = true;
			request.Ids = recordIds;
			request.OverWrite = false;
			ParameterMap paramInstance = new ParameterMap();
			APIResponse<RecordActionHandler> response = tagsOperations.AddTagsToMultipleRecords(moduleAPIName, request, paramInstance);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					RecordActionHandler recordActionHandler = response.Object;
					if (recordActionHandler is RecordActionWrapper)
					{
						RecordActionWrapper recordActionWrapper = (RecordActionWrapper) recordActionHandler;
						List<RecordActionResponse> actionResponses = recordActionWrapper.Data;
						foreach (RecordActionResponse actionResponse in actionResponses)
						{
							if (actionResponse is RecordSuccessResponse)
							{
								RecordSuccessResponse successResponse = (RecordSuccessResponse) actionResponse;
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
						if (recordActionWrapper.LockedCount != null)
						{
							Console.WriteLine ("Locked Count: " + recordActionWrapper.LockedCount);
						}
						if (recordActionWrapper.SuccessCount != null)
						{
							Console.WriteLine ("Success Count: " + recordActionWrapper.SuccessCount);
						}
						if (recordActionWrapper.WfScheduler != null)
						{
							Console.WriteLine ("WF Scheduler: " + recordActionWrapper.WfScheduler);
						}
					}
					else if (recordActionHandler is APIException)
					{
						APIException exception = (APIException) recordActionHandler;
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
				string moduleAPIName = "Leads";
				List<long?> recordIds = new List<long?>() {34770615623115, 34770616114067 } ;
                AddTagsToMultipleRecords_1(moduleAPIName, recordIds);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}