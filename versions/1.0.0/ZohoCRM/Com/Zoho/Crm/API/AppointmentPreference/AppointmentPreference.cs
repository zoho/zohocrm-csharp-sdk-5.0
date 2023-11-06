using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.AppointmentPreference
{

	public class AppointmentPreference : Model
	{
		private bool? showJobSheet;
		private Choice<string> whenDurationExceeds;
		private Choice<string> whenAppointmentCompleted;
		private bool? allowBookingOutsideServiceAvailability;
		private bool? allowBookingOutsideBusinesshours;
		private Dictionary<string, object> dealRecordConfiguration;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? ShowJobSheet
		{
			/// <summary>The method to get the showJobSheet</summary>
			/// <returns>bool? representing the showJobSheet</returns>
			get
			{
				return  this.showJobSheet;

			}
			/// <summary>The method to set the value to showJobSheet</summary>
			/// <param name="showJobSheet">bool?</param>
			set
			{
				 this.showJobSheet=value;

				 this.keyModified["show_job_sheet"] = 1;

			}
		}

		public Choice<string> WhenDurationExceeds
		{
			/// <summary>The method to get the whenDurationExceeds</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  this.whenDurationExceeds;

			}
			/// <summary>The method to set the value to whenDurationExceeds</summary>
			/// <param name="whenDurationExceeds">Instance of Choice<string></param>
			set
			{
				 this.whenDurationExceeds=value;

				 this.keyModified["when_duration_exceeds"] = 1;

			}
		}

		public Choice<string> WhenAppointmentCompleted
		{
			/// <summary>The method to get the whenAppointmentCompleted</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  this.whenAppointmentCompleted;

			}
			/// <summary>The method to set the value to whenAppointmentCompleted</summary>
			/// <param name="whenAppointmentCompleted">Instance of Choice<string></param>
			set
			{
				 this.whenAppointmentCompleted=value;

				 this.keyModified["when_appointment_completed"] = 1;

			}
		}

		public bool? AllowBookingOutsideServiceAvailability
		{
			/// <summary>The method to get the allowBookingOutsideServiceAvailability</summary>
			/// <returns>bool? representing the allowBookingOutsideServiceAvailability</returns>
			get
			{
				return  this.allowBookingOutsideServiceAvailability;

			}
			/// <summary>The method to set the value to allowBookingOutsideServiceAvailability</summary>
			/// <param name="allowBookingOutsideServiceAvailability">bool?</param>
			set
			{
				 this.allowBookingOutsideServiceAvailability=value;

				 this.keyModified["allow_booking_outside_service_availability"] = 1;

			}
		}

		public bool? AllowBookingOutsideBusinesshours
		{
			/// <summary>The method to get the allowBookingOutsideBusinesshours</summary>
			/// <returns>bool? representing the allowBookingOutsideBusinesshours</returns>
			get
			{
				return  this.allowBookingOutsideBusinesshours;

			}
			/// <summary>The method to set the value to allowBookingOutsideBusinesshours</summary>
			/// <param name="allowBookingOutsideBusinesshours">bool?</param>
			set
			{
				 this.allowBookingOutsideBusinesshours=value;

				 this.keyModified["allow_booking_outside_businesshours"] = 1;

			}
		}

		public Dictionary<string, object> DealRecordConfiguration
		{
			/// <summary>The method to get the dealRecordConfiguration</summary>
			/// <returns>Dictionary representing the dealRecordConfiguration<String,Object></returns>
			get
			{
				return  this.dealRecordConfiguration;

			}
			/// <summary>The method to set the value to dealRecordConfiguration</summary>
			/// <param name="dealRecordConfiguration">Dictionary<string,object></param>
			set
			{
				 this.dealRecordConfiguration=value;

				 this.keyModified["deal_record_configuration"] = 1;

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