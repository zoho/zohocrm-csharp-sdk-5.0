using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Pipeline.APIException;
using BodyWrapper = Com.Zoho.Crm.API.Pipeline.BodyWrapper;
using ForecastCategory = Com.Zoho.Crm.API.Pipeline.ForecastCategory;
using Maps = Com.Zoho.Crm.API.Pipeline.Maps;
using PipelineOperations = Com.Zoho.Crm.API.Pipeline.PipelineOperations;
using ResponseHandler = Com.Zoho.Crm.API.Pipeline.ResponseHandler;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Pipeline
{
	public class GetPipelines
	{
		public static void GetPipelines_1(long layoutId)
		{
			PipelineOperations pipelineOperations = new PipelineOperations(layoutId);
			APIResponse<ResponseHandler> response = pipelineOperations.GetPipelines();
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
					if (responseHandler is BodyWrapper)
					{
						BodyWrapper responseWrapper = (BodyWrapper) responseHandler;
						List<Com.Zoho.Crm.API.Pipeline.Pipeline> pipelines = responseWrapper.Pipeline;
						if (pipelines != null)
						{
							foreach (Com.Zoho.Crm.API.Pipeline.Pipeline pipeline in pipelines)
							{
								Console.WriteLine ("Pipeline ID: " + pipeline.Id);
								Console.WriteLine ("Pipeline default  : " + pipeline.Default);
								Console.WriteLine ("Pipeline Display value : " + pipeline.DisplayValue);
								Console.WriteLine ("Pipeline Actual value : " + pipeline.ActualValue);
								Console.WriteLine ("Pipeline child available  : " + pipeline.ChildAvailable);
								Com.Zoho.Crm.API.Pipeline.Pipeline parent =  pipeline.Parent;
								if (parent != null)
								{
									Console.WriteLine ("Pipeline parent ID: " + parent.Id);
								}
								List<Maps> maps = pipeline.Maps;
								foreach (Maps map in maps)
								{
									Console.WriteLine ("PickListValue Display Value: " + map.DisplayValue);
									Console.WriteLine ("PickListValue Sequence Number: " + map.SequenceNumber);
									ForecastCategory forecastCategory = map.ForecastCategory;
									if (forecastCategory != null)
									{
										Console.WriteLine ("Forecast Category Name: " + forecastCategory.Name);
										Console.WriteLine ("Forecast Category ID: " + forecastCategory.Id);
									}
									Console.WriteLine ("PickListValue Actual Value: " + map.ActualValue);
									Console.WriteLine ("PickListValue ID: " + map.Id);
									Console.WriteLine ("PickListValue Forecast type: " + map.ForecastType);
									Console.WriteLine ("PickListValue delete: " + map.Delete);
								}
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
		public static void Call()
		{
			try
			{
				Environment environment = USDataCenter.PRODUCTION;
				IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL" ).Build();
				new Initializer.Builder().Environment(environment).Token(token).Initialize();
				long layoutId = 4402480167l;
                GetPipelines_1(layoutId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}