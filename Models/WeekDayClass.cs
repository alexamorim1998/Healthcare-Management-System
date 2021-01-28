using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RINTE_Care.Models
{
	[Table("WeekDay")]
	public class WeekDayClass
	{
		[Dapper.Contrib.Extensions.Key]

		public int idWeekDay { get; set; }
		[Display(Name = "Week Day")]
		public int WeekDay { get; set; }
		public string Name { get; set; }
		public WeekDayClass() { }
		public WeekDayClass(int idWeekDay_, int WeekDay_, string Name_)
		{
			this.idWeekDay = idWeekDay_;
			this.WeekDay = WeekDay_;
			this.Name = Name_;
		}
	}
}
