using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Models
{
    [Table("UserApplication")]
    public class UserApplication
    {
        public const string Administrator = "Administrator";
        public const string Patient = "Patient";
        public const string Doctor = "Doctor";
        public const string DefaultPassword="11111";
        [Dapper.Contrib.Extensions.Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Email needed", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Password needed", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role needed", AllowEmptyStrings = false)]
        public string Role { get; set; }

        public bool Active { get; set; } = true;

        public UserApplication()
        {

        }

        public UserApplication(int id_, string Username_, string Password_, string Role_, bool Active_)
        {
            this.ID = id_;
            this.Username = Username_;
            this.Password = Password_;
            this.Role = Role_;
            this.Active = Active_;
        }



    }
}
