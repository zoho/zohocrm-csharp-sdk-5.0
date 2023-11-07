using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.UsersTransferDelete.APIException;
using ResponseHandler = Com.Zoho.Crm.API.UsersTransferDelete.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.UsersTransferDelete.ResponseWrapper;
using Status = Com.Zoho.Crm.API.UsersTransferDelete.Status;
using UsersTransferDeleteOperations = Com.Zoho.Crm.API.UsersTransferDelete.UsersTransferDeleteOperations;
using GetStatusParam = Com.Zoho.Crm.API.UsersTransferDelete.UsersTransferDeleteOperations.GetStatusParam;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Userstransferanddelete
{
	public class GetStatus
	{
		public static void GetStatus_1()
		{
			UsersTransferDeleteOperations usersTransferDeleteOperations = new UsersTransferDeleteOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetStatusParam.JOB_ID, 32838742872382);
			APIResponse<ResponseHandler> response = usersTransferDeleteOperations.GetStatus(paramInstance);
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
						List<Status> transferAndDelete = responseWrapper.TransferAndDelete;
						if (transferAndDelete != null)
						{
							foreach (Status status in transferAndDelete)
							{
								Console.WriteLine ("TransferAndDelete Status: " + status.Status_1);
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
                GetStatus_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}