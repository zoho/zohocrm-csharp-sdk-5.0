using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.PortalUserType
{

	public class PortalUserTypeOperations
	{
		private string portal;

		/// <summary>		/// Creates an instance of PortalUserTypeOperations with the given parameters
		/// <param name="portal">string</param>
		
		public PortalUserTypeOperations(string portal)
		{
			 this.portal=portal;


		}

		/// <summary>The method to get user types</summary>
		/// <param name="paramInstance">Instance of ParameterMap</param>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetUserTypes(ParameterMap paramInstance)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v5/settings/portals/");

			apiPath=string.Concat(apiPath,  this.portal.ToString());

			apiPath=string.Concat(apiPath, "/user_type");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.Param=paramInstance;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}

		/// <summary>The method to get user type</summary>
		/// <param name="userTypeId">string</param>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetUserType(string userTypeId)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v5/settings/portals/");

			apiPath=string.Concat(apiPath,  this.portal.ToString());

			apiPath=string.Concat(apiPath, "/user_type/");

			apiPath=string.Concat(apiPath, userTypeId.ToString());

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}


		public static class GetUserTypesParam
		{
			public static readonly Param<string> INCLUDE=new Param<string>("include", "com.zoho.crm.api.PortalUserType.GetUserTypesParam");
		}

	}
}