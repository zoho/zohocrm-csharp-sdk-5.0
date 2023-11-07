using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.CustomViews.APIException;
using BodyWrapper = Com.Zoho.Crm.API.CustomViews.BodyWrapper;
using Criteria = Com.Zoho.Crm.API.CustomViews.Criteria;
using CustomViewsOperations = Com.Zoho.Crm.API.CustomViews.CustomViewsOperations;
using GetCustomViewsParam = Com.Zoho.Crm.API.CustomViews.CustomViewsOperations.GetCustomViewsParam;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Fields = Com.Zoho.Crm.API.CustomViews.Fields;
using Info = Com.Zoho.Crm.API.CustomViews.Info;
using Owner = Com.Zoho.Crm.API.CustomViews.Owner;
using ResponseHandler = Com.Zoho.Crm.API.CustomViews.ResponseHandler;
using SharedTo = Com.Zoho.Crm.API.CustomViews.SharedTo;
using Translation = Com.Zoho.Crm.API.CustomViews.Translation;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Customview
{
	public class GetCustomView
	{
		public static void GetCustomView_1(string moduleAPIName, long customViewId)
		{
			CustomViewsOperations customViewsOperations = new CustomViewsOperations();
			ParameterMap paramInstance = new ParameterMap();
			paramInstance.Add (GetCustomViewsParam.MODULE, moduleAPIName);
			APIResponse<ResponseHandler> response = customViewsOperations.GetCustomView(customViewId, paramInstance);
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
					if (responseHandler is BodyWrapper)
					{
						BodyWrapper responseWrapper = (BodyWrapper) responseHandler;
						List<Com.Zoho.Crm.API.CustomViews.CustomView> customViews = responseWrapper.CustomViews;
						foreach (Com.Zoho.Crm.API.CustomViews.CustomView customView in customViews)
						{
							Console.WriteLine ("CustomView DisplayValue: " + customView.DisplayValue);
							Console.WriteLine ("CustomView AccessType: " + customView.AccessType);
							Console.WriteLine ("CustomView SystemName: " + customView.SystemName);
							Criteria criteria = customView.Criteria;
							if (criteria != null)
							{
								printCriteria(criteria);
							}
							List<SharedTo> sharedToDetails = customView.SharedTo;
							if (sharedToDetails != null)
							{
								foreach (SharedTo sharedTo in sharedToDetails)
								{
									Console.WriteLine ("SharedDetails Name: " + sharedTo.Name);
									Console.WriteLine ("SharedDetails ID: " + sharedTo.Id);
									Console.WriteLine ("SharedDetails Type: " + sharedTo.Type);
									Console.WriteLine ("SharedDetails Subordinates: " + sharedTo.Subordinates);
								}
							}
							Console.WriteLine ("CustomView SortBy: " + customView.SortBy);
							Console.WriteLine ("CustomView Default: " + customView.Default);
							Console.WriteLine ("CustomView SystemDefined: " + customView.SystemDefined);
							Console.WriteLine ("CustomView Name: " + customView.Name);
							Console.WriteLine ("CustomView ID: " + customView.Id);
							Console.WriteLine ("CustomView Category: " + customView.Category);
							if (customView.Favorite != null)
							{
								Console.WriteLine ("CustomView Favorite: " + customView.Favorite);
							}
							if (customView.SortOrder != null)
							{
								Console.WriteLine ("CustomView SortOrder: " + customView.SortOrder);
							}
							List<Com.Zoho.Crm.API.CustomViews.Fields> fields = customView.Fields;
							if (fields != null)
							{
								foreach (Com.Zoho.Crm.API.CustomViews.Fields field in fields)
								{
									Console.WriteLine ("Custome view field name: " + field.APIName);
								}
							}
							Console.WriteLine ("CustomView LastAccessedType: " + customView.LastAccessedTime);
							Console.WriteLine ("CustomView ModifiedTime: " + customView.ModifiedTime);
							Owner createdBy = customView.CreatedBy;
							if (createdBy != null)
							{
								Console.WriteLine ("CustomView Created By Name : " + createdBy.Name);
								Console.WriteLine ("CustomView Created By id : " + createdBy.Id);
								Console.WriteLine ("CustomView Created By Name : " + createdBy.Email);
							}
							Owner modifiedBy = customView.ModifiedBy;
							if (modifiedBy != null)
							{
								Console.WriteLine ("CustomView Modified By Name : " + modifiedBy.Name);
								Console.WriteLine ("CustomView Modified By id : " + modifiedBy.Id);
								Console.WriteLine ("CustomView Modified By Name : " + modifiedBy.Email);
							}
						}
						Info info = responseWrapper.Info;
						if (info != null)
						{
							Translation translation = info.Translation;
							if (translation != null)
							{
								Console.WriteLine ("CustomView Info Translation PublicViews: " + translation.PublicViews);
								Console.WriteLine ("CustomView Info Translation OtherUsersViews: " + translation.OtherUsersViews);
								Console.WriteLine ("CustomView Info Translation SharedWithMe: " + translation.SharedWithMe);
								Console.WriteLine ("CustomView Info Translation CreatedByMe: " + translation.CreatedByMe);
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
		private static void printCriteria(Criteria criteria)
		{
			if (criteria.Comparator != null)
			{
				Console.WriteLine ("CustomView Criteria Comparator: " + criteria.Comparator);
			}
			if (criteria.Field != null)
			{
				Console.WriteLine ("CustomView Criteria field name: " + criteria.Field.APIName);
			}
			if (criteria.Value != null)
			{
				Console.WriteLine ("CustomView Criteria Value: " + criteria.Value);
			}
			List<Criteria> criteriaGroup = criteria.Group;
			if (criteriaGroup != null)
			{
				foreach (Criteria criteria1 in criteriaGroup)
				{
					printCriteria(criteria1);
				}
			}
			if (criteria.GroupOperator != null)
			{
				Console.WriteLine ("CustomView Criteria Group Operator: " + criteria.GroupOperator);
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
				long customViewId = 4402480892044l;
                GetCustomView_1(moduleAPIName, customViewId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}