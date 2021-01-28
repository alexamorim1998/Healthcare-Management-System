using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RINTE_Care.Models
{
    public class AppoimentDateView
    {
        public long idAvailability { get; set; }
        [Display(Name = "Date")]
        public DateTime date { get; set; }
        public string[] datesAvailables;
    }
}
