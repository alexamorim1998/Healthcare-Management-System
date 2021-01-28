using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Models
{
    public class DoctorAppointmentView
    {
        public int id { get; set; }
        public int idDoctorAvailability { get; set; }
        public int idPatient { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateAppointment { get; set; }
        public State State { get; set; }
        [Display(Name = "Patient")]
        public string PatientName { get; set; }
        [Display(Name = "Physician")]
        public string DoctorName { get; set; }
        [Display(Name = "Week Day")]
        public string WeekdayString { get; set; }
        [Display(Name = "Hours")]
        public string SlotString { get; set; }

        public DoctorAppointmentView() { }
        
    }
}
