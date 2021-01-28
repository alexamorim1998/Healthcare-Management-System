using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Models
{
    [Table("Patient")]
    public class Patient
    {
        [Dapper.Contrib.Extensions.Key]
        public int idPatient { get; set; }
        [StringLength(20)]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name needed", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name needed", AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Select a gender", AllowEmptyStrings = false)]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Insert your valid age", AllowEmptyStrings = false)]
        public int Age { get; set; }
        [Required(ErrorMessage = "Insert your valid height in centimeters (p.e. 180)", AllowEmptyStrings = false)]
        public string Height { get; set; }
        [Required(ErrorMessage = "Insert your valid weight in kilograms (p.e. 75)", AllowEmptyStrings = false)]
        public string Weight { get; set; }
        [Display(Name = "Blood Type")]
        [Required(ErrorMessage = "Select a blookd type", AllowEmptyStrings = false)]
        public string BloodType { get; set; }
        [Required(ErrorMessage = "Insert your allergies. In case you don't have introduce None", AllowEmptyStrings = false)]
        public string Allergies { get; set; }
        [Required(ErrorMessage = "Insert your job title", AllowEmptyStrings = false)]
        public string Job { get; set; }
        [Display(Name = "Tax Number")]
        [Required(ErrorMessage = "Tax Number needed", AllowEmptyStrings = false)]
        public int TaxNumber { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number needed", AllowEmptyStrings = false)]
        public int PhoneNumber { get; set; }
        [Required(ErrorMessage = "Insert your location", AllowEmptyStrings = false)]
        public string Location { get; set; }
        [Required(ErrorMessage = "Insert your job address", AllowEmptyStrings = false)]
        public string Address { get; set; }
        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Insert your zip code", AllowEmptyStrings = false)]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Insert your description", AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Insert your email", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Username { get; set; }
        public Patient()
        {

        }
        public Patient(int idPatient_, string FirstName_, string LastName_, string Gender_, int Age_, string Height_, string Weight_, string BloodType_, string Allergies_, string Job_, int TaxNumber_, int PhoneNumber_, string Location_, string Address_, string ZipCode_, string Description_, string Username_)
        {
            this.idPatient = idPatient_;
            this.FirstName = FirstName_;
            this.LastName = LastName_;
            this.Gender = Gender_;
            this.Age = Age_;
            this.Height = Height_;
            this.Weight = Weight_;
            this.BloodType = BloodType_;
            this.Allergies = Allergies_;
            this.Job = Job_;
            this.TaxNumber = TaxNumber_;
            this.PhoneNumber = PhoneNumber_;
            this.Location = Location_;
            this.Address = Address_;
            this.ZipCode = ZipCode_;
            this.Description = Description_;
            this.Username = Username_;
        }
    }
}
