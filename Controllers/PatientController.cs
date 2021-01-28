using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RINTE_Care.Data;
using RINTE_Care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RINTE_Care.Controllers
{
    public class PatientController : Controller
    {
        readonly IConfiguration _configuration;
        private readonly Patients patients;
        private readonly string _connectionString;
        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger,IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            patients = new Patients(_connectionString);
        }

        //view razor edit
        public ActionResult GetProfile()
        {
            string username = HttpContext.Session.GetString(SessionInfo.UsernameKey);
            return View(patients.Get(username));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProfile(Patient patient)
        {
            try
            {
                patients.Update(patient);
                return RedirectToAction("Index", "ManagementPatient");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.StackTrace;
                return View(patient);
            }
        }


        // GET: DoctorController
        public ActionResult Index()
        {
            return View(patients.GetAll());
        }

        // GET: DoctorController/Details/5
        public ActionResult Details(int id)
        {
            return View(patients.Get(id));
        }

        // GET: DoctorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient)
        {
            try
            {
                patients.Add(patient);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        // GET: DoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(patients.Get(id));
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Patient patient)
        {
            try
            {
                patients.Update(patient);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(patients.Get(id));
        }

        // POST: AdministratorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Patient patient = patients.Get(id);
                patients.Delete(patient.Username);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "User is in use in database can not be deleted!!";
                _logger.LogDebug(ex.StackTrace);
                return View(patients.Get(id));
            }
        }

    }

}
