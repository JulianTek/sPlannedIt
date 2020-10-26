using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Data.Models;
using sPlannedIt.Logic.Models;
using sPlannedIt.Models;
using sPlannedIt.Viewmodels.Company_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class AdminController : Controller
    {
        private readonly CompanyContainer _container = new CompanyContainer();

  
        public IActionResult IndexAdmin()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCompany(CreateCompanyViewModel model)
        {
            Company company = _container.CreateCompany(model.CompanyName);
            if (company != null)
            {
                Logic.CompanyManager_Logic.AddCompanyDto(company.CompanyID, company.CompanyName);
                return RedirectToAction("ListCompanies", "Admin");
            }
            
            return View();
        }

        public IActionResult ListCompanies()
        {
            var model = _container.AllCompanies;

            return View(model);
        }

        public IActionResult CompanyDetails(string companyId)
        {
            var model = _container.FindCompany(companyId);
            return View(model);
        }

    }
}
