using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.FieldMapDependency.APIException;
using BodyWrapper = Com.Zoho.Crm.API.FieldMapDependency.BodyWrapper;
using Child = Com.Zoho.Crm.API.FieldMapDependency.Child;
using FieldMapDependencyOperations = Com.Zoho.Crm.API.FieldMapDependency.FieldMapDependencyOperations;
using MapDependency = Com.Zoho.Crm.API.FieldMapDependency.MapDependency;
using Parent = Com.Zoho.Crm.API.FieldMapDependency.Parent;
using PickListMapping = Com.Zoho.Crm.API.FieldMapDependency.PickListMapping;
using PicklistMap = Com.Zoho.Crm.API.FieldMapDependency.PicklistMap;
using ResponseHandler = Com.Zoho.Crm.API.FieldMapDependency.ResponseHandler;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Fieldmapdependency
{
	public class GetMapDependency
	{
		public static void GetMapDependency_1(long layoutId, string module, long dependencyId)
		{
			FieldMapDependencyOperations fieldMapDependencyOperations = new FieldMapDependencyOperations(layoutId, module);
			APIResponse<ResponseHandler> response = fieldMapDependencyOperations.GetMapDependency(dependencyId);
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
						List<MapDependency> mapDependencies = responseWrapper.MapDependency;
						foreach (MapDependency mapDependency in mapDependencies)
						{
							Parent parent = mapDependency.Parent;
							if (parent != null)
							{
								Console.WriteLine ("MapDependency Map ID: " + parent.Id);
								Console.WriteLine ("MapDependency Map APIName: " + parent.APIName);
							}
							Child child = mapDependency.Child;
							if (child != null)
							{
								Console.WriteLine ("MapDependency Child ID: " + child.Id);
								Console.WriteLine ("MapDependency Child APIName: " + child.APIName);
							}
							List<PickListMapping> pickListValues = mapDependency.PickListValues;
							if (pickListValues != null)
							{
								pickListValues.ForEach(pickListValue =>
								{
									Console.WriteLine ("MapDependency PickListValue ID: " + pickListValue.Id);
									Console.WriteLine ("MapDependency PickListValue ActualValue: " + pickListValue.ActualValue);
									Console.WriteLine ("MapDependency PickListValue DisplayValue: " + pickListValue.DisplayValue);
									List<PicklistMap> picklistMaps = pickListValue.Maps;
									if (picklistMaps != null)
									{
										picklistMaps.ForEach(picklistMap =>
										{
											Console.WriteLine ("MapDependency PickListValue Map ID: " + picklistMap.Id);
											Console.WriteLine ("MapDependency PickListValue Map ActualValue: " + picklistMap.ActualValue);
											Console.WriteLine ("MapDependency PickListValue Map DisplayValue: " + picklistMap.DisplayValue);
										});
									}
								});
							}
							Console.WriteLine ("MapDependency Internal: " + mapDependency.Internal);
							Console.WriteLine ("MapDependency Active: " + mapDependency.Active);
							Console.WriteLine ("MapDependency ID: " + mapDependency.Id);
							Console.WriteLine ("MapDependency Active: " + mapDependency.Source);
							Console.WriteLine ("MapDependency Category: " + mapDependency.Category);
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
				string moduleAPIName = "Leads";
				long layoutId = 347706111613002l;
				long dependencyId = 347706111613002l;
                GetMapDependency_1(layoutId, moduleAPIName, dependencyId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}