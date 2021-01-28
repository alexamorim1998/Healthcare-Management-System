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
    public class DoctorController : Controller
    {

        readonly IConfiguration _configuration;
        private readonly Doctors doctors;
        private readonly Specializations specializations;
        private readonly string _connectionString;

        public DoctorController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            doctors = new Doctors(_connectionString);
            specializations = new Specializations(_connectionString);
        }

        // GET: DoctorController
        public ActionResult Index()
        {
            ViewBag.specializations = specializations.GetAll();
            return View(doctors.GetAll());
        }

        //view razor edit
        public ActionResult GetProfile()
        {
            string username = HttpContext.Session.GetString(SessionInfo.UsernameKey);
            return View(doctors.Get(username));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProfile(Doctor doctor)
        {
            try
            {
                doctors.Update(doctor);
                return RedirectToAction("Index", "ManagementDoctors");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.StackTrace;
                return View(doctor);
            }
        }

        // GET: DoctorController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.specializations = specializations.GetAll();
            return View(doctors.Get(id));
        }

        // GET: DoctorController/Create
        public ActionResult Create()
        {
            ViewBag.specializations = specializations.GetAll();
            return View();            
        }

        // POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Doctor doctor)
        {
            try
            {
                doctors.Add(doctor);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ViewBag.specializations = specializations.GetAll();
                ViewBag.Error = e.Message;
                return View();
            }
        }

        // GET: DoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.specializations = specializations.GetAll();
            return View(doctors.Get(id));
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Doctor doctor)
        {
            try
            {
                doctors.Update(doctor);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ViewBag.specializations = specializations.GetAll();
                ViewBag.Error = e.Message;
                return View();
            }
        }

        // GET: DoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.specializations = specializations.GetAll();
            return View(doctors.Get(id));
        }

        // POST: DoctorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Doctor doctor)
        {
            try
            {
                doctors.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.specializations = specializations.GetAll();
                ViewBag.Error = e.Message;
                return View();
            }
        }

        public ActionResult CreateAvailability()
        {
            string username = HttpContext.Session.GetString(SessionInfo.UsernameKey);
            var doctor = doctors.Get(username);
            ViewBag.idDoctor = doctor.idDoctor;
            return View(doctors.GetAvailabilityOfDoctor(doctor.idDoctor));
        }
    }
}
