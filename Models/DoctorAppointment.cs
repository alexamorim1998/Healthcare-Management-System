using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace RINTE_Care.Models
{
    [Table("DoctorAppointment")]
    public class DoctorAppointment
    {
        [Dapper.Contrib.Extensions.Key]
        public int id { get; set; }
        public int idDoctorAvailability { get; set; }
        public int idPatient { get; set; }
        public DateTime DateAppointment { get; set; }
        public int State { get; set; }
        public DoctorAppointment() { }
        public DoctorAppointment(int id_, int idDoctorAvailability_, int idPatient_, DateTime DateAppointment_, int State_)
        {
            this.id = id_;
            this.idDoctorAvailability = idDoctorAvailability_;
            this.idPatient = idPatient_;
            this.DateAppointment = DateAppointment_;
            this.State = State_;
        }
    }
}
