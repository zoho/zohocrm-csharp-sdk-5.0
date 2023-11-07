using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Currencies
{

	public class BaseCurrency : Model
	{
		private bool? prefixSymbol;
		private string isoCode;
		private string symbol;
		private string exchangeRate;
		private Format format;
		private long? id;
		private bool? isActive;
		private string name;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? PrefixSymbol
		{
			/// <summary>The method to get the prefixSymbol</summary>
			/// <returns>bool? representing the prefixSymbol</returns>
			get
			{
				return  this.prefixSymbol;

			}
			/// <summary>The method to set the value to prefixSymbol</summary>
			/// <param name="prefixSymbol">bool?</param>
			set
			{
				 this.prefixSymbol=value;

				 this.keyModified["prefix_symbol"] = 1;

			}
		}

		public string IsoCode
		{
			/// <summary>The method to get the isoCode</summary>
			/// <returns>string representing the isoCode</returns>
			get
			{
				return  this.isoCode;

			}
			/// <summary>The method to set the value to isoCode</summary>
			/// <param name="isoCode">string</param>
			set
			{
				 this.isoCode=value;

				 this.keyModified["iso_code"] = 1;

			}
		}

		public string Symbol
		{
			/// <summary>The method to get the symbol</summary>
			/// <returns>string representing the symbol</returns>
			get
			{
				return  this.symbol;

			}
			/// <summary>The method to set the value to symbol</summary>
			/// <param name="symbol">string</param>
			set
			{
				 this.symbol=value;

				 this.keyModified["symbol"] = 1;

			}
		}

		public string ExchangeRate
		{
			/// <summary>The method to get the exchangeRate</summary>
			/// <returns>string representing the exchangeRate</returns>
			get
			{
				return  this.exchangeRate;

			}
			/// <summary>The method to set the value to exchangeRate</summary>
			/// <param name="exchangeRate">string</param>
			set
			{
				 this.exchangeRate=value;

				 this.keyModified["exchange_rate"] = 1;

			}
		}

		public Format Format
		{
			/// <summary>The method to get the format</summary>
			/// <returns>Instance of Format</returns>
			get
			{
				return  this.format;

			}
			/// <summary>The method to set the value to format</summary>
			/// <param name="format">Instance of Format</param>
			set
			{
				 this.format=value;

				 this.keyModified["format"] = 1;

			}
		}

		public long? Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>long? representing the id</returns>
			get
			{
				return  this.id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">long?</param>
			set
			{
				 this.id=value;

				 this.keyModified["id"] = 1;

			}
		}

		public bool? IsActive
		{
			/// <summary>The method to get the isActive</summary>
			/// <returns>bool? representing the isActive</returns>
			get
			{
				return  this.isActive;

			}
			/// <summary>The method to set the value to isActive</summary>
			/// <param name="isActive">bool?</param>
			set
			{
				 this.isActive=value;

				 this.keyModified["is_active"] = 1;

			}
		}

		public string Name
		{
			/// <summary>The method to get the name</summary>
			/// <returns>string representing the name</returns>
			get
			{
				return  this.name;

			}
			/// <summary>The method to set the value to name</summary>
			/// <param name="name">string</param>
			set
			{
				 this.name=value;

				 this.keyModified["name"] = 1;

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