using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.DealContactRoles.APIException;
using ActionHandler = Com.Zoho.Crm.API.DealContactRoles.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.DealContactRoles.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.DealContactRoles.ActionWrapper;
using DealContactRolesOperations = Com.Zoho.Crm.API.DealContactRoles.DealContactRolesOperations;
using SuccessResponse = Com.Zoho.Crm.API.DealContactRoles.SuccessResponse;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Dealcontactroles
{
    public class RemoveContactRoleFromDeal
	{
		public static void RemoveContactRoleFromDeal_1(long contactId, long dealId)
		{
			DealContactRolesOperations contactRolesOperations = new DealContactRolesOperations();
			APIResponse<ActionHandler> response = contactRolesOperations.DeleteContactRoleRealation(contactId, dealId);
			if (response != null)
			{
				Console.WriteLine ("Status code" + response.StatusCode);
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
							if (successResponse.Details != null)
							{
								foreach (KeyValuePair<string, object> entry in successResponse.Details)
								{
									Console.WriteLine (entry.Key + ": " + entry.Value);
								}
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
		}
		public static void Call()
		{
			try
			{
				Environment environment = USDataCenter.PRODUCTION;
				IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL" ).Build();
				new Initializer.Builder().Environment(environment).Token(token).Initialize();
				long dealId = 440248001177050l;
				long contactId = 440248001030088l;
                RemoveContactRoleFromDeal_1(contactId, dealId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}