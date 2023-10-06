using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.Fields
{

	public class FieldsOperations
	{
		/// <summary>The method to get fields</summary>
		/// <param name="paramInstance">Instance of ParameterMap</param>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetFields(ParameterMap paramInstance)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v5/settings/fields");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.Param=paramInstance;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}

		/// <summary>The method to get field</summary>
		/// <param name="field">long?</param>
		/// <param name="paramInstance">Instance of ParameterMap</param>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetField(long? field, ParameterMap paramInstance)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v5/settings/fields/");

			apiPath=string.Concat(apiPath, field.ToString());

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.Param=paramInstance;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}


		public static class GetFieldsParam
		{
			public static readonly Param<string> MODULE=new Param<string>("module", "com.zoho.crm.api.Fields.GetFieldsParam");
			public static readonly Param<string> DATA_TYPE=new Param<string>("data_type", "com.zoho.crm.api.Fields.GetFieldsParam");
			public static readonly Param<string> TYPE=new Param<string>("type", "com.zoho.crm.api.Fields.GetFieldsParam");
			public static readonly Param<string> INCLUDE=new Param<string>("include", "com.zoho.crm.api.Fields.GetFieldsParam");
			public static readonly Param<string> FEATURE_NAME=new Param<string>("feature_name", "com.zoho.crm.api.Fields.GetFieldsParam");
			public static readonly Param<string> COMPONENT=new Param<string>("component", "com.zoho.crm.api.Fields.GetFieldsParam");
			public static readonly Param<long?> LAYOUT_ID=new Param<long?>("layout_id", "com.zoho.crm.api.Fields.GetFieldsParam");
		}


		public static class GetFieldParam
		{
			public static readonly Param<string> MODULE=new Param<string>("module", "com.zoho.crm.api.Fields.GetFieldParam");
			public static readonly Param<string> INCLUDE=new Param<string>("include", "com.zoho.crm.api.Fields.GetFieldParam");
		}

	}
}