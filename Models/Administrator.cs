using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Models
{
	[Table("Administrator")]
	public class Administrator
	{
		[Dapper.Contrib.Extensions.Key]
		public int idAdministrator { get; set; }
		[Display(Name = "Fist Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		public string Gender { get; set; }
		public int Age { get; set; }
		[Display(Name = "Tax Number")]
		public int TaxNumber { get; set; }
		[Display(Name = "Phone Number")]
		public int PhoneNumber { get; set; }
		public string Location { get; set; }
		public string Address { get; set; }
		[Display(Name = "Zip Code")]
		public string ZipCode { get; set; }
		[Required(ErrorMessage = "Email needed", AllowEmptyStrings = false)]
		public string Username { get; set; }

        public Administrator()
        {

        }
		public Administrator(int idAdministrator_, string FirstName_, string LastName_, string Gender_, int Age_, int TaxNumber_, int PhoneNumber_, string Location_, string Address_, string ZipCode_, string Username_)
		{
			this.idAdministrator = idAdministrator_;
			this.FirstName = FirstName_;
			this.LastName = LastName_;
			this.Gender = Gender_;
			this.Age = Age_;
			this.TaxNumber = TaxNumber_;
			this.PhoneNumber = PhoneNumber_;
			this.Location = Location_;
			this.Address = Address_;
			this.ZipCode = ZipCode_;
			this.Username = Username_;
		}
	}
}
