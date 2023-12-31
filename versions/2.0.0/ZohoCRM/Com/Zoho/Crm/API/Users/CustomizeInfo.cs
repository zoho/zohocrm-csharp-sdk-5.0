using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Users
{

	public class CustomizeInfo : Model
	{
		private object notesDesc;
		private object showRightPanel;
		private object bcView;
		private object unpinRecentItem;
		private bool? showHome;
		private bool? showDetailView;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public object NotesDesc
		{
			/// <summary>The method to get the notesDesc</summary>
			/// <returns>object representing the notesDesc</returns>
			get
			{
				return  this.notesDesc;

			}
			/// <summary>The method to set the value to notesDesc</summary>
			/// <param name="notesDesc">object</param>
			set
			{
				 this.notesDesc=value;

				 this.keyModified["notes_desc"] = 1;

			}
		}

		public object ShowRightPanel
		{
			/// <summary>The method to get the showRightPanel</summary>
			/// <returns>object representing the showRightPanel</returns>
			get
			{
				return  this.showRightPanel;

			}
			/// <summary>The method to set the value to showRightPanel</summary>
			/// <param name="showRightPanel">object</param>
			set
			{
				 this.showRightPanel=value;

				 this.keyModified["show_right_panel"] = 1;

			}
		}

		public object BcView
		{
			/// <summary>The method to get the bcView</summary>
			/// <returns>object representing the bcView</returns>
			get
			{
				return  this.bcView;

			}
			/// <summary>The method to set the value to bcView</summary>
			/// <param name="bcView">object</param>
			set
			{
				 this.bcView=value;

				 this.keyModified["bc_view"] = 1;

			}
		}

		public object UnpinRecentItem
		{
			/// <summary>The method to get the unpinRecentItem</summary>
			/// <returns>object representing the unpinRecentItem</returns>
			get
			{
				return  this.unpinRecentItem;

			}
			/// <summary>The method to set the value to unpinRecentItem</summary>
			/// <param name="unpinRecentItem">object</param>
			set
			{
				 this.unpinRecentItem=value;

				 this.keyModified["unpin_recent_item"] = 1;

			}
		}

		public bool? ShowHome
		{
			/// <summary>The method to get the showHome</summary>
			/// <returns>bool? representing the showHome</returns>
			get
			{
				return  this.showHome;

			}
			/// <summary>The method to set the value to showHome</summary>
			/// <param name="showHome">bool?</param>
			set
			{
				 this.showHome=value;

				 this.keyModified["show_home"] = 1;

			}
		}

		public bool? ShowDetailView
		{
			/// <summary>The method to get the showDetailView</summary>
			/// <returns>bool? representing the showDetailView</returns>
			get
			{
				return  this.showDetailView;

			}
			/// <summary>The method to set the value to showDetailView</summary>
			/// <param name="showDetailView">bool?</param>
			set
			{
				 this.showDetailView=value;

				 this.keyModified["show_detail_view"] = 1;

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