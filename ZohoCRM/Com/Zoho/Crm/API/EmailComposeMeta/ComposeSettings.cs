using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailComposeMeta
{

	public class ComposeSettings : Model
	{
		private DefaultForm defaultFromAddress;
		private int? fontSize;
		private string fontFamily;
		private List<object> emailSignatures;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public DefaultForm DefaultFromAddress
		{
			/// <summary>The method to get the defaultFromAddress</summary>
			/// <returns>Instance of DefaultForm</returns>
			get
			{
				return  this.defaultFromAddress;

			}
			/// <summary>The method to set the value to defaultFromAddress</summary>
			/// <param name="defaultFromAddress">Instance of DefaultForm</param>
			set
			{
				 this.defaultFromAddress=value;

				 this.keyModified["default_from_address"] = 1;

			}
		}

		public int? FontSize
		{
			/// <summary>The method to get the fontSize</summary>
			/// <returns>int? representing the fontSize</returns>
			get
			{
				return  this.fontSize;

			}
			/// <summary>The method to set the value to fontSize</summary>
			/// <param name="fontSize">int?</param>
			set
			{
				 this.fontSize=value;

				 this.keyModified["font_size"] = 1;

			}
		}

		public string FontFamily
		{
			/// <summary>The method to get the fontFamily</summary>
			/// <returns>string representing the fontFamily</returns>
			get
			{
				return  this.fontFamily;

			}
			/// <summary>The method to set the value to fontFamily</summary>
			/// <param name="fontFamily">string</param>
			set
			{
				 this.fontFamily=value;

				 this.keyModified["font_family"] = 1;

			}
		}

		public List<object> EmailSignatures
		{
			/// <summary>The method to get the emailSignatures</summary>
			/// <returns>Instance of List<Object></returns>
			get
			{
				return  this.emailSignatures;

			}
			/// <summary>The method to set the value to emailSignatures</summary>
			/// <param name="emailSignatures">Instance of List<object></param>
			set
			{
				 this.emailSignatures=value;

				 this.keyModified["email_signatures"] = 1;

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