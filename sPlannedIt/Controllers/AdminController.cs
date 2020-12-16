using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Models;
using sPlannedIt.Viewmodels.Company_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class AdminController : Controller
    {


        public IActionResult IndexAdmin()
        {
            return View();
        }

    }
}
