using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Models
{
    [Table("Specialization")]
    public class Specialization
    {
        [Required(ErrorMessage = "Id needed", AllowEmptyStrings = false)]
        [Dapper.Contrib.Extensions.Key]
        public int idSpecialization { get; set; }

        [Required(ErrorMessage = "Name needed", AllowEmptyStrings = false)]
        public string Name { get; set; }
        public Specialization()
        {

        }


    }
}
