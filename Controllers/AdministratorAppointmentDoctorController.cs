using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RINTE_Care.Data;
using RINTE_Care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Controllers
{
    public class AdministratorAppointmentDoctorController : Controller
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


        public AdministratorAppointmentDoctorController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            doctors = new Doctors(_connectionString);
            weekDays = new WeekDays(_connectionString);
            slots = new Slots(_connectionString);
            doctorAvailabilitys = new DoctorAvailabilitys(_connectionString);
            doctorAppointments = new DoctorAppointments(_connectionString);
            patients = new Patients(_connectionString);

            listOfweekdays = weekDays.GetAll();
            listOfSlots = slots.GetAll();
        }
        // GET: AdministratorAppointmentDoctorController
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ProcurarMedico()
        {
            return View(doctors.GetAll());
        }


        public ActionResult ShowAvailabilityOfDoctor(int id)
        {
            ViewBag.idDoctor = id;
            //idDoctor = id;
            return View(doctors.GetAvailabilityOfDoctorTrue(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of availabilty</param>
        /// <returns></returns>
        public ActionResult FindPatient(int id)
        {
            ViewBag.idAvailability = id;
            return View(patients.GetAll());
        }

        public ActionResult FindByName(long idAvailability, string searchString)
        {
            ViewBag.idAvailability = idAvailability;
            return View(patients.GetAll(searchString));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id Patient</param>
        /// <param name="idAvailability">id Availabity</param>
        /// <returns></returns>

        // é como se fosse GET do EDIT
        public ActionResult Appointment(int id, int idAvailability)
        {
            SmallAppointmenView smallAppointmenView = new SmallAppointmenView()
            {
                idAvailability = idAvailability,
                idPatient=id,
                dateAppointment= DateTime.Now
            };
            return View(smallAppointmenView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Appointment(SmallAppointmenView smallAppointmenView)
        {
            try
            {
                doctorAppointments.Add(new DoctorAppointment()
                {
                    DateAppointment=smallAppointmenView.dateAppointment,
                    idDoctorAvailability=smallAppointmenView.idAvailability,
                    idPatient=smallAppointmenView.idPatient,
                    State= (int)State.Create
                });
                return RedirectToAction("Index", "ManagementAdministrator");
                
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(smallAppointmenView);
            }
        }


        // GET: AdministratorAppointmentDoctorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdministratorAppointmentDoctorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdministratorAppointmentDoctorController/Create
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

        // GET: AdministratorAppointmentDoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdministratorAppointmentDoctorController/Edit/5
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

        // GET: AdministratorAppointmentDoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdministratorAppointmentDoctorController/Delete/5
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
