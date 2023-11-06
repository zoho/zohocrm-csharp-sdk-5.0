using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.UserTypeUsers
{

	public class UserTypeUsersOperations
	{
		/// <summary>The method to get users of user type</summary>
		/// <param name="userTypeId">long?</param>
		/// <param name="portalName">string</param>
		/// <param name="paramInstance">Instance of ParameterMap</param>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetUsersOfUserType(long? userTypeId, string portalName, ParameterMap paramInstance)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v5/settings/portals/");

			apiPath=string.Concat(apiPath, portalName.ToString());

			apiPath=string.Concat(apiPath, "/user_type/");

			apiPath=string.Concat(apiPath, userTypeId.ToString());

			apiPath=string.Concat(apiPath, "/users");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.Param=paramInstance;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}


		public static class GetUsersofUserTypeParam
		{
			public static readonly Param<string> FILTERS=new Param<string>("filters", "com.zoho.crm.api.UserTypeUsers.GetUsersofUserTypeParam");
			public static readonly Param<string> TYPE=new Param<string>("type", "com.zoho.crm.api.UserTypeUsers.GetUsersofUserTypeParam");
		}

	}
}