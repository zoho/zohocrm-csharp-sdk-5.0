using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailSharing
{

	public class ResponseWrapper : Model, ResponseHandler
	{
		private List<GetEmailSharing> emailssharingdetails;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<GetEmailSharing> Emailssharingdetails
		{
			/// <summary>The method to get the emailssharingdetails</summary>
			/// <returns>Instance of List<GetEmailSharing></returns>
			get
			{
				return  this.emailssharingdetails;

			}
			/// <summary>The method to set the value to emailssharingdetails</summary>
			/// <param name="emailssharingdetails">Instance of List<GetEmailSharing></param>
			set
			{
				 this.emailssharingdetails=value;

				 this.keyModified["__emails_sharing_details"] = 1;

			}
		}

		/// <summary>The method to check if the user has modified the given key</summary>
		/// <param name="key">string</param>
		/// <returns>int? representing the modification</returns>
		public int? IsKeyModified(string key)
		{
			if((( this.keyModified.ContainsKey(key))))
			{
				return  this.keyModified[key];

			}
			return null;


		}

		/// <summary>The method to mark the given key as modified</summary>
		/// <param name="key">string</param>
		/// <param name="modification">int?</param>
		public void SetKeyModified(string key, int? modification)
		{
			 this.keyModified[key] = modification;


		}


	}
}