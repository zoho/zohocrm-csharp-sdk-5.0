using System;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Org;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Organization
{
	public class DeleteOrganizationPhoto
	{
        public static void DeleteOrganizationPhoto_1()
        {
            OrgOperations orgOperations = new OrgOperations();
            APIResponse<ActionHandler> response = orgOperations.DeleteOrganizationPhoto();
            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionHandler actionResponse = response.Object;
                    if (actionResponse is SuccessResponse)
                    {
                        SuccessResponse successResponse = (SuccessResponse)actionResponse;
                        Console.WriteLine("Status: " + successResponse.Status.Value);
                        Console.WriteLine("Code: " + successResponse.Code.Value);
                        Console.WriteLine("Details: ");
                        if (successResponse.Details != null)
                        {
                            foreach (KeyValuePair<string, object> entry in successResponse.Details)
                            {
                                Console.WriteLine(entry.Key + ": " + entry.Value);
                            }
                        }
                        Console.WriteLine("Message: " + successResponse.Message);
                    }
                    else if (actionResponse is APIException)
                    {
                        APIException exception = (APIException)actionResponse;
                        Console.WriteLine("Status: " + exception.Status.Value);
                        Console.WriteLine("Code: " + exception.Code.Value);
                        Console.WriteLine("Details: ");
                        if (exception.Details != null)
                        {
                            foreach (KeyValuePair<string, object> entry in exception.Details)
                            {
                                Console.WriteLine(entry.Key + ": " + entry.Value);
                            }
                        }
                        Console.WriteLine("Message: " + exception.Message);
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
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                DeleteOrganizationPhoto_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}

