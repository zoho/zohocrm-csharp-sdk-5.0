using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.ChangeOwner.APIException;
using ActionHandler = Com.Zoho.Crm.API.ChangeOwner.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.ChangeOwner.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.ChangeOwner.ActionWrapper;
using ChangeOwnerOperations = Com.Zoho.Crm.API.ChangeOwner.ChangeOwnerOperations;
using MassWrapper = Com.Zoho.Crm.API.ChangeOwner.MassWrapper;
using Owner = Com.Zoho.Crm.API.ChangeOwner.Owner;
using RelatedModules = Com.Zoho.Crm.API.ChangeOwner.RelatedModules;
using SuccessResponse = Com.Zoho.Crm.API.ChangeOwner.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Changeowner
{
	public class UpdateRecordsOwner
	{
		public static void UpdateRecordsOwner_1(string moduleAPIName)
		{
			ChangeOwnerOperations changeOwnerOperations = new ChangeOwnerOperations(moduleAPIName);
			MassWrapper bodyWrapper = new MassWrapper();
			List<long?> Ids = new List<long?>();
			Ids.Add (347706114612001l);
			Ids.Add (347706114611002l);
			bodyWrapper.Ids = Ids;
			Owner owner = new Owner();
			owner.Id = 34770615791024l;
			bodyWrapper.Owner = owner;
			bodyWrapper.Notify = true;
			List<RelatedModules> relatedModules = new List<RelatedModules>();
			RelatedModules relatedModule = new RelatedModules();
			relatedModule.Id = 347706114686005l;
			relatedModule.APIName = "Tasks";
			relatedModules.Add (relatedModule);
			relatedModule = new RelatedModules();
			relatedModule.Id = 347706114686005l;
			relatedModule.APIName = "Tasks";
			relatedModules.Add (relatedModule);
			bodyWrapper.RelatedModules = relatedModules;
			APIResponse<ActionHandler> response = changeOwnerOperations.MassUpdate(bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.Data;
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
                UpdateRecordsOwner_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}