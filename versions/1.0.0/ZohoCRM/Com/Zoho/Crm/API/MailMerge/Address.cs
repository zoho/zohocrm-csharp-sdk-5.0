using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.MailMerge
{

	public class Address : Model
	{
		private AddressValueMap addressValueMap;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public AddressValueMap AddressValueMap
		{
			/// <summary>The method to get the addressValueMap</summary>
			/// <returns>Instance of AddressValueMap</returns>
			get
			{
				return  this.addressValueMap;

			}
			/// <summary>The method to set the value to addressValueMap</summary>
			/// <param name="addressValueMap">Instance of AddressValueMap</param>
			set
			{
				 this.addressValueMap=value;

				 this.keyModified["Address_Value_Map"] = 1;

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