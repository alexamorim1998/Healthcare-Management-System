using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RINTE_Care.Data;
using RINTE_Care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RINTECare.Controllers
{
    public class AdministratorController : Controller
    {
        readonly IConfiguration _configuration;
        private readonly Administrators administrators;
        private readonly string _connectionString;

        public AdministratorController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            administrators = new Administrators(_connectionString);
        }

        // GET: DoctorController
        public ActionResult Index()
        {
            return View(administrators.GetAll());
        }

        // GET: DoctorController/Details/5
        public ActionResult Details(int id)
        {
            return View(administrators.Get(id));
        }

        // GET: DoctorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Administrator doctor)
        {
            try
            {
                administrators.Add(doctor);
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
            return View(administrators.Get(id));
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Administrator administrator)
        {
            try
            {
                administrators.Update(administrator);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        // GET: DoctorController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View(administrators.Get(id));
        //}

        //// POST: DoctorController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, Administrator doctor)
        //{
        //    try
        //    {
        //        administrators.Delete(id);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception e)
        //    {
                
        //        ViewBag.Error = e.Message;
        //        return View();
        //    }
        //}
    }
}
