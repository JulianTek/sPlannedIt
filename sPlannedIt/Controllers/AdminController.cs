using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Data.Models;
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


        [HttpGet]
        public IActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCompany(CreateCompanyViewModel model)
        {
            Company company = convertDtOtoCompany(Logic.CompanyManager_Logic.AddCompanyDto(Guid.NewGuid().ToString(), model.CompanyName));
            if (company != null)
            {
                return RedirectToAction("ListCompanies", "Admin");
            }
            
            return View();
        }

        public IActionResult ListCompanies()
        {
            ListCompaniesViewmodel model = new ListCompaniesViewmodel()
            {
                Companies = new List<Company>()
            };
            foreach (CompanyDTO dto in Logic.CompanyManager_Logic.FindAllCompanies())
            {
                model.Companies.Add(convertDtOtoCompany(dto));
            }

            return View(model);
        }


        private Company convertDtOtoCompany(CompanyDTO dto)
        {
            if (dto != null)
            {
                return new Company()
                {
                    CompanyID = dto.CompanyID,
                    CompanyName = dto.CompanyName
                };
            }

            return null;
        }
    }
}
