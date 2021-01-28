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
    public class AvailabilityController : Controller
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


        public AvailabilityController(IConfiguration configuration)
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

        // GET: AvailabilityController
        public ActionResult Index()
        {
            return View(doctorAvailabilitys.GetAll());
        }

        // GET: AvailabilityController/Details/5
        public ActionResult Details(long id)
        {
            return View(doctorAvailabilitys.Get(id));
        }

        // GET: AvailabilityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AvailabilityController/Create
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

        // GET: AvailabilityController/Edit/5
        public ActionResult Edit(long id)
        {
            return View(doctorAvailabilitys.Get(id));
        }

        // POST: AvailabilityController/Edit/5
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

        // GET: AvailabilityController/Delete/5
        public ActionResult Delete(long id)
        {
            return View(doctorAvailabilitys.Get(id));
        }

        // POST: AvailabilityController/Delete/5
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
