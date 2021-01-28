using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RINTE_Care.Models;
using RINTE_Care.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;

namespace RINTECare.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly UserApplications userApplications;
        private readonly ILogger<HomeController> _logger;
        private readonly Patients patients;
        private readonly EMails emails;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            userApplications = new UserApplications(_connectionString);
            patients = new Patients(_connectionString);
            emails = new EMails();
        }

        public IActionResult Index()
        {
            try
            {
                ViewData["Title"] = "Home Page";
                return ValidateOrLogin();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                HttpContext.Session.Clear();
                return RedirectToAction("HomeIndex");
            }
        }

        private IActionResult ValidateOrLogin()
        {
            try
            {
                if (!HttpContext.Session.IsAvailable)
                {
                    return RedirectToAction("HomeIndex");
                }

                if (!HttpContext.Session.Keys.Any(x => x == SessionInfo.UsernameKey) ||
                    !HttpContext.Session.Keys.Any(x => x == SessionInfo.RoleKey))
                {
                    HttpContext.Session.Clear();
                    return RedirectToAction("HomeIndex");
                }
                else
                {
                    string role = HttpContext.Session.GetString(SessionInfo.RoleKey);
                    return role switch
                    {
                        UserApplication.Administrator => RedirectToAction("Index", "ManagementAdministrator"),
                        UserApplication.Doctor => RedirectToAction("Index", "ManagementDoctors"),
                        UserApplication.Patient => RedirectToAction("Index", "ManagementPatient"),
                        _ => throw new ArgumentException("Tipo de Utilizador não esperado"),
                    };
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
                //Informar do erro
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }

        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Patient patient)
        {
            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                UserApplication userApplication = userApplications.Get(patient.Username);
                if (userApplication != default)
                {
                    ViewBag.Error = $"Username {patient.Username} already exists in our database. Please, select another username[email]";
                    return RedirectToAction("Register");
                }
                Patient patientInDatabase = patients.GetUsingTaxNumber(patient.TaxNumber);
                if (patientInDatabase != default)
                {
                    ViewBag.Error = $"Patient with this tax number {patient.TaxNumber} already exists in our database. Please check again the Tax Number inserted. If the problem persists, you may try to talk with our support team.";
                    return RedirectToAction("Register");
                }
                /********Fazer mais validacoes e fazer o mesmo***********/

                /*Fim de validacoes*/
                string password = userApplications.GetNewPassword();
                try
                {
                    userApplications.Add(
                        new UserApplication()
                        {
                            Username = patient.Username,
                            Role = UserApplication.Patient,
                            Active = true,
                            Password = password
                        }
                        );
                    patients.Add(patient);
                }
                catch (Exception e)
                {
                    ViewBag.Error = $"{e.StackTrace}";
                    _logger.LogError(e.StackTrace);
                    return View();
                }

                try
                {
                    emails.SendEmail(
                        new Email()
                        {
                            SenderEmail = EMails.EmailClinic,
                            Body = $"Dear {patient.FirstName} {patient.LastName},\n" +
                                   $"\nCongrats, you are set up on RINTECare Porto! Checkout our website and enjoy our Health+ blog.\n " +
                                   $"\nBest regards, \n" +
                                   $"\nRINTECare Porto",
                            Subject = "Welcome to RINTECare Porto",
                            DestinationName1 = patient.FirstName + " " + patient.LastName,
                            DestinationEmail1 = patient.Username
                        }
                        );
                }
                catch (Exception e)
                {
                    _logger.LogError(e.StackTrace);
                }
            }
            return View();
        }

        public IActionResult Health()
        {
            return View();
        }

        public IActionResult Healthp2()
        {
            return View();
        }

        public IActionResult Healthp3()
        {
            return View();
        }

        public IActionResult Logout()
        {
            
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult HomeIndex()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        
        public IActionResult Login()
        {
            if(TempData["info"]!=null)
            {
                ViewBag.Info = TempData["info"];
            }
            //read cookie from IHttpContextAccessor  
            if (HttpContext.Request.Cookies[CookiesInfo.UsernameKey] != null)
            {
                //string cookieValueFromContext = HttpContext.Request.Cookies[CokkiesInfo.UsernameKey];
                //read cookie from Request object  
                string username = Request.Cookies[CookiesInfo.UsernameKey];
                bool rememberMe = false;
                if(Request.Cookies[CookiesInfo.RememberMe] != default)
                {
                    if (Request.Cookies[CookiesInfo.RememberMe] == "True")
                    {
                        rememberMe = true;
                    }
                }
                if (userApplications.Get(username) != default)
                {
                    return View(new UserCredential()
                    {
                        Username = username,
                        RememberMe=rememberMe
                        
                    });
                }
                else
                    return View();
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserCredential user)
        {
            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                UserApplication userApplication = userApplications.GetUser(user);
                if (userApplication != default)
                {
                    if (user.RememberMe)
                    {
                        SettingCookie(userApplication.Username, user.RememberMe);
                    }
                    else
                    {
                        ResetCookie(userApplication.Username);
                    }
                    SettingSession(userApplication.Username, userApplication.Role);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Error = "User or password invalid";
            return View();
        }

        private void SettingCookie(string username, bool rememberMe)
        {
            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            
            Response.Cookies.Append(CookiesInfo.UsernameKey, username, option);
            Response.Cookies.Append(CookiesInfo.RememberMe, "True", option);
        }
        private void ResetCookie(string username)
        {
            Response.Cookies.Append(CookiesInfo.UsernameKey, username);
            Response.Cookies.Append(CookiesInfo.RememberMe, username);
        }

        [AllowAnonymous]
        public IActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Reset(OnlyUsername user)
        {
            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                UserApplication userApplication = userApplications.Get(user.Username);
                if (userApplication != default)
                {
                    try
                    {
                        userApplication.Password = userApplications.GetNewPassword();
                        userApplications.Update(userApplication);
                        emails.SendEmail(
                            new Email()
                            {
                                SenderEmail = EMails.EmailClinic,
                                Body = $"Dear Customer,\n" +
                                   $"\nYour new password is: {userApplication.Password}.\n" +
                                   $"\nFeel free to change your password in MyProfile -> Change Password.\n" +
                                   $"\nBest regards, \n" +
                                   $"\nRINTECare Porto",
                                Subject = "RINTECare Porto | New Password",
                                DestinationName1 = user.Username,
                                DestinationEmail1 = user.Username
                            }
                            );
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                        _logger.LogError(ex.StackTrace);
                        return View();
                    }
                    TempData["info"]= "Password reset was successful! Please, check your mail.";                    
                    return RedirectToAction("Login");
                }
            }
            ViewBag.Error = $"username:{user.Username} does not exist in our system. Please, check If you inserted your correct username or contact our support team.";
            return View();
        }


        private void SettingSession(string username, string role)
        {
            HttpContext.Session.SetString(SessionInfo.UsernameKey, username);
            HttpContext.Session.SetString(SessionInfo.RoleKey, role);
        }

    }
}
