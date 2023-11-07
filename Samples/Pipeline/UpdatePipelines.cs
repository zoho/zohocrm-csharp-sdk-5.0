using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Pipeline.APIException;
using ActionHandler = Com.Zoho.Crm.API.Pipeline.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Pipeline.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Pipeline.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Pipeline.BodyWrapper;
using Maps = Com.Zoho.Crm.API.Pipeline.Maps;
using PipelineOperations = Com.Zoho.Crm.API.Pipeline.PipelineOperations;
using SuccessResponse = Com.Zoho.Crm.API.Pipeline.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Pipeline
{
	public class UpdatePipelines
	{
		public static void UpdatePipelines_1(long LayoutID)
		{
			BodyWrapper bodyWrapper = new BodyWrapper();
			List<Com.Zoho.Crm.API.Pipeline.Pipeline> pipelines = new List<Com.Zoho.Crm.API.Pipeline.Pipeline>();
			Com.Zoho.Crm.API.Pipeline.Pipeline pipeLine =  new Com.Zoho.Crm.API.Pipeline.Pipeline();
			pipeLine.DisplayValue = "Pipeline222123";
			pipeLine.Default = true;
			List<Maps> maps = new List<Maps>();
			Maps pickListValue = new Maps();
			pickListValue.SequenceNumber = 1;
			pickListValue.Id = 3477061006801l;
			pickListValue.DisplayValue = "Closed Won";
			maps.Add (pickListValue);
			pipeLine.Maps = maps;
			pipelines.Add (pipeLine);
			pipeLine.Id = 34770619599012l;
			bodyWrapper.Pipeline = pipelines;
			PipelineOperations pipelineOperations = new PipelineOperations(LayoutID);
			APIResponse<ActionHandler> response = pipelineOperations.UpdatePipelines(bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Pipeline;
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
				long layoutId = 3477061091023l;
                UpdatePipelines_1(layoutId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}