using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.ScoringRules.APIException;
using ActionHandler = Com.Zoho.Crm.API.ScoringRules.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.ScoringRules.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.ScoringRules.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.ScoringRules.BodyWrapper;
using Criteria = Com.Zoho.Crm.API.ScoringRules.Criteria;
using FieldRule = Com.Zoho.Crm.API.ScoringRules.FieldRule;
using Layout = Com.Zoho.Crm.API.ScoringRules.Layout;
using ScoringRulesOperations = Com.Zoho.Crm.API.ScoringRules.ScoringRulesOperations;
using Signal = Com.Zoho.Crm.API.ScoringRules.Signal;
using SignalRule = Com.Zoho.Crm.API.ScoringRules.SignalRule;
using SuccessResponse = Com.Zoho.Crm.API.ScoringRules.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Scoringrules
{
	public class UpdateScoringRules
	{
		public static void UpdateScoringRules_1()
		{
			ScoringRulesOperations scoringRulesOperations = new ScoringRulesOperations();
			BodyWrapper bodyWrapper = new BodyWrapper();
			List<Com.Zoho.Crm.API.ScoringRules.ScoringRule> scoringRules = new List<Com.Zoho.Crm.API.ScoringRules.ScoringRule>();
			Com.Zoho.Crm.API.ScoringRules.ScoringRule scoringRule =  new Com.Zoho.Crm.API.ScoringRules.ScoringRule();
			scoringRule.Id = "3477061002175";
			scoringRule.Name = "Rule 10";
			scoringRule.Description = "Rule for Module Leads";
			Com.Zoho.Crm.API.Modules.Modules module =  new Com.Zoho.Crm.API.Modules.Modules();
			module.APIName = "Leads";
			module.Id = 3477061002175l;
			scoringRule.Module = module;
			Layout layout = new Layout();
			layout.APIName = "Standard";
			layout.Id = 3477061091055l;
			scoringRule.Layout = layout;
			scoringRule.Active = false;
			List<FieldRule> fieldRules = new List<FieldRule>();
			FieldRule fieldRule = new FieldRule();
			fieldRule.Score = 10;
	//		fieldRule.Id = 347706114954005l;
	//		fieldRule.Delete = null;
			Criteria criteria = new Criteria();
			criteria.GroupOperator = "or";
			List<Criteria> group = new List<Criteria>();
			Criteria criteria1 = new Criteria();
			Com.Zoho.Crm.API.ScoringRules.Field field1 =  new Com.Zoho.Crm.API.ScoringRules.Field();
			field1.APIName = "Company";
			criteria1.Field = field1;
			criteria1.Comparator = "equal";
			criteria1.Value = "zoho";
			group.Add (criteria1);
			Criteria criteria2 = new Criteria();
			Com.Zoho.Crm.API.ScoringRules.Field field2 =  new Com.Zoho.Crm.API.ScoringRules.Field();
			field2.APIName = "Designation";
			criteria2.Field = field2;
			criteria2.Comparator = "equal";
			criteria2.Value = "review";
			group.Add (criteria2);
			Criteria criteria3 = new Criteria();
			Com.Zoho.Crm.API.ScoringRules.Field field3 =  new Com.Zoho.Crm.API.ScoringRules.Field();
			field3.APIName = "Last_Name";
			criteria3.Field = field3;
			criteria3.Comparator = "equal";
			criteria3.Value = "SDK";
			group.Add (criteria3);
			criteria.Group = group;
			fieldRule.Criteria = criteria;
			fieldRules.Add (fieldRule);
			scoringRule.FieldRules = fieldRules;
			List<SignalRule> signalRules = new List<SignalRule>();
			SignalRule signalRule = new SignalRule();
			signalRule.Id = "4836976111233";
			signalRule.Score = 10;
			Signal signal = new Signal();
			signal.Id = 4876876112019L;
			signal.Namespace = "Email_Incoming__s";
			signalRule.Signal = signal;
			signalRules.Add (signalRule);
			scoringRule.SignalRules = signalRules;
			scoringRules.Add (scoringRule);
			bodyWrapper.ScoringRules = scoringRules;
			APIResponse<ActionHandler> response = scoringRulesOperations.UpdateScoringRules(bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.ScoringRules;
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
                UpdateScoringRules_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}