using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.BulkRead.APIException;
using BulkReadOperations = Com.Zoho.Crm.API.BulkRead.BulkReadOperations;
using Criteria = Com.Zoho.Crm.API.BulkRead.Criteria;
using JobDetail = Com.Zoho.Crm.API.BulkRead.JobDetail;
using ResponseHandler = Com.Zoho.Crm.API.BulkRead.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.BulkRead.ResponseWrapper;
using Result = Com.Zoho.Crm.API.BulkRead.Result;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using MinifiedModule = Com.Zoho.Crm.API.Modules.MinifiedModule;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Bulkread
{
    public class GetBulkReadJobDetails
	{
		public static void GetBulkReadJobDetails_1(long jobId)
		{
			BulkReadOperations bulkReadOperations = new BulkReadOperations();
			APIResponse<ResponseHandler> response = bulkReadOperations.GetBulkReadJobDetails(jobId);
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
						List<JobDetail> jobDetails = responseWrapper.Data;
						foreach (JobDetail jobDetail in jobDetails)
						{
							Console.WriteLine ("Bulk read Job ID: " + jobDetail.Id);
							Console.WriteLine ("Bulk read Operation: " + jobDetail.Operation);
							Console.WriteLine ("Bulk read State: " + jobDetail.State.Value);
							Result result = jobDetail.Result;
							if (result != null)
							{
								Console.WriteLine ("Bulkread Result Page: " + result.Page);
								Console.WriteLine ("Bulkread Result Count: " + result.Count);
								Console.WriteLine ("Bulkread Result Download URL: " + result.DownloadUrl);
								Console.WriteLine ("Bulkread Result Per_Page: " + result.PerPage);
								Console.WriteLine ("Bulkread Result MoreRecords: " + result.MoreRecords);
							}
                            Com.Zoho.Crm.API.BulkRead.Query query = jobDetail.Query;
							if (query != null)
							{
								MinifiedModule module = query.Module;
								if (module != null)
								{
									Console.WriteLine ("Bulkread Resource Module Name : " + module.APIName);
									Console.WriteLine ("Bulkread Resource Module Id : " + module.Id);
								}
								Console.WriteLine ("Bulk read Query Page: " + query.Page);
								Console.WriteLine ("Bulk read Query cvid: " + query.Cvid);
								List<string> fields = query.Fields;
								if (fields != null)
								{
									foreach (object fieldName in fields)
									{
										Console.WriteLine ("Bulk read Query Fields: " + fieldName);
									}
								}
								Criteria criteria = query.Criteria;
								if (criteria != null)
								{
									PrintCriteria(criteria);
								}
							}
							Com.Zoho.Crm.API.Users.MinifiedUser createdBy =  jobDetail.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("Bulkread Created By User-ID: " + createdBy.Id);
								Console.WriteLine ("Bulkread Created By user-Name: " + createdBy.Name);
							}
							Console.WriteLine ("Bulkread CreatedTime: " + jobDetail.CreatedTime);
							Console.WriteLine ("Bulkread File Type: " + jobDetail.FileType.Value);
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
		private static void PrintCriteria(Criteria criteria)
		{
			Console.WriteLine ("Bulk read Query Criteria APIName: " + criteria.APIName);
			if (criteria.Comparator != null)
			{
				Console.WriteLine ("Bulk read Query Criteria Comparator: " + criteria.Comparator.Value);
			}
			if (criteria.Value != null)
			{
				Console.WriteLine ("Bulk read Query Criteria Value: " + criteria.Value);
			}
			if (criteria.Field != null)
			{
				Console.WriteLine ("Bulk read Query Criteria field name: " + criteria.Field.APIName);
			}
			List<Criteria> criteriaGroup = criteria.Group;
			if (criteriaGroup != null)
			{
				foreach (Criteria criteria1 in criteriaGroup)
				{
                    PrintCriteria(criteria1);
				}
			}
			if (criteria.GroupOperator != null)
			{
				Console.WriteLine ("Bulk read Query Criteria Group Operator: " + criteria.GroupOperator.Value);
			}
		}
		public static void Call()
		{
			try
			{
				Environment environment = USDataCenter.PRODUCTION;
				IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL" ).Build();
				new Initializer.Builder().Environment(environment).Token(token).Initialize();
				long jobId = 34770615177002l;
                GetBulkReadJobDetails_1(jobId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}