using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Controllers
{
    public class ManagementAdministratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
