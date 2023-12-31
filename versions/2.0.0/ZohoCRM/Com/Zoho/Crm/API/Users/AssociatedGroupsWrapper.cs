using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Users
{

	public class AssociatedGroupsWrapper : Model, ResponseHandler
	{
		private List<AssociatedGroup> userGroups;
		private Info info;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<AssociatedGroup> UserGroups
		{
			/// <summary>The method to get the userGroups</summary>
			/// <returns>Instance of List<AssociatedGroup></returns>
			get
			{
				return  this.userGroups;

			}
			/// <summary>The method to set the value to userGroups</summary>
			/// <param name="userGroups">Instance of List<AssociatedGroup></param>
			set
			{
				 this.userGroups=value;

				 this.keyModified["user_groups"] = 1;

			}
		}

		public Info Info
		{
			/// <summary>The method to get the info</summary>
			/// <returns>Instance of Info</returns>
			get
			{
				return  this.info;

			}
			/// <summary>The method to set the value to info</summary>
			/// <param name="info">Instance of Info</param>
			set
			{
				 this.info=value;

				 this.keyModified["info"] = 1;

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