using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Models
{
    public enum State
    {
        [Display(Name = "Created")]
        Create =0,
        [Display(Name = "Held")]
        Accomplish = 1,
        [Display(Name = "Cancelled by Client")]
        Canceled_by_Client = 2,
        [Display(Name = "Cancelled by Doctor")]
        Canceled_by_Doctor = 3,
        [Display(Name = "Cancelled by Administrator")]
        Canceled_by_Administrator = 4,
    }
}
