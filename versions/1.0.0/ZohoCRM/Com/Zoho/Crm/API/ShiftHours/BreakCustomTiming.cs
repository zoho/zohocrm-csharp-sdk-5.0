using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.ShiftHours
{

	public class BreakCustomTiming : Model
	{
		private string days;
		private List<string> breakTiming;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Days
		{
			/// <summary>The method to get the days</summary>
			/// <returns>string representing the days</returns>
			get
			{
				return  this.days;

			}
			/// <summary>The method to set the value to days</summary>
			/// <param name="days">string</param>
			set
			{
				 this.days=value;

				 this.keyModified["days"] = 1;

			}
		}

		public List<string> BreakTiming
		{
			/// <summary>The method to get the breakTiming</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  this.breakTiming;

			}
			/// <summary>The method to set the value to breakTiming</summary>
			/// <param name="breakTiming">Instance of List<string></param>
			set
			{
				 this.breakTiming=value;

				 this.keyModified["break_timing"] = 1;

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