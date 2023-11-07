using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.VariableGroups.APIException;
using ResponseHandler = Com.Zoho.Crm.API.VariableGroups.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.VariableGroups.ResponseWrapper;
using VariableGroupsOperations = Com.Zoho.Crm.API.VariableGroups.VariableGroupsOperations;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Variablegroups
{
	public class GetVariableGroupByAPIName
	{
		public static void GetVariableGroupByAPIName_1(string variableGroupName)
		{
			VariableGroupsOperations variableGroupsOperations = new VariableGroupsOperations();
			APIResponse<ResponseHandler> response = variableGroupsOperations.GetVariableGroupByAPIName(variableGroupName);
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
						List<Com.Zoho.Crm.API.VariableGroups.VariableGroup> variableGroups = responseWrapper.VariableGroups;
						foreach (Com.Zoho.Crm.API.VariableGroups.VariableGroup variableGroup in variableGroups)
						{
							Console.WriteLine ("VariableGroup DisplayLabel: " + variableGroup.DisplayLabel);
							Console.WriteLine ("VariableGroup APIName: " + variableGroup.APIName);
							Console.WriteLine ("VariableGroup Name: " + variableGroup.Name);
							Console.WriteLine ("VariableGroup Description: " + variableGroup.Description);
							Console.WriteLine ("VariableGroup ID: " + variableGroup.Id);
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
				string variableGroupName = "General";
                GetVariableGroupByAPIName_1(variableGroupName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}