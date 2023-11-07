using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Pipeline.APIException;
using PipelineOperations = Com.Zoho.Crm.API.Pipeline.PipelineOperations;
using Stages = Com.Zoho.Crm.API.Pipeline.Stages;
using TransferPipelineActionHandler = Com.Zoho.Crm.API.Pipeline.TransferPipelineActionHandler;
using TransferPipelineActionResponse = Com.Zoho.Crm.API.Pipeline.TransferPipelineActionResponse;
using TransferPipeline = Com.Zoho.Crm.API.Pipeline.TransferPipeline;
using TransferPipelineActionWrapper = Com.Zoho.Crm.API.Pipeline.TransferPipelineActionWrapper;
using TransferPipelineWrapper = Com.Zoho.Crm.API.Pipeline.TransferPipelineWrapper;
using TransferPipelineSuccessResponse = Com.Zoho.Crm.API.Pipeline.TransferPipelineSuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Pipeline
{
	public class TransferAndDelete
	{
		public static void TransferAndDelete_1(long LayoutId)
		{
			PipelineOperations pipelineOperations = new PipelineOperations(LayoutId);
			TransferPipelineWrapper request = new TransferPipelineWrapper();
			List<TransferPipeline> transferPipelines = new List<TransferPipeline>();
			TransferPipeline transferPipeline = new TransferPipeline();
			Com.Zoho.Crm.API.Pipeline.TPipeline pipeline =  new Com.Zoho.Crm.API.Pipeline.TPipeline();
			pipeline.From = 347706116634118l;
			pipeline.To = 34770619599012l;
			transferPipeline.Pipeline = pipeline;
			List<Stages> stages = new List<Stages>();
			Stages stage = new Stages();
			stage.From = 3652397006817L;
			stage.To = 3652397006819L;
			stages.Add (stage);
			transferPipeline.Stages = stages;
			transferPipelines.Add (transferPipeline);
			request.TransferPipeline = transferPipelines;
			APIResponse<TransferPipelineActionHandler> response = pipelineOperations.TransferPipelines(request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					TransferPipelineActionHandler transferActionHandler = response.Object;
					if (transferActionHandler is TransferPipelineActionWrapper)
					{
						TransferPipelineActionWrapper transferActionWrapper = (TransferPipelineActionWrapper) transferActionHandler;
						List<TransferPipelineActionResponse> transferPipelines1 = transferActionWrapper.TransferPipeline;
						foreach (TransferPipelineActionResponse transferPipeline1 in transferPipelines1)
						{
							if (transferPipeline1 is TransferPipelineSuccessResponse)
							{
								TransferPipelineSuccessResponse successResponse = (TransferPipelineSuccessResponse) transferPipeline1;
								Console.WriteLine ("Status: " + successResponse.Status.Value);
								Console.WriteLine ("Code: " + successResponse.Code.Value);
								Console.WriteLine ("Details: ");
								foreach (KeyValuePair<string, object> entry in successResponse.Details)
								{
									Console.WriteLine (entry.Key + ": " + entry.Value);
								}
								Console.WriteLine ("Message: " + successResponse.Message);
							}
							else if (transferPipeline1 is APIException)
							{
								APIException exception = (APIException) transferPipeline1;
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
					else if (transferActionHandler is APIException)
					{
						APIException exception = (APIException) transferActionHandler;
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
                TransferAndDelete_1(layoutId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}