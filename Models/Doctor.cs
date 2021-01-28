using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Models
{
	[Table("Doctor")]
	public class Doctor
	{
		[Dapper.Contrib.Extensions.Key]
		public int idDoctor { get; set; }
		[StringLength(20)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Required]
		[StringLength(20)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		public string Gender { get; set; }
		public int Age { get; set; }
		[Display(Name = "Tax Number")]
		public int TaxNumber { get; set; }
		[Display(Name = "License Number")]
		public int ProfessionalLicenseNumber { get; set; }
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
		public string Location { get; set; }
		public string Address { get; set; }
		[Display(Name = "Zip Code")]
		public string ZipCode { get; set; }
		public int idSpecialization { get; set; }
		
		[Required(ErrorMessage = "Email needed", AllowEmptyStrings = false)]
		public string Username { get; set; }
		public Doctor()
        {

        }
		public Doctor(int idDoctor_, string FirstName_, string LastName_, string Gender_, int Age_, int TaxNumber_, int ProfessionalLicenseNumber_, string PhoneNumber_, string Location_, string Address_, string ZipCode_, int idSpecialization_, string Username_)
		{
			this.idDoctor = idDoctor_;
			this.FirstName = FirstName_;
			this.LastName = LastName_;
			this.Gender = Gender_;
			this.Age = Age_;
			this.TaxNumber = TaxNumber_;
			this.ProfessionalLicenseNumber = ProfessionalLicenseNumber_;
			this.PhoneNumber = PhoneNumber_;
			this.Location = Location_;
			this.Address = Address_;
			this.ZipCode = ZipCode_;
			this.idSpecialization = idSpecialization_;
			this.Username = Username_;
		}
	}


}
