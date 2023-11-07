using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Tags.APIException;
using Info = Com.Zoho.Crm.API.Tags.Info;
using ResponseHandler = Com.Zoho.Crm.API.Tags.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Tags.ResponseWrapper;
using TagsOperations = Com.Zoho.Crm.API.Tags.TagsOperations;
using GetTagsParam = Com.Zoho.Crm.API.Tags.TagsOperations.GetTagsParam;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Tags
{
	public class GetTags
	{
		public static void GetTags_1(string moduleAPIName)
		{
			TagsOperations tagsOperations = new TagsOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetTagsParam.MODULE, moduleAPIName);
			paramInstance.Add (GetTagsParam.MY_TAGS, "false");
			APIResponse<ResponseHandler> response = tagsOperations.GetTags(paramInstance);
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
						List<Com.Zoho.Crm.API.Tags.Tag> tags = responseWrapper.Tags;
						foreach (Com.Zoho.Crm.API.Tags.Tag tag in tags)
						{
							Console.WriteLine ("Tag CreatedTime: " + tag.CreatedTime);
							Console.WriteLine ("Tag ModifiedTime: " + tag.ModifiedTime);
							Console.WriteLine ("Tag ColorCode: " + tag.ColorCode);
							Console.WriteLine ("Tag Name: " + tag.Name);
							Com.Zoho.Crm.API.Users.MinifiedUser modifiedBy =  tag.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("Tag Modified By User-ID: " + modifiedBy.Id);
								Console.WriteLine ("Tag Modified By User-Name: " + modifiedBy.Name);
							}
							Console.WriteLine ("Tag ID: " + tag.Id);
							Com.Zoho.Crm.API.Users.MinifiedUser createdBy =  tag.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("Tag Created By User-ID: " + createdBy.Id);
								Console.WriteLine ("Tag Created By User-Name: " + createdBy.Name);
							}
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							if (info.Count != null)
							{
								Console.WriteLine ("Tag Info Count: " + info.Count);
							}
							if (info.AllowedCount != null)
							{
								Console.WriteLine ("Tag Info AllowedCount: " + info.AllowedCount);
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
				string moduleAPIName = "Leads";
                GetTags_1(moduleAPIName);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}