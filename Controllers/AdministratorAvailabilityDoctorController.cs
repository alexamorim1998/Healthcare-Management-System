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
    public class AdministratorAvailabilityDoctorController : Controller
    {
        readonly IConfiguration _configuration;
        private readonly Doctors doctors;
        private readonly WeekDays weekDays;
        private readonly Slots slots;
        private readonly DoctorAvailabilitys doctorAvailabilitys;

        private readonly IEnumerable<WeekDayClass> listOfweekdays;
        private readonly IEnumerable<Slot> listOfSlots;
        private readonly string _connectionString;

        
        public AdministratorAvailabilityDoctorController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            doctors = new Doctors(_connectionString);
            weekDays = new WeekDays(_connectionString);
            slots = new Slots(_connectionString);
            doctorAvailabilitys = new DoctorAvailabilitys(_connectionString);

            listOfweekdays = weekDays.GetAll();
            listOfSlots = slots.GetAll();
        }

        public ActionResult ProcurarMedico()
        {
            return View(doctors.GetAll());
        }

        /// <summary>
        /// Show all availability of doctor
        /// </summary>
        /// <param name="id">id from doctors</param>
        /// <returns></returns>
        public ActionResult ShowAvailabilityOfDoctor(int id)
        {
            ViewBag.idDoctor = id;
            //idDoctor = id;
            return View(doctors.GetAvailabilityOfDoctor(id));
        }
        

        // GET: AdministratorAvailabilityDoctorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdministratorAvailabilityDoctorController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.listOfweekdays = listOfweekdays;
            ViewBag.listOfSlots = listOfSlots;

            var result = doctors.GetAvailability(id);
            if (result == default)
                return View();
            else
                return View(result);
        }

        // GET: AdministratorAvailabilityDoctorController/Create
        //public ActionResult Create()
        //{
        //    ViewBag.listOfweekdays = listOfweekdays;
        //    ViewBag.listOfSlots = listOfSlots;
            
        //    return View();
        //}

        public ActionResult Create(int id)
        {
            ViewBag.listOfweekdays = listOfweekdays;
            ViewBag.listOfSlots = listOfSlots;
            ViewBag.idDoctor = id;            
            return View();
        }

        // POST: AdministratorAvailabilityDoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, DoctorAvailabilityView doctorAvailabilityView)
        {
            try
            {
                
                DoctorAvailability doctorAvailability = new DoctorAvailability()
                {
                    id = doctorAvailabilityView.id,
                    idSlot = doctorAvailabilityView.idSlot,

                    idWeekDay = doctorAvailabilityView.idWeekDay,
                    idDoctor = id,
                    active = doctorAvailabilityView.active,
                    DayStart = doctorAvailabilityView.DayStart,
                    DayEnd = doctorAvailabilityView.DayEnd
                };
                doctorAvailabilitys.Add(doctorAvailability);
                return RedirectToAction(nameof(ShowAvailabilityOfDoctor), new { id =id });
            }
            catch
            {
                ViewBag.listOfweekdays = listOfweekdays;
                ViewBag.listOfSlots = listOfSlots;
                return View();
            }
        }


        public ActionResult CreateByDoctor(int id)
        {
            ViewBag.listOfweekdays = listOfweekdays;
            ViewBag.listOfSlots = listOfSlots;
            ViewBag.idDoctor = id;
            return View();
        }

        // POST: AdministratorAvailabilityDoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateByDoctor(int id, DoctorAvailabilityView doctorAvailabilityView)
        {
            try
            {

                DoctorAvailability doctorAvailability = new DoctorAvailability()
                {
                    id = doctorAvailabilityView.id,
                    idSlot = doctorAvailabilityView.idSlot,

                    idWeekDay = doctorAvailabilityView.idWeekDay,
                    idDoctor = id,
                    active = doctorAvailabilityView.active,
                    DayStart = doctorAvailabilityView.DayStart,
                    DayEnd = doctorAvailabilityView.DayEnd
                };
                doctorAvailabilitys.Add(doctorAvailability);
                return RedirectToAction("CreateAvailability","Doctor"  , new { id = id });
            }
            catch
            {
                ViewBag.listOfweekdays = listOfweekdays;
                ViewBag.listOfSlots = listOfSlots;
                return View();
            }
        }


        // GET: AdministratorAvailabilityDoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = doctors.GetAvailability(id);
            ViewBag.listOfweekdays = listOfweekdays;
            ViewBag.listOfSlots = listOfSlots;
            if (result == default)
                return View();
            else
                return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorAvailabilityView doctorAvailabilityView)
        {
            try
            {
                DoctorAvailability doctorAvailability = new DoctorAvailability()
                {
                    id = doctorAvailabilityView.id,
                    idSlot =doctorAvailabilityView.idSlot,
                    idWeekDay= doctorAvailabilityView.idWeekDay,
                    idDoctor = doctorAvailabilityView.idDoctor,
                    active = doctorAvailabilityView.active,
                    DayStart = doctorAvailabilityView.DayStart,
                    DayEnd = doctorAvailabilityView.DayEnd
                };

                doctorAvailabilitys.Update(doctorAvailability);
                return RedirectToAction(nameof(ShowAvailabilityOfDoctor), new { id = doctorAvailability.idDoctor });
                //return RedirectToAction(
                //    nameof(ShowAvailabilityOfDoctor),
                //    "AdministratorAvailabilityDoctor", new { @id = doctorAvailability.idDoctor});
            }
            catch
            {
                ViewBag.listOfweekdays = listOfweekdays;
                ViewBag.listOfSlots = listOfSlots;
                return View();
            }
        }


        // GET: AdministratorAvailabilityDoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = doctors.GetAvailability(id);
            ViewBag.idDoctor = result.idDoctor;
            if (result == default)
                return View();
            else
                return View(result);
        }

        // POST: AdministratorAvailabilityDoctorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DoctorAvailabilityView doctorAvailabilityView)
        {
            try
            {
                doctorAvailabilitys.Delete(doctorAvailabilityView.id);
                return RedirectToAction(nameof(ShowAvailabilityOfDoctor), new { id = doctorAvailabilityView.idDoctor });
            }
            catch
            {
                return View();
            }
        }
    }
}
