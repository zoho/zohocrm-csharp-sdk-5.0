using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.FieldMapDependency.APIException;
using ActionHandler = Com.Zoho.Crm.API.FieldMapDependency.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.FieldMapDependency.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.FieldMapDependency.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.FieldMapDependency.BodyWrapper;
using Child = Com.Zoho.Crm.API.FieldMapDependency.Child;
using FieldMapDependencyOperations = Com.Zoho.Crm.API.FieldMapDependency.FieldMapDependencyOperations;
using MapDependency = Com.Zoho.Crm.API.FieldMapDependency.MapDependency;
using Parent = Com.Zoho.Crm.API.FieldMapDependency.Parent;
using PickListMapping = Com.Zoho.Crm.API.FieldMapDependency.PickListMapping;
using PicklistMap = Com.Zoho.Crm.API.FieldMapDependency.PicklistMap;
using SuccessResponse = Com.Zoho.Crm.API.FieldMapDependency.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Fieldmapdependency
{
	public class UpdateMapDependency
	{
		public static void UpdateMapDependency_1(long layoutId, string module, long dependencyId)
		{
			FieldMapDependencyOperations fieldMapDependencyOperations = new FieldMapDependencyOperations(layoutId, module);
			BodyWrapper bodyWrapper = new BodyWrapper();
			List<MapDependency> mapDependencies = new List<MapDependency>();
			MapDependency mapdependency = new MapDependency();
			Parent parent = new Parent();
			parent.APIName = "Lead_Status";
			parent.Id = 3652397002611l;
			mapdependency.Parent = parent;
			Child child = new Child();
			child.APIName = "Lead_Status";
			child.Id = 3652397002611l;
			mapdependency.Child = child;
			List<PickListMapping> pickListValues = new List<PickListMapping>();
			PickListMapping pickListValue = new PickListMapping();
			pickListValue.DisplayValue = "-None-";
			pickListValue.Id = 3652397003409l;
			pickListValue.ActualValue = "-None-";
			List<PicklistMap> picklistMaps = new List<PicklistMap>();
			PicklistMap picklistMap = new PicklistMap();
			picklistMap.Id = 3652397003389l;
			picklistMap.ActualValue = "Cold Call";
			picklistMap.DisplayValue = "Cold Call";
			picklistMaps.Add (picklistMap);
			picklistMap = new PicklistMap();
			picklistMap.Id = 3652397003391l;
			picklistMap.ActualValue = "-None-";
			picklistMap.DisplayValue = "-None-";
			picklistMaps.Add (picklistMap);
			pickListValue.Maps = picklistMaps;
			pickListValues.Add (pickListValue);
			mapdependency.PickListValues = pickListValues;
			mapDependencies.Add (mapdependency);
			bodyWrapper.MapDependency = mapDependencies;
			APIResponse<ActionHandler> response = fieldMapDependencyOperations.UpdateMapDependency(dependencyId, bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.MapDependency;
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
				string moduleAPIName = "Leads";
				long layoutId = 347706111613002l;
				long dependencyId = 347706111613002l;
                UpdateMapDependency_1(layoutId, moduleAPIName, dependencyId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}