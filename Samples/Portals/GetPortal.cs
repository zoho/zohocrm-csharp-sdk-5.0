using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Portals.APIException;
using Owner = Com.Zoho.Crm.API.Portals.Owner;
using PortalsOperations = Com.Zoho.Crm.API.Portals.PortalsOperations;
using ResponseHandler = Com.Zoho.Crm.API.Portals.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Portals.ResponseWrapper;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Portals
{
	public class GetPortal
	{
		public static void GetPortal_1(string portalName)
		{
			PortalsOperations portalsOperations = new PortalsOperations();
			APIResponse<ResponseHandler> response = portalsOperations.GetPortal(portalName);
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
						List<Com.Zoho.Crm.API.Portals.Portals> portals = responseWrapper.Portals;
						foreach (Com.Zoho.Crm.API.Portals.Portals portal in portals)
						{
							Console.WriteLine ("Portals CreatedTime: " + portal.CreatedTime);
							Console.WriteLine ("Portals ModifiedTime: " + portal.ModifiedTime);
							Owner modifiedBy = portal.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("Portals ModifiedBy User-ID: " + modifiedBy.Id);
								Console.WriteLine ("Portals ModifiedBy User-Name: " + modifiedBy.Name);
							}
							Owner createdBy = portal.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("Portals CreatedBy User-ID: " + createdBy.Id);
								Console.WriteLine ("Portals CreatedBy User-Name: " + createdBy.Name);
							}
							Console.WriteLine ("Portals Zaid: " + portal.Zaid);
							Console.WriteLine ("Portals Name: " + portal.Name);
							Console.WriteLine ("Portals Active: " + portal.Active);
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
				else if (response.StatusCode != 204)
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
				string portalName = "PortalsAPItest100";
                GetPortal_1(portalName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}