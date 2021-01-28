using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace RINTE_Care.Models
{

	[Table("DoctorAvailability")]
	public class DoctorAvailability
	{
		[Dapper.Contrib.Extensions.Key]
		public int id { get; set; }
		public int idDoctor { get; set; }
		public int idWeekDay { get; set; }
		public int idSlot { get; set; }
		[Display(Name = "Day Start")]
		public DateTime DayStart { get; set; }
		[Display(Name = "Day End")]
		public DateTime DayEnd { get; set; }
		[Display(Name = "Active")]
		public bool active { get; set; }

		public DoctorAvailability() { }
		public DoctorAvailability(int id_, int idDoctor_, int idWeekDay_, int idSlot_, DateTime DayStart_, DateTime DayEnd_, bool active_)
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
