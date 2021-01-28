using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RINTE_Care.Models
{
	public class DoctorAvailabilityView
	{
		public int id { get; set; }
		public int idDoctor { get; set; }
		[Display(Name = "Physician")]
		public string DoctorName { get; set; }

		public int idWeekDay { get; set; }
		[Display(Name = "Week Day")]
		public string WeekDay { get; set; }

		public int idSlot { get; set; }
		[Display(Name = "Slot Information")]
		public string SlotInformation { get; set; }
		[Display(Name = "Day Start")]		
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
		public DateTime DayStart { get; set; }
		[Display(Name = "Day End")]

		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
		public DateTime DayEnd { get; set; }
		[Display(Name = "Active")]
		public bool active { get; set; }

		public DoctorAvailabilityView() { }
		public DoctorAvailabilityView(int id_, int idDoctor_, int idWeekDay_, int idSlot_, DateTime DayStart_, DateTime DayEnd_, bool active_)
		{
			this.id = id_;
			this.idDoctor = idDoctor_;
			this.idWeekDay = idWeekDay_;
			this.idSlot = idSlot_;
			this.DayStart = DayStart_;
			this.DayEnd = DayEnd_;
			this.active = active_;
		}
	}
}
