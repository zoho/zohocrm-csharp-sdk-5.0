using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.UsersTransferDelete.APIException;
using ActionHandler = Com.Zoho.Crm.API.UsersTransferDelete.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.UsersTransferDelete.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.UsersTransferDelete.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.UsersTransferDelete.BodyWrapper;
using MoveSubordinate = Com.Zoho.Crm.API.UsersTransferDelete.MoveSubordinate;
using SuccessResponse = Com.Zoho.Crm.API.UsersTransferDelete.SuccessResponse;
using Transfer = Com.Zoho.Crm.API.UsersTransferDelete.Transfer;
using TransferAndDelete = Com.Zoho.Crm.API.UsersTransferDelete.TransferAndDelete;
using UsersTransferDeleteOperations = Com.Zoho.Crm.API.UsersTransferDelete.UsersTransferDeleteOperations;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Userstransferanddelete
{
	public class UserTransferAndDelete
	{
		public static void UserTransferAndDelete_1(long id)
		{
			UsersTransferDeleteOperations usersTransferDeleteOperations = new UsersTransferDeleteOperations();
			BodyWrapper request = new BodyWrapper();
			List<TransferAndDelete> transferAndDeletes = new List<TransferAndDelete>();
			TransferAndDelete transferAndDelete = new TransferAndDelete();
			Transfer transfer = new Transfer();
			transfer.Records = true;
			transfer.Assignment = true;
			transfer.Criteria = false;
			transfer.Id = 34349178323;
			transferAndDelete.Transfer = transfer;
			MoveSubordinate moveSubordinate = new MoveSubordinate();
			moveSubordinate.Id = 323234872984342;
			transferAndDelete.MoveSubordinate = moveSubordinate;
			transferAndDeletes.Add (transferAndDelete);
			request.TransferAndDelete = transferAndDeletes;
			APIResponse<ActionHandler> response = usersTransferDeleteOperations.UserTransferAndDelete(id, request);
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ActionHandler actionHandler = response.Object;
					if (actionHandler is ActionWrapper)
					{
						ActionWrapper actionWrapper = (ActionWrapper) actionHandler;
						List<ActionResponse> actionResponses = actionWrapper.TransferAndDelete;
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
				long id = 3376487238733;
                UserTransferAndDelete_1(id);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}