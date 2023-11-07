using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.InventoryTemplates.APIException;
using Folder = Com.Zoho.Crm.API.InventoryTemplates.Folder;
using Info = Com.Zoho.Crm.API.InventoryTemplates.Info;
using InventoryTemplate = Com.Zoho.Crm.API.InventoryTemplates.InventoryTemplate;
using InventoryTemplatesOperations = Com.Zoho.Crm.API.InventoryTemplates.InventoryTemplatesOperations;
using ResponseHandler = Com.Zoho.Crm.API.InventoryTemplates.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.InventoryTemplates.ResponseWrapper;
using GetInventoryTemplatesParam = Com.Zoho.Crm.API.InventoryTemplates.InventoryTemplatesOperations.GetInventoryTemplatesParam;
using MinifiedModule = Com.Zoho.Crm.API.Modules.MinifiedModule;
using MinifiedUser = Com.Zoho.Crm.API.Users.MinifiedUser;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Inventorytemplates
{
	public class GetInventoryTemplates
	{
		public static void GetInventoryTemplates_1(string moduleAPIName)
		{
			InventoryTemplatesOperations inventoryTemplatesOperations = new InventoryTemplatesOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetInventoryTemplatesParam.MODULE, moduleAPIName);
			APIResponse<ResponseHandler> response = inventoryTemplatesOperations.GetInventoryTemplates(paramInstance);
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
						List<InventoryTemplate> inventoryTemplates = responseWrapper.InventoryTemplates;
						foreach (InventoryTemplate inventoryTemplate in inventoryTemplates)
						{
							Console.WriteLine ("InventoryTemplate CreatedTime: " + inventoryTemplate.CreatedTime);
							MinifiedModule module = inventoryTemplate.Module;
							if (module != null)
							{
								Console.WriteLine ("InventoryTemplate Module ID: " + module.Id);
								Console.WriteLine ("InventoryTemplate Module apiName: " + module.APIName);
							}
							Console.WriteLine ("InventoryTemplate Type: " + inventoryTemplate.Type);
							MinifiedUser createdBy = inventoryTemplate.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("InventoryTemplate Created By Name : " + createdBy.Name);
								Console.WriteLine ("InventoryTemplate Created By id : " + createdBy.Id);
							}
							Console.WriteLine ("InventoryTemplate ModifiedTime: " + inventoryTemplate.ModifiedTime);
							Folder folder = inventoryTemplate.Folder;
							if (folder != null)
							{
								Console.WriteLine ("InventoryTemplate Folder  id : " + folder.Id);
								Console.WriteLine ("InventoryTemplate Folder  Name : " + folder.Name);
							}
							Console.WriteLine ("InventoryTemplate Last Usage Time: " + inventoryTemplate.LastUsageTime);
							Console.WriteLine ("InventoryTemplate Name: " + inventoryTemplate.Name);
							MinifiedUser modifiedBy = inventoryTemplate.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("InventoryTemplate Modified By Name : " + modifiedBy.Name);
								Console.WriteLine ("InventoryTemplate Modified By id : " + modifiedBy.Id);
							}
							Console.WriteLine ("InventoryTemplate ID: " + inventoryTemplate.Id);
							Console.WriteLine ("InventoryTemplate EditorMode : " + inventoryTemplate.EditorMode);
							Console.WriteLine ("InventoryTemplate Content: " + inventoryTemplate.Content);
							Console.WriteLine ("InventoryTemplate Favourite: " + inventoryTemplate.Favorite);
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.PerPage != null)
							{
								Console.WriteLine ("Record Info PerPage: " + info.PerPage);
							}
							if (info.Count != null)
							{
								Console.WriteLine ("Record Info Count: " + info.Count);
							}
							if (info.Page != null)
							{
								Console.WriteLine ("Record Info Page: " + info.Page);
							}
							if (info.MoreRecords != null)
							{
								Console.WriteLine ("Record Info MoreRecords: " + info.MoreRecords);
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
				string moduleAPIName = "Quotes";
                GetInventoryTemplates_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}