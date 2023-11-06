using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailComposeMeta
{

	public class User : Model
	{
		private string fullName;
		private string email;
		private string zuid;
		private string zgid;
		private ComposeSettings composeSettings;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string FullName
		{
			/// <summary>The method to get the fullName</summary>
			/// <returns>string representing the fullName</returns>
			get
			{
				return  this.fullName;

			}
			/// <summary>The method to set the value to fullName</summary>
			/// <param name="fullName">string</param>
			set
			{
				 this.fullName=value;

				 this.keyModified["full_name"] = 1;

			}
		}

		public string Email
		{
			/// <summary>The method to get the email</summary>
			/// <returns>string representing the email</returns>
			get
			{
				return  this.email;

			}
			/// <summary>The method to set the value to email</summary>
			/// <param name="email">string</param>
			set
			{
				 this.email=value;

				 this.keyModified["email"] = 1;

			}
		}

		public string Zuid
		{
			/// <summary>The method to get the zuid</summary>
			/// <returns>string representing the zuid</returns>
			get
			{
				return  this.zuid;

			}
			/// <summary>The method to set the value to zuid</summary>
			/// <param name="zuid">string</param>
			set
			{
				 this.zuid=value;

				 this.keyModified["zuid"] = 1;

			}
		}

		public string Zgid
		{
			/// <summary>The method to get the zgid</summary>
			/// <returns>string representing the zgid</returns>
			get
			{
				return  this.zgid;

			}
			/// <summary>The method to set the value to zgid</summary>
			/// <param name="zgid">string</param>
			set
			{
				 this.zgid=value;

				 this.keyModified["zgid"] = 1;

			}
		}

		public ComposeSettings ComposeSettings
		{
			/// <summary>The method to get the composeSettings</summary>
			/// <returns>Instance of ComposeSettings</returns>
			get
			{
				return  this.composeSettings;

			}
			/// <summary>The method to set the value to composeSettings</summary>
			/// <param name="composeSettings">Instance of ComposeSettings</param>
			set
			{
				 this.composeSettings=value;

				 this.keyModified["compose_settings"] = 1;

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