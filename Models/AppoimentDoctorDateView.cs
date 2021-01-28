using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RINTE_Care.Models
{
    public class AppoimentDoctorDateView
    {
        public long idAvailability { get; set; }
        public long idPatient { get; set; }
        [Display(Name = "Date")]
        public DateTime date { get; set; }
        public string[] datesAvailables;
    }
}
