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
    public class UserController : Controller
    {
        readonly IConfiguration _configuration;
        private readonly UserApplications userApplications;
        private readonly string _connectionString;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            userApplications = new UserApplications(_connectionString);
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View(userApplications.GetAll());
        }

        public ActionResult ChangePassword()
        {
            string username = HttpContext.Session.GetString(SessionInfo.UsernameKey);
            return View(userApplications.Get(username));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserApplication userApplication)
        {
            var userOld = userApplications.Get(userApplication.ID);
            if (userOld.Password != userApplication.Password)
            {
                userApplications.Update(userApplication);
                return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                ViewBag.Error = "The password inserted is the same. Please, provide a new password.";
                return View(userOld);
            }
        }


        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View(userApplications.Get(id));
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserApplication userApplication)
        {
            try
            {
                userApplications.Add(userApplication);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(userApplications.Get(id));
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserApplication userApplication)
        {
            try
            {
                userApplications.Update(userApplication);
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
            return View(userApplications.Get(id));
        }

        // POST: SpecializationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                userApplications.Delete(id);
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
