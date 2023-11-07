using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Variables.APIException;
using ResponseHandler = Com.Zoho.Crm.API.Variables.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Variables.ResponseWrapper;
using VariableGroup = Com.Zoho.Crm.API.Variables.VariableGroup;
using VariablesOperations = Com.Zoho.Crm.API.Variables.VariablesOperations;
using GetVariablesParam = Com.Zoho.Crm.API.Variables.VariablesOperations.GetVariablesParam;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Variables
{
	public class GetVariables
	{
		public static void GetVariables_1()
		{
			VariablesOperations variablesOperations = new VariablesOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetVariablesParam.GROUP, "General");
			APIResponse<ResponseHandler> response = variablesOperations.GetVariables(paramInstance);
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
						List<Com.Zoho.Crm.API.Variables.Variable> variables = responseWrapper.Variables;
						foreach (Com.Zoho.Crm.API.Variables.Variable variable in variables)
						{
							Console.WriteLine ("Variable APIName: " + variable.APIName);
							Console.WriteLine ("Variable Name: " + variable.Name);
							Console.WriteLine ("Variable Description: " + variable.Description);
							Console.WriteLine ("Variable ID: " + variable.Id);
							Console.WriteLine ("Variable Source: " + variable.Source);
							Console.WriteLine ("Variable Type: " + variable.Type);
							VariableGroup variableGroup = variable.VariableGroup;
							if (variableGroup != null)
							{
								Console.WriteLine ("Variable VariableGroup APIName: " + variableGroup.APIName);
								Console.WriteLine ("Variable VariableGroup ID: " + variableGroup.Id);
							}
							Console.WriteLine ("Variable Value: " + variable.Value);
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
                GetVariables_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}