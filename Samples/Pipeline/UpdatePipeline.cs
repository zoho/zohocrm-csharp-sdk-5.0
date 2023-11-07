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
	public class UpdatePipeline
	{
		public static void UpdatePipeline_1(long LayoutId, long pipelineId)
		{
			BodyWrapper bodyWrapper = new BodyWrapper();
			List<Com.Zoho.Crm.API.Pipeline.Pipeline> pipelines = new List<Com.Zoho.Crm.API.Pipeline.Pipeline>();
			Com.Zoho.Crm.API.Pipeline.Pipeline pipeLine =  new Com.Zoho.Crm.API.Pipeline.Pipeline();
			pipeLine.DisplayValue = "Adfasfsad112123";
			pipeLine.Default = true;
			List<Maps> maps = new List<Maps>();
			Maps pickListValue = new Maps();
			pickListValue.SequenceNumber = 1;
			pickListValue.Id = 3477061006801l;
			pickListValue.DisplayValue = "Adfasfsad112";
			maps.Add (pickListValue);
			pipeLine.Maps = maps;
			pipelines.Add (pipeLine);
			bodyWrapper.Pipeline = pipelines;
			PipelineOperations pipelineOperations = new PipelineOperations(LayoutId);
			APIResponse<ActionHandler> response = pipelineOperations.UpdatePipeline(pipelineId, bodyWrapper);
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
				long pipelineId = 347706117224001l;
                UpdatePipeline_1(layoutId, pipelineId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}