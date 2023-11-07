using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.MassConvert.APIException;
using ActionResponse = Com.Zoho.Crm.API.MassConvert.ActionResponse;
using AssignTo = Com.Zoho.Crm.API.MassConvert.AssignTo;
using Convert = Com.Zoho.Crm.API.MassConvert.Convert;
using MassConvertOperations = Com.Zoho.Crm.API.MassConvert.MassConvertOperations;
using MoveAttachmentsTo = Com.Zoho.Crm.API.MassConvert.MoveAttachmentsTo;
using RelatedModule = Com.Zoho.Crm.API.MassConvert.RelatedModule;
using SuccessResponse = Com.Zoho.Crm.API.MassConvert.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using Com.Zoho.Crm.API.Record;

namespace Samples.Massconvert
{
	public class MassConvert
	{
		public static void MassConvert_1()
		{
			MassConvertOperations massConvertOperations = new MassConvertOperations();
			Convert bodyWrapper = new Convert();
			bodyWrapper.Ids = new List<long?>() { 347706116634119 };
			Com.Zoho.Crm.API.Record.Record deals =  new Com.Zoho.Crm.API.Record.Record();
			deals.AddFieldValue(Deals.AMOUNT, 1d);
			deals.AddFieldValue(Deals.DEAL_NAME, "V4SDK");
			deals.AddFieldValue(Deals.CLOSING_DATE, new DateTime(2022, 12, 13).Date);
			deals.AddFieldValue(Deals.PIPELINE, new Choice<string>("Closed Won"));
			deals.AddFieldValue(Deals.STAGE, new Choice<string>("Closed Won"));
			bodyWrapper.Deals = deals;
			MoveAttachmentsTo carryovertags = new MoveAttachmentsTo();
			carryovertags.Id = 3652397002179l;
			carryovertags.APIName = "Contacts";
			bodyWrapper.CarryOverTags = new List<MoveAttachmentsTo>() {carryovertags } ;
			List<RelatedModule> related_modules = new List<RelatedModule>();
			RelatedModule relatedmodule = new RelatedModule();
			relatedmodule.APIName = "Tasks";
			relatedmodule.Id = 3652397002193l;
			related_modules.Add (relatedmodule);
			bodyWrapper.RelatedModules = related_modules;
			AssignTo assign_to = new AssignTo();
			assign_to.Id = 3652397281001l;
			bodyWrapper.AssignTo = assign_to;
			MoveAttachmentsTo move_attachments_to = new MoveAttachmentsTo();
			move_attachments_to.APIName = "Contacts";
			move_attachments_to.Id = 3652397002179l;
	//		bodyWrapper.MoveAttachmentsTo = move_attachments_to;
			APIResponse<ActionResponse> response = massConvertOperations.MassConvert(bodyWrapper);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionResponse actionHandler = response.Object;
					if (actionHandler is SuccessResponse)
					{
						SuccessResponse successResponse = (SuccessResponse) actionHandler;
						Console.WriteLine ("Status: " + successResponse.Status.Value);
						Console.WriteLine ("Code: " + successResponse.Code.Value);
						Console.WriteLine ("Details: ");
						foreach (KeyValuePair<string, object> entry in successResponse.Details)
						{
							Console.WriteLine (entry.Key + ": " + entry.Value);
						}
						Console.WriteLine ("Message: " + successResponse.Message);
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
                MassConvert_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}