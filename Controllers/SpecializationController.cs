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
    public class SpecializationController : Controller
    {
        readonly IConfiguration _configuration;
        private readonly Specializations specializations;
        private readonly string _connectionString;

        public SpecializationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            specializations = new Specializations(_connectionString);
        }

        // GET: SpecializationController
        public ActionResult Index()
        {
            return View(specializations.GetAll());
        }

        // GET: SpecializationController/Details/5
        public ActionResult Details(int id)
        {
            return View(specializations.Get(id));
        }

        // GET: SpecializationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpecializationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Specialization specialization)
        {
            try
            {
                specializations.Add(specialization);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        // GET: SpecializationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(specializations.Get(id));
        }

        // POST: SpecializationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Specialization specialization)
        {
            try
            {
                specializations.Update(specialization);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        // GET: SpecializationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(specializations.Get(id));
        }

        // POST: SpecializationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                specializations.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }
    }
}
