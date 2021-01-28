using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RINTE_Care.Data;
using RINTE_Care.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Controllers
{
    public class DoctorAppointmentController : Controller
    {
        readonly IConfiguration _configuration;
        private readonly Doctors doctors;
        private readonly WeekDays weekDays;
        private readonly Slots slots;
        private readonly DoctorAvailabilitys doctorAvailabilitys;
        private readonly DoctorAppointments doctorAppointments;
        private readonly Patients patients;

        private readonly IEnumerable<WeekDayClass> listOfweekdays;
        private readonly IEnumerable<Slot> listOfSlots;
        private readonly string _connectionString;
        private readonly Specializations specializations;


        public DoctorAppointmentController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            doctors = new Doctors(_connectionString);
            weekDays = new WeekDays(_connectionString);
            slots = new Slots(_connectionString);
            doctorAvailabilitys = new DoctorAvailabilitys(_connectionString);
            doctorAppointments = new DoctorAppointments(_connectionString);
            patients = new Patients(_connectionString);
            specializations = new Specializations(_connectionString);

            listOfweekdays = weekDays.GetAll();
            listOfSlots = slots.GetAll();
        }

        public ActionResult FindPatient()
        {
            return View(patients.GetAll());
        }

        public ActionResult FindByName(string searchString)
        {
            return View(patients.GetAll(searchString));
        }
        

        public ActionResult SelectWeekDay(long idPatient)
        {
            string usernameDoctor = HttpContext.Session.GetString(SessionInfo.UsernameKey);
            ViewBag.idPatient = idPatient;
            return View(doctors.GetAllWeekDays(usernameDoctor));
        }

        public ActionResult SelectAvailability(long idPatient, long idWeekDay)
        {
            string usernameDoctor = HttpContext.Session.GetString(SessionInfo.UsernameKey);
            ViewBag.idPatient = idPatient;
            return View(doctors.GetAllFutureAvailabilities(usernameDoctor, idWeekDay));
        }

        public ActionResult SelectDate(long idPatient, long idAvailability)
        {
            var availability = doctorAvailabilitys.Get(idAvailability);
            IEnumerable<DoctorAppointment> appointments = this.doctorAppointments.GetFutureAppoimentsForAvailability(idAvailability);
            List<string> list = new List<string>();
            DateTime startDate = availability.DayStart > DateTime.Today ? availability.DayStart : DateTime.Today;
            
            for (DateTime date = startDate; date <= availability.DayEnd; date = date.AddDays(1))
            {
                if (((int)date.DayOfWeek) == availability.idWeekDay &&
                    !appointments.Select(x => x.DateAppointment).Contains(date))
                {
                    list.Add(date.ToString("d-M-yyyy"));
                }
            }

            AppoimentDoctorDateView appoimentDateView = new AppoimentDoctorDateView()
            {
                idAvailability = idAvailability,
                idPatient=idPatient,
                date = list.Any() ? DateTime.ParseExact(list.First(), "d-M-yyyy", CultureInfo.InvariantCulture) : DateTime.Today,
                datesAvailables = list.ToArray()
            };
            return View(appoimentDateView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectDate(AppoimentDoctorDateView appoimentDateView)
        {
            try
            {
                DoctorAppointment doctorAppointment = new DoctorAppointment()
                {
                    DateAppointment = appoimentDateView.date,
                    idDoctorAvailability = (int)appoimentDateView.idAvailability,
                    idPatient = (int)appoimentDateView.idPatient,
                    State = (int)State.Create
                };
                doctorAppointments.Add(doctorAppointment);
                return RedirectToAction("IndexDoctor", "Appointment");
            }
            catch
            {
                return View();
            }
        }


        // GET: DoctorAppointmentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DoctorAppointmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DoctorAppointmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorAppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorAppointmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorAppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorAppointmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorAppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
