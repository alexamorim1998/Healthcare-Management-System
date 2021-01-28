using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Models
{
    public class OnlyUsername
    {
        [Required(ErrorMessage = "Email needed", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Username { get; set; }
    }
}
