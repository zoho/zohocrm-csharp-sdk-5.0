using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.ScoringRules.APIException;
using ActionHandler = Com.Zoho.Crm.API.ScoringRules.ActionHandler;
using Layout = Com.Zoho.Crm.API.ScoringRules.Layout;
using LayoutRequestWrapper = Com.Zoho.Crm.API.ScoringRules.LayoutRequestWrapper;
using ScoringRulesOperations = Com.Zoho.Crm.API.ScoringRules.ScoringRulesOperations;
using SuccessResponse = Com.Zoho.Crm.API.ScoringRules.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Scoringrules
{
	public class ScoringRuleExecutionUsingLayoutId
	{
		public static void ScoringRuleExecutionUsingLayoutId_1(string moduleAPIName)
		{
			ScoringRulesOperations scoringRulesOperations = new ScoringRulesOperations();
			LayoutRequestWrapper bodyWrapper = new LayoutRequestWrapper();
			Layout layout = new Layout();
			layout.Id = 34770601;
			bodyWrapper.Layout = layout;
			APIResponse<ActionHandler> response = scoringRulesOperations.ScoringRuleExecutionUsingLayoutId(moduleAPIName, bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionResponse = response.Object;
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
                ScoringRuleExecutionUsingLayoutId_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}