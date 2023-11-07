using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.UsersTerritories.APIException;
using BulkValidation = Com.Zoho.Crm.API.UsersTerritories.BulkValidation;
using UsersTerritoriesOperations = Com.Zoho.Crm.API.UsersTerritories.UsersTerritoriesOperations;
using Validation = Com.Zoho.Crm.API.UsersTerritories.Validation;
using ValidationGroup = Com.Zoho.Crm.API.UsersTerritories.ValidationGroup;
using ValidationHandler = Com.Zoho.Crm.API.UsersTerritories.ValidationHandler;
using ValidationWrapper = Com.Zoho.Crm.API.UsersTerritories.ValidationWrapper;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Usersterritories
{
	public class ValidateBeforeTransferForAllTerritories
	{
		public static void ValidateBeforeTransferForAllTerritories_1(long userId)
		{
			UsersTerritoriesOperations usersTerritoriesOperations = new UsersTerritoriesOperations();
			APIResponse<ValidationHandler> response = usersTerritoriesOperations.ValidateBeforeTransferForAllTerritories(userId);
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
					ValidationHandler responseHandler = response.Object;
					if (responseHandler is ValidationWrapper)
					{
						ValidationWrapper responseWrapper = (ValidationWrapper) responseHandler;
						List<ValidationGroup> usersTerritory = responseWrapper.ValidateBeforeTransfer;
						foreach (ValidationGroup validationGroup in usersTerritory)
						{
							if (validationGroup is BulkValidation)
							{
								BulkValidation validation = (BulkValidation) validationGroup;
								Console.WriteLine ("User Territory Validation Alert : " + validation.Alert);
								Console.WriteLine ("User Territory Validation Assignment : " + validation.Assignment);
								Console.WriteLine ("User Territory Validation Criteria : " + validation.Criteria);
								Console.WriteLine ("User Territory Validation Name : " + validation.Name);
								Console.WriteLine ("User Territory Validation Id : " + validation.Id);
							}
							else if (validationGroup is Validation)
							{
								Validation validation = (Validation) validationGroup;
								Console.WriteLine ("User Territory Validation Records : " + validation.Records);
								Console.WriteLine ("User Territory Validation Name : " + validation.Name);
								Console.WriteLine ("User Territory Validation Id : " + validation.Id);
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
				long userId = 34770611709;
                ValidateBeforeTransferForAllTerritories_1(userId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}