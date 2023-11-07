using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using ConversionOption = Com.Zoho.Crm.API.Record.ConversionOption;
using ConversionOptionsResponseWrapper = Com.Zoho.Crm.API.Record.ConversionOptionsResponseWrapper;
using PreferenceFieldMatchedValue = Com.Zoho.Crm.API.Record.PreferenceFieldMatchedValue;
using RecordOperations = Com.Zoho.Crm.API.Record.RecordOperations;
using ResponseHandler = Com.Zoho.Crm.API.Record.ResponseHandler;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using System.Collections;

namespace Samples.Record
{
	public class LeadConversionOptions
	{
		public static void LeadConversionOptions_1(long recordId)
		{
			RecordOperations recordOperations = new RecordOperations();
			string moduleAPIName = "Leads";
			APIResponse<ResponseHandler> response = recordOperations.LeadConversionOptions(recordId, moduleAPIName);
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
					if (responseHandler is ConversionOptionsResponseWrapper)
					{
						ConversionOptionsResponseWrapper conversionOptionResponseWrapper = (ConversionOptionsResponseWrapper) responseHandler;
						ConversionOption conversionOption = conversionOptionResponseWrapper.Conversionoptions;
						Com.Zoho.Crm.API.Modules.Modules module =  conversionOption.ModulePreference;
						if (module != null)
						{
							Console.WriteLine ("ConversionOptions ModulePreference API-Name: " + module.APIName);
							Console.WriteLine ("ConversionOptions ModulePreference ID: " + module.Id);
						}
						List<Com.Zoho.Crm.API.Record.Record> contacts = conversionOption.Contacts;
						if (contacts != null)
						{
							foreach (Com.Zoho.Crm.API.Record.Record contact in contacts)
							{
								Console.WriteLine ("Record ID: " + contact.Id);
								Console.WriteLine ("Record KeyValues: ");
								foreach (KeyValuePair<string, object> entry in contact.GetKeyValues())
								{
									string keyName = entry.Key;
									object value = entry.Value;
									if (value is IList)
									{
										Console.WriteLine ("Record KeyName : " + keyName);
                                        IList dataList = (IList) value;
										foreach (object data in dataList)
										{
											if (data is IDictionary)
											{
												Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
												foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) data))
												{
													Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
												}
											}
											else
											{
												Console.WriteLine (data);
											}
										}
									}
									else if (value is IDictionary)
									{
										Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
										foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) value))
										{
											Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
										}
									}
									else
									{
										Console.WriteLine ("Record KeyName : " + keyName + " - Value : " + value);
									}
								}
							}
						}
						PreferenceFieldMatchedValue preferenceFieldMatchedValue = conversionOption.PreferenceFieldMatchedValue;
						if (preferenceFieldMatchedValue != null)
						{
							List<Com.Zoho.Crm.API.Record.Record> contactsPreferenceField = preferenceFieldMatchedValue.Contacts;
							if (contactsPreferenceField != null)
							{
								foreach (Com.Zoho.Crm.API.Record.Record contact in contactsPreferenceField)
								{
									Console.WriteLine ("Record ID: " + contact.Id);
									Console.WriteLine ("Record KeyValues: ");
									foreach (KeyValuePair<string, object> entry in contact.GetKeyValues())
									{
										string keyName = entry.Key;
										object value = entry.Value;
										if (value is IDictionary)
										{
											Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
											foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) value))
											{
												Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
											}
										}
										else
										{
											Console.WriteLine ("Record KeyName : " + keyName + " - Value : " + value);
										}
									}
								}
							}
							List<Com.Zoho.Crm.API.Record.Record> accountsPreferenceField = preferenceFieldMatchedValue.Accounts;
							if (accountsPreferenceField != null)
							{
								foreach (Com.Zoho.Crm.API.Record.Record account in accountsPreferenceField)
								{
									Console.WriteLine ("Record ID: " + account.Id);
									Console.WriteLine ("Record KeyValues: ");
									foreach (KeyValuePair<string, object> entry in account.GetKeyValues())
									{
										string keyName = entry.Key;
										object value = entry.Value;
										if (value is IDictionary)
										{
											Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
											foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) value))
											{
												Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
											}
										}
										else
										{
											Console.WriteLine ("Record KeyName : " + keyName + " - Value : " + value);
										}
									}
								}
							}
							List<Com.Zoho.Crm.API.Record.Record> dealsPreferenceField = preferenceFieldMatchedValue.Deals;
							if (dealsPreferenceField != null)
							{
								foreach (Com.Zoho.Crm.API.Record.Record deal in dealsPreferenceField)
								{
									Console.WriteLine ("Record ID: " + deal.Id);
									Console.WriteLine ("Record KeyValues: ");
									foreach (KeyValuePair<string, object> entry in deal.GetKeyValues())
									{
										string keyName = entry.Key;
										Object value = entry.Value;
										if (value is IDictionary)
										{
											Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
											foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) value))
											{
												Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
											}
										}
										else
										{
											Console.WriteLine ("Record KeyName : " + keyName + " - Value : " + value);
										}
									}
								}
							}
						}
						List<Com.Zoho.Crm.API.Record.Record> accoutns = conversionOption.Accounts;
						if (accoutns != null)
						{
							foreach (Com.Zoho.Crm.API.Record.Record account in accoutns)
							{
								Console.WriteLine ("Record ID: " + account.Id);
								Console.WriteLine ("Record KeyValues: ");
								foreach (KeyValuePair<string, object> entry in account.GetKeyValues())
								{
									string keyName = entry.Key;
									Object value = entry.Value;
									if (value is IList)
									{
										Console.WriteLine ("Record KeyName : " + keyName);
                                        IList dataList = (IList) value;
										foreach (object data in dataList)
										{
											if (data is IDictionary)
											{
												Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
												foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) data))
												{
													Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
												}
											}
											else
											{
												Console.WriteLine (data);
											}
										}
									}
									else if (value is IDictionary)
									{
										Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
										foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) value))
										{
											Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
										}
									}
									else
									{
										Console.WriteLine ("Record KeyName : " + keyName + " - Value : " + value);
									}
								}
							}
						}
						List<Com.Zoho.Crm.API.Record.Record> deals = conversionOption.Deals;
						if (deals != null)
						{
							foreach (Com.Zoho.Crm.API.Record.Record deal in deals)
							{
								Console.WriteLine ("Record ID: " + deal.Id);
								Console.WriteLine ("Record KeyValues: ");
								foreach (KeyValuePair<string, object> entry in deal.GetKeyValues())
								{
									string keyName = entry.Key;
									Object value = entry.Value;
									if (value is IList)
									{
										Console.WriteLine ("Record KeyName : " + keyName);
                                        IList dataList = (IList) value;
										foreach (object data in dataList)
										{
											if (data is IDictionary)
											{
												Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
												foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) data))
												{
													Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
												}
											}
											else
											{
												Console.WriteLine (data);
											}
										}
									}
									else if (value is IDictionary)
									{
										Console.WriteLine ("Record KeyName : " + keyName + " - Value : ");
										foreach (KeyValuePair<string, object> mapValue in ((Dictionary<string, object>) value))
										{
											Console.WriteLine (mapValue.Key + " : " + mapValue.Value);
										}
									}
									else
									{
										Console.WriteLine ("Record KeyName : " + keyName + " - Value : " + value);
									}
								}
							}
						}
						List<Com.Zoho.Crm.API.Modules.Modules> modulesWithMultipleLayouts = conversionOption.ModulesWithMultipleLayouts;
						if (modulesWithMultipleLayouts != null)
						{
							foreach (Com.Zoho.Crm.API.Modules.Modules module_1 in modulesWithMultipleLayouts)
							{
								Console.WriteLine ("ConversionOptions ModulesWithMultipleLayouts API-Name: " + module_1.APIName);
								Console.WriteLine ("ConversionOptions ModulesWithMultipleLayouts ID: " + module_1.Id);
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
						Console.WriteLine ("Message: " + exception.Message.Value);
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
				long recordId = 347706116989001;
                LeadConversionOptions_1(recordId);
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}