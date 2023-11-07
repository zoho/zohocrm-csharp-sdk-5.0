using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.UsersTerritories
{

	public class Territory : Model
	{
		private string id;
		private Manager manager;
		private Manager reportingTo;
		private string name;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>string representing the id</returns>
			get
			{
				return  this.id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">string</param>
			set
			{
				 this.id=value;

				 this.keyModified["id"] = 1;

			}
		}

		public Manager Manager
		{
			/// <summary>The method to get the manager</summary>
			/// <returns>Instance of Manager</returns>
			get
			{
				return  this.manager;

			}
			/// <summary>The method to set the value to manager</summary>
			/// <param name="manager">Instance of Manager</param>
			set
			{
				 this.manager=value;

				 this.keyModified["Manager"] = 1;

			}
		}

		public Manager ReportingTo
		{
			/// <summary>The method to get the reportingTo</summary>
			/// <returns>Instance of Manager</returns>
			get
			{
				return  this.reportingTo;

			}
			/// <summary>The method to set the value to reportingTo</summary>
			/// <param name="reportingTo">Instance of Manager</param>
			set
			{
				 this.reportingTo=value;

				 this.keyModified["Reporting_To"] = 1;

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

				 this.keyModified["Name"] = 1;

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