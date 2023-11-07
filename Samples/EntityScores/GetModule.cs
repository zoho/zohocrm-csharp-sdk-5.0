using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.EntityScores.APIException;
using EntityScores = Com.Zoho.Crm.API.EntityScores.EntityScores;
using EntityScoresOperations = Com.Zoho.Crm.API.EntityScores.EntityScoresOperations;
using Info = Com.Zoho.Crm.API.EntityScores.Info;
using ResponseHandler = Com.Zoho.Crm.API.EntityScores.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.EntityScores.ResponseWrapper;
using ScoringRuleStructure = Com.Zoho.Crm.API.EntityScores.ScoringRuleStructure;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Entityscores
{
	public class GetModule
	{
		public static void GetModule_1(long recordId, string module)
		{
			EntityScoresOperations entityScoresOperations = new EntityScoresOperations("Positive_Score");
			APIResponse<ResponseHandler> response = entityScoresOperations.GetModule(recordId, module);
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
						List<EntityScores> data = responseWrapper.Data;
						if (data != null)
						{
							foreach (EntityScores score in data)
							{
								Console.WriteLine ("Score : " + score.Score);
								Console.WriteLine ("PositiveScore : " + score.PositiveScore);
								Console.WriteLine ("TouchPointScore : " + score.TouchPointScore);
								Console.WriteLine ("NegativeScore : " + score.NegativeScore);
								Console.WriteLine ("touchPointNegativeScore : " + score.TouchPointNegativeScore);
								Console.WriteLine ("touchPointPositiveScore : " + score.TouchPointPositiveScore);
								Console.WriteLine ("Id : " + score.Id);
								Console.WriteLine ("ZiaVisions : " + score.ZiaVisions);
								ScoringRuleStructure scoringRule = score.ScoringRule;
								if (scoringRule != null)
								{
									Console.WriteLine ("ScoringRule Id : " + scoringRule.Id);
									Console.WriteLine ("ScoringRule Name : " + scoringRule.Name);
								}
								List<object> fieldStates = score.FieldStates;
								foreach (object field in fieldStates)
								{
									Console.WriteLine ("fieldStates : " + field);
								}
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
				long recordId = 440248001177154l;
				string module = "leads";
                GetModule_1(recordId, module);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}