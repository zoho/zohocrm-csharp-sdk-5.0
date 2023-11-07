using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.RelatedLists.APIException;
using RelatedListsOperations = Com.Zoho.Crm.API.RelatedLists.RelatedListsOperations;
using GetRelatedListsParam = Com.Zoho.Crm.API.RelatedLists.RelatedListsOperations.GetRelatedListsParam;
using ResponseHandler = Com.Zoho.Crm.API.RelatedLists.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.RelatedLists.ResponseWrapper;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Relatedlist
{
	public class GetRelatedLists
	{
		public static void GetRelatedLists_1(string moduleAPIName, long layoutId)
		{
			RelatedListsOperations relatedListsOperations = new RelatedListsOperations(layoutId);
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetRelatedListsParam.MODULE, moduleAPIName);
			APIResponse<ResponseHandler> response = relatedListsOperations.GetRelatedLists(paramInstance);
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
						List<Com.Zoho.Crm.API.RelatedLists.RelatedList> relatedLists = responseWrapper.RelatedLists;
						foreach (Com.Zoho.Crm.API.RelatedLists.RelatedList relatedList in relatedLists)
						{
							Console.WriteLine ("RelatedList SequenceNumber: " + relatedList.SequenceNumber);
							Console.WriteLine ("RelatedList DisplayLabel: " + relatedList.DisplayLabel);
							Console.WriteLine ("RelatedList APIName: " + relatedList.APIName);
							Console.WriteLine ("RelatedList Module: " + relatedList.Module);
							Console.WriteLine ("RelatedList Name: " + relatedList.Name);
							Console.WriteLine ("RelatedList Action: " + relatedList.Action);
							Console.WriteLine ("RelatedList ID: " + relatedList.Id);
							Console.WriteLine ("RelatedList Href: " + relatedList.Href);
							Console.WriteLine ("RelatedList Type: " + relatedList.Type);
							Console.WriteLine ("RelatedList Connectedmodule: " + relatedList.Connectedmodule);
							Console.WriteLine ("RelatedList Linkingmodule: " + relatedList.Linkingmodule);
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
				string moduleAPIName = "Leads";
				long layoutId = 4402480167l;
                GetRelatedLists_1(moduleAPIName, layoutId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}