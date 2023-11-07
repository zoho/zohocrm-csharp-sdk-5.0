using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Territories.APIException;
using Criteria = Com.Zoho.Crm.API.Territories.Criteria;
using Manager = Com.Zoho.Crm.API.Territories.Manager;
using ResponseHandler = Com.Zoho.Crm.API.Territories.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Territories.ResponseWrapper;
using TerritoriesOperations = Com.Zoho.Crm.API.Territories.TerritoriesOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Territories
{
    public class GetTerritory
	{
		public static void GetTerritory_1(long territoryId)
		{
			TerritoriesOperations territoriesOperations = new TerritoriesOperations();
			APIResponse<ResponseHandler> response = territoriesOperations.GetTerritory(territoryId);
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
						List<Com.Zoho.Crm.API.Territories.Territories> territoryList = responseWrapper.Territories;
						foreach (Com.Zoho.Crm.API.Territories.Territories territory in territoryList)
						{
							Console.WriteLine ("Territory CreatedTime: " + territory.CreatedTime);
							Console.WriteLine ("Territory ModifiedTime: " + territory.ModifiedTime);
							Manager manager = territory.Manager;
							if (manager != null)
							{
								Console.WriteLine ("Territory Manager User-Name: " + manager.Name);
								Console.WriteLine ("Territory Manager User-ID: " + manager.Id);
							}
							Criteria accountRuleCriteria = territory.AccountRuleCriteria;
							if (accountRuleCriteria != null)
							{
								printCriteria(accountRuleCriteria);
							}
							Criteria dealRuleCriteria = territory.DealRuleCriteria;
							if (dealRuleCriteria != null)
							{
								printCriteria(dealRuleCriteria);
							}
							Console.WriteLine ("Territory Name: " + territory.Name);
							Com.Zoho.Crm.API.Users.MinifiedUser modifiedBy =  territory.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("Territory Modified By User-Name: " + modifiedBy.Name);
								Console.WriteLine ("Territory Modified By User-ID: " + modifiedBy.Id);
							}
							Console.WriteLine ("Territory Description: " + territory.Description);
							Console.WriteLine ("Territory ID: " + territory.Id);
							Com.Zoho.Crm.API.Users.MinifiedUser createdBy =  territory.CreatedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("Territory Created By User-Name: " + createdBy.Name);
								Console.WriteLine ("Territory Created By User-ID: " + createdBy.Id);
							}
							Com.Zoho.Crm.API.Territories.ReportingTo reportingTo =  territory.ReportingTo;
							if (reportingTo != null)
							{
								Console.WriteLine ("Territory reporting By Territory-Name: " + reportingTo.Name);
								Console.WriteLine ("Territory reporting By Territory-ID: " + reportingTo.Id);
							}
							Console.WriteLine ("Territory permission type: " + territory.PermissionType);
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
		private static void printCriteria(Criteria criteria)
		{
			if (criteria.Comparator != null)
			{
				Console.WriteLine ("CustomView Criteria Comparator: " + criteria.Comparator);
			}
			if (criteria.Field != null)
			{
				Console.WriteLine ("CustomView Criteria field name: " + criteria.Field.APIName);
			}
			if (criteria.Value != null)
			{
				Console.WriteLine ("CustomView Criteria Value: " + criteria.Value);
			}
			List<Criteria> criteriaGroup = criteria.Group;
			if (criteriaGroup != null)
			{
				foreach (Criteria criteria1 in criteriaGroup)
				{
					printCriteria(criteria1);
				}
			}
			if (criteria.GroupOperator != null)
			{
				Console.WriteLine ("CustomView Criteria Group Operator: " + criteria.GroupOperator);
			}
		}
		public static void Call()
		{
			try
			{
				Environment environment = USDataCenter.PRODUCTION;
				IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL" ).Build();
				new Initializer.Builder().Environment(environment).Token(token).Initialize();
				long territoryId = 440248001305227;
                GetTerritory_1(territoryId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}