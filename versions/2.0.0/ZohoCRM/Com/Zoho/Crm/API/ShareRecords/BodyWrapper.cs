using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.ShareRecords
{

	public class BodyWrapper : Model
	{
		private List<ShareRecord> share;
		private bool? notifyOnCompletion;
		private bool? notify;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<ShareRecord> Share
		{
			/// <summary>The method to get the share</summary>
			/// <returns>Instance of List<ShareRecord></returns>
			get
			{
				return  this.share;

			}
			/// <summary>The method to set the value to share</summary>
			/// <param name="share">Instance of List<ShareRecord></param>
			set
			{
				 this.share=value;

				 this.keyModified["share"] = 1;

			}
		}

		public bool? NotifyOnCompletion
		{
			/// <summary>The method to get the notifyOnCompletion</summary>
			/// <returns>bool? representing the notifyOnCompletion</returns>
			get
			{
				return  this.notifyOnCompletion;

			}
			/// <summary>The method to set the value to notifyOnCompletion</summary>
			/// <param name="notifyOnCompletion">bool?</param>
			set
			{
				 this.notifyOnCompletion=value;

				 this.keyModified["notify_on_completion"] = 1;

			}
		}

		public bool? Notify
		{
			/// <summary>The method to get the notify</summary>
			/// <returns>bool? representing the notify</returns>
			get
			{
				return  this.notify;

			}
			/// <summary>The method to set the value to notify</summary>
			/// <param name="notify">bool?</param>
			set
			{
				 this.notify=value;

				 this.keyModified["notify"] = 1;

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