using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.AssignmentRules.APIException;
using AssignmentRulesOperations = Com.Zoho.Crm.API.AssignmentRules.AssignmentRulesOperations;
using DefaultAssignee = Com.Zoho.Crm.API.AssignmentRules.DefaultAssignee;
using ResponseHandler = Com.Zoho.Crm.API.AssignmentRules.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.AssignmentRules.ResponseWrapper;
using GetAssignmentRulesParam = Com.Zoho.Crm.API.AssignmentRules.AssignmentRulesOperations.GetAssignmentRulesParam;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using MinifiedModule = Com.Zoho.Crm.API.Modules.MinifiedModule;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Assignmentrules
{
	public class GetAssignmentRules
	{
		public static void GetAssignmentRules_1(string moduleAPIName)
		{
			AssignmentRulesOperations assignmentRuleOperations = new AssignmentRulesOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetAssignmentRulesParam.MODULE, moduleAPIName);
			APIResponse<ResponseHandler> response = assignmentRuleOperations.GetAssignmentRules(paramInstance);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.StatusCode == 204)
				{
					Console.WriteLine ("No Content");
					return;
				}
				if (response.IsExpected)
				{
					ResponseHandler responseHandler = response.Object;
					if (responseHandler is ResponseWrapper)
					{
						ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;
						List<Com.Zoho.Crm.API.AssignmentRules.AssignmentRules> assignmentRules = responseWrapper.AssignmentRules;
						foreach (Com.Zoho.Crm.API.AssignmentRules.AssignmentRules assignmentRule in  assignmentRules)
						{
							Console.WriteLine ("AssignmentRule Modified Time: " + assignmentRule.ModifiedTime);
							Console.WriteLine ("AssignmentRule Created Time: " + assignmentRule.CreatedTime);
							DefaultAssignee defaultAssignee = assignmentRule.DefaultAssignee;
							if (defaultAssignee != null)
							{
								Console.WriteLine ("AssignmentRule DefaultUser User-ID: " + defaultAssignee.Id);
								Console.WriteLine ("AssignmentRule DefaultUser User-Name: " + defaultAssignee.Name);
							}
							MinifiedModule module = assignmentRule.Module;
							if (module != null)
							{
								Console.WriteLine ("AssignmentRule Module ID: " + module.Id);
								Console.WriteLine ("AssignmentRule Module API Name: " + module.APIName);
							}
							Console.WriteLine ("AssignmentRule Name: " + assignmentRule.Name);
							Com.Zoho.Crm.API.Users.MinifiedUser modifiedBy =  assignmentRule.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("AssignmentRule Modified By User-Name: " + modifiedBy.Name);
								Console.WriteLine ("AssignmentRule Modified By User-ID: " + modifiedBy.Id);
							}
							Com.Zoho.Crm.API.Users.MinifiedUser createdBy =  assignmentRule.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("AssignmentRule Created By User-Name: " + createdBy.Name);
								Console.WriteLine ("AssignmentRule Created By User-ID: " + createdBy.Id);
							}
							Console.WriteLine ("AssignmentRule ID: " + assignmentRule.Id);
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
		public static void Call()
		{
			try
			{
				Environment environment = USDataCenter.PRODUCTION;
				IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL" ).Build();
				new Initializer.Builder().Environment(environment).Token(token).Initialize();
				string moduleAPIName = "Leads";
                GetAssignmentRules_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}