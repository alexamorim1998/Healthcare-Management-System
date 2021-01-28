using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RINTE_Care.Models
{
    public class SmallAppointmenView
    {
        [Display(Name = "Email address")]
        public int idPatient { get; set; }
        public int idAvailability { get; set; }

        public DateTime dateAppointment { get; set; }
        public SmallAppointmenView() { }
    }
}
