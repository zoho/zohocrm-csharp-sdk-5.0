using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.ScoringRules.APIException;
using Criteria = Com.Zoho.Crm.API.ScoringRules.Criteria;
using FieldRule = Com.Zoho.Crm.API.ScoringRules.FieldRule;
using Info = Com.Zoho.Crm.API.ScoringRules.Info;
using Layout = Com.Zoho.Crm.API.ScoringRules.Layout;
using ResponseHandler = Com.Zoho.Crm.API.ScoringRules.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.ScoringRules.ResponseWrapper;
using ScoringRulesOperations = Com.Zoho.Crm.API.ScoringRules.ScoringRulesOperations;
using Signal = Com.Zoho.Crm.API.ScoringRules.Signal;
using SignalRule = Com.Zoho.Crm.API.ScoringRules.SignalRule;
using MinifiedUser = Com.Zoho.Crm.API.Users.MinifiedUser;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Scoringrules
{
	public class GetScoringRule
	{
		public static void GetScoringRule_1(string module, long id)
		{
			ScoringRulesOperations scoringRulesOperations = new ScoringRulesOperations();
			ParameterMap paramInstance = new ParameterMap();
			APIResponse<ResponseHandler> response = scoringRulesOperations.GetScoringRule(module, id, paramInstance);
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
						List<Com.Zoho.Crm.API.ScoringRules.ScoringRule> scoringRules = responseWrapper.ScoringRules;
						foreach (Com.Zoho.Crm.API.ScoringRules.ScoringRule scoringRule in scoringRules)
						{
							Layout layout = scoringRule.Layout;
							if (layout != null)
							{
								Console.WriteLine ("ScoringRule Layout ID: " + layout.Id);
								Console.WriteLine ("ScoringRule Layout APIName: " + layout.APIName);
							}
							Console.WriteLine ("ScoringRule CreatedTime: " + scoringRule.CreatedTime);
							Console.WriteLine ("ScoringRule ModifiedTime: " + scoringRule.ModifiedTime);
							List<FieldRule> fieldRules = scoringRule.FieldRules;
							foreach (FieldRule fieldRule in fieldRules)
							{
								Console.WriteLine ("ScoringRule FieldRule Score: " + fieldRule.Score);
								Criteria criteria = fieldRule.Criteria;
								if (criteria != null)
								{
									printCriteria(criteria);
								}
								Console.WriteLine ("ScoringRule FieldRule Id: " + fieldRule.Id);
							}
							Com.Zoho.Crm.API.Modules.Modules module1 =  scoringRule.Module;
							if (module1 != null)
							{
								Console.WriteLine ("ScoringRule Module ID: " + module1.Id);
								Console.WriteLine ("ScoringRule Module APIName: " + module1.APIName);
							}
							Console.WriteLine ("ScoringRule Name: " + scoringRule.Name);
							MinifiedUser modifiedBy = scoringRule.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("ScoringRule Modified By Name : " + modifiedBy.Name);
								Console.WriteLine ("ScoringRule Modified By id : " + modifiedBy.Id);
							}
							Console.WriteLine ("ScoringRule Active: " + scoringRule.Active);
							Console.WriteLine ("ScoringRule Description: " + scoringRule.Description);
							Console.WriteLine ("ScoringRule Id: " + scoringRule.Id);
							List<SignalRule> signalRules = scoringRule.SignalRules;
							if (signalRules != null)
							{
								foreach (SignalRule signalRule in signalRules)
								{
									Console.WriteLine ("ScoringRule SignalRule Score: " + signalRule.Score);
									Console.WriteLine ("ScoringRule SignalRule Id: " + signalRule.Id);
									Signal signal = signalRule.Signal;
									if (signal != null)
									{
										Console.WriteLine ("ScoringRule SignalRule Signal Namespace: " + signal.Namespace);
										Console.WriteLine ("ScoringRule SignalRule Signal Id: " + signal.Id);
									}
								}
							}
							MinifiedUser createdBy = scoringRule.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("ScoringRule Created By Name : " + createdBy.Name);
								Console.WriteLine ("ScoringRule Created By id : " + createdBy.Id);
							}
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.PerPage != null)
							{
								Console.WriteLine ("Info PerPage: " + info.PerPage);
							}
							if (info.Count != null)
							{
								Console.WriteLine ("Info Count: " + info.Count);
							}
							if (info.Page != null)
							{
								Console.WriteLine ("Info Page: " + info.Page);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("Info MoreRecords: " + info.MoreRecords);
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
						Console.WriteLine ("Message: " + exception.Message.Value);
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
				long id = 440248001310044;
				string module = "leads";
                GetScoringRule_1(module, id);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}