using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using APIException = Com.Zoho.Crm.API.Org.APIException;
using HierarchyPreferences = Com.Zoho.Crm.API.Org.HierarchyPreferences;
using LicenseDetails = Com.Zoho.Crm.API.Org.LicenseDetails;
using OrgOperations = Com.Zoho.Crm.API.Org.OrgOperations;
using ResponseHandler = Com.Zoho.Crm.API.Org.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Org.ResponseWrapper;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Organization
{
	public class GetOrganization
	{
		public static void GetOrganization_1()
		{
			OrgOperations orgOperations = new OrgOperations();
			APIResponse<ResponseHandler> response = orgOperations.GetOrganization();
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ResponseHandler responseHandler = response.Object;
					if (responseHandler is ResponseWrapper)
					{
						ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;
						List<Com.Zoho.Crm.API.Org.Org> orgs = responseWrapper.Org;
						foreach (Com.Zoho.Crm.API.Org.Org org in orgs)
						{
							Console.WriteLine ("Organization Country: " + org.Country);
							HierarchyPreferences hierarchyPreferences = org.HierarchyPreferences;
							if (hierarchyPreferences != null)
							{
								Console.WriteLine ("Organization HierarchyPreferences Type: " + hierarchyPreferences.Type);
							}
							Console.WriteLine ("Organization PhotoId: " + org.PhotoId);
							Console.WriteLine ("Organization City: " + org.City);
							Console.WriteLine ("Organization Description: " + org.Description);
							Console.WriteLine ("Organization McStatus: " + org.McStatus);
							Console.WriteLine ("Organization GappsEnabled: " + org.GappsEnabled);
							Console.WriteLine ("Organization DomainName: " + org.DomainName);
							Console.WriteLine ("Organization TranslationEnabled: " + org.TranslationEnabled);
							Console.WriteLine ("Organization Street: " + org.Street);
							Console.WriteLine ("Organization Alias: " + org.Alias);
							Console.WriteLine ("Organization Currency: " + org.Currency);
							Console.WriteLine ("Organization Id: " + org.Id);
							Console.WriteLine ("Organization State: " + org.State);
							Console.WriteLine ("Organization Fax: " + org.Fax);
							Console.WriteLine ("Organization EmployeeCount: " + org.EmployeeCount);
							Console.WriteLine ("Organization Zip: " + org.Zip);
							Console.WriteLine ("Organization Website: " + org.Website);
							Console.WriteLine ("Organization CurrencySymbol: " + org.CurrencySymbol);
							Console.WriteLine ("Organization Mobile: " + org.Mobile);
							Console.WriteLine ("Organization CurrencyLocale: " + org.CurrencyLocale);
							Console.WriteLine ("Organization PrimaryZuid: " + org.PrimaryZuid);
							Console.WriteLine ("Organization ZiaPortalId: " + org.ZiaPortalId);
							Console.WriteLine ("Organization TimeZone: " + org.TimeZone.Id);
							Console.WriteLine ("Organization Zgid: " + org.Zgid);
							Console.WriteLine ("Organization CountryCode: " + org.CountryCode);
							LicenseDetails licenseDetails = org.LicenseDetails;
							if (licenseDetails != null)
							{
								Console.WriteLine ("Organization LicenseDetails PaidExpiry: " + licenseDetails.PaidExpiry);
								Console.WriteLine ("Organization LicenseDetails UsersLicensePurchased: " + licenseDetails.UsersLicensePurchased);
								Console.WriteLine ("Organization LicenseDetails TrialType: " + licenseDetails.TrialType);
								Console.WriteLine ("Organization LicenseDetails TrialExpiry: " + licenseDetails.TrialExpiry);
								Console.WriteLine ("Organization LicenseDetails Paid: " + licenseDetails.Paid);
								Console.WriteLine ("Organization LicenseDetails PaidType: " + licenseDetails.PaidType);
							}
							Console.WriteLine ("Organization Phone: " + org.Phone);
							Console.WriteLine ("Organization CompanyName: " + org.CompanyName);
							Console.WriteLine ("Organization PrivacySettings: " + org.PrivacySettings);
							Console.WriteLine ("Organization HipaaComplianceEnabled: " + org.HipaaComplianceEnabled);
							Console.WriteLine ("Organization PrimaryEmail: " + org.PrimaryEmail);
							Console.WriteLine ("Organization IsoCode: " + org.IsoCode);
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
                GetOrganization_1();
			}
			catch (Exception e)
			{
				Console.WriteLine(JsonConvert.SerializeObject(e));
			}
		}
	}
}