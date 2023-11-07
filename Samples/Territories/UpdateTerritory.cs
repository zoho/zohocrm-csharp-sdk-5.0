using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Territories.APIException;
using ActionHandler = Com.Zoho.Crm.API.Territories.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Territories.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Territories.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Territories.BodyWrapper;
using Criteria = Com.Zoho.Crm.API.Territories.Criteria;
using Field = Com.Zoho.Crm.API.Territories.Field;
using Manager = Com.Zoho.Crm.API.Territories.Manager;
using ReportingTo = Com.Zoho.Crm.API.Territories.ReportingTo;
using SuccessResponse = Com.Zoho.Crm.API.Territories.SuccessResponse;
using TerritoriesOperations = Com.Zoho.Crm.API.Territories.TerritoriesOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Territories
{
	public class UpdateTerritory
	{
		public static void UpdateTerritory_1(long id)
		{
			TerritoriesOperations territoriesOperations = new TerritoriesOperations();
			BodyWrapper request = new BodyWrapper();
			List<Com.Zoho.Crm.API.Territories.Territories> territories = new List<Com.Zoho.Crm.API.Territories.Territories>();
            Com.Zoho.Crm.API.Territories.Territories territory = new Com.Zoho.Crm.API.Territories.Territories();
			territory.Id = 32133234546765;
			territory.Name = "territoryName";
			Criteria criteria = new Criteria();
			criteria.Comparator = "equal";
			criteria.Value = "3";
			Field field = new Field();
			field.APIName = "Account_Name";
			field.Id = 32321323411;
			criteria.Field = field;
			territory.AccountRuleCriteria = criteria;
			Criteria criteria1 = new Criteria();
			criteria1.Comparator = "not_between";
			List<string> value = new List<string>();
			value.Add ("2023-08-10");
			value.Add ("2023-08-30");
			criteria1.Value = value;
			Field field1 = new Field();
			field1.APIName = "Closing_Date";
			field1.Id = 323213231223411;
			criteria1.Field = field1;
			territory.DealRuleCriteria = criteria1;
			territory.Description = "description";
			territory.PermissionType = new Choice<string>("read_only");
			ReportingTo reportingTo = new ReportingTo();
			reportingTo.Id = 312324234312l;
			territory.ReportingTo = reportingTo;
			Manager manager = new Manager();
			manager.Id = 324234564533l;
			territory.Manager = manager;
			territories.Add (territory);
			request.Territories = territories;
			APIResponse<ActionHandler> response = territoriesOperations.UpdateTerritory(id, request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Territories;
						if (actionResponses != null)
						{
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
				long id = 3242343241;
                UpdateTerritory_1(id);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}