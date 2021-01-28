using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace RINTE_Care.Models
{
	[Table("Slots")]
	public class Slot
	{
		[Dapper.Contrib.Extensions.Key]
		public int idSlot { get; set; }
		[Display(Name = "Start Hour")]
		public TimeSpan StartHour { get; set; }
		[Display(Name = "End Hour")]
		public TimeSpan EndHour { get; set; }
		public Slot() { }
		public Slot(int idSlot_, TimeSpan StartHour_, TimeSpan EndHour_)
		{
			this.idSlot = idSlot_;
			this.StartHour = StartHour_;
			this.EndHour = EndHour_;
		}
	}
}
