using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RINTECare.Controllers
{
    /// <summary>
    /// This is the painel that the doctor will see
    /// </summary>
    public class ManagementDoctorsController : Controller
    {
        readonly IConfiguration _configuration;

        public ManagementDoctorsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: ManagmentDoctorsController
        public ActionResult Index()
        {
            return View();
        }
    }
}
