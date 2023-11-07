using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Variables.APIException;
using ActionHandler = Com.Zoho.Crm.API.Variables.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Variables.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Variables.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Variables.BodyWrapper;
using SuccessResponse = Com.Zoho.Crm.API.Variables.SuccessResponse;
using VariableGroup = Com.Zoho.Crm.API.Variables.VariableGroup;
using VariablesOperations = Com.Zoho.Crm.API.Variables.VariablesOperations;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Variables
{
	public class CreateVariables
	{
		public static void CreateVariables_1()
		{
			VariablesOperations variablesOperations = new VariablesOperations();
			BodyWrapper request = new BodyWrapper();
			List<Com.Zoho.Crm.API.Variables.Variable> variableList = new List<Com.Zoho.Crm.API.Variables.Variable>();
			Com.Zoho.Crm.API.Variables.Variable variable1 =  new Com.Zoho.Crm.API.Variables.Variable();
			variable1.Name = "variables11";
			variable1.APIName = "variables11";
			VariableGroup variableGroup = new VariableGroup();
			variableGroup.Id = 347706110321010;
			variableGroup.Name = "created";
			variable1.VariableGroup = variableGroup;
			variable1.Type = new Choice<string>("integer");
			variable1.Value = "55";
			variable1.Description = "This denotes variable 5 description";
			variableList.Add (variable1);
			variable1 =  new Com.Zoho.Crm.API.Variables.Variable();
			variable1.Name = "variables12";
			variable1.APIName = "variables12";
			variableGroup = new VariableGroup();
			variableGroup.Name = "General";
			variable1.VariableGroup = variableGroup;
			variable1.Type = new Choice<string>("text");
			variable1.Value = "Hello";
			variable1.Description = "This denotes variable 6 description";
			variableList.Add (variable1);
			request.Variables = variableList;
			APIResponse<ActionHandler> response = variablesOperations.Createvariable(request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Variables;
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
                CreateVariables_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}