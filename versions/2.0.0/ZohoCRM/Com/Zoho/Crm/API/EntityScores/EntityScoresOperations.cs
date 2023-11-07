using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.EntityScores
{

	public class EntityScoresOperations
	{
		private string fields;

		/// <summary>		/// Creates an instance of EntityScoresOperations with the given parameters
		/// <param name="fields">string</param>
		
		public EntityScoresOperations(string fields)
		{
			 this.fields=fields;


		}

		/// <summary>The method to get module</summary>
		/// <param name="recordId">long?</param>
		/// <param name="module">string</param>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetModule(long? recordId, string module)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v5/");

			apiPath=string.Concat(apiPath, module.ToString());

			apiPath=string.Concat(apiPath, "/");

			apiPath=string.Concat(apiPath, recordId.ToString());

			apiPath=string.Concat(apiPath, "/Entity_Scores__s");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.AddParam(new Param<string>("fields", "com.zoho.crm.api.EntityScores.GetModuleParam"),  this.fields);

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}

		/// <summary>The method to get modules</summary>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetModules()
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v5/Entity_Scores__s");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.AddParam(new Param<string>("fields", "com.zoho.crm.api.EntityScores.GetModulesParam"),  this.fields);

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}


		public static class GetModuleParam
		{
		}


		public static class GetModulesParam
		{
		}

	}
}