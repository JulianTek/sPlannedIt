using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Logic;
using sPlannedIt.Logic.Models;
using sPlannedIt.Models;
using sPlannedIt.Viewmodels.Company_Viewmodels;
using sPlannedIt.Viewmodels.Role_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class CompanyController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CompanyCollection _collection;

        public CompanyController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _collection = new CompanyCollection();
        }
        [HttpGet]
        public IActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCompany(CreateCompanyViewModel model)
        {
            Company company = new Company(model.CompanyName);
            if (company != null)
            {
                if (!_collection.CheckIfCompanyNameExists(company))
                {
                    _collection.Create(company);
                    return RedirectToAction("RegisterEmployer", "Account", new { id = company.CompanyID });
                }
                ModelState.AddModelError("", "Company name already exists");
            }

            return View();
        }

        public IActionResult ListCompanies()
        {
            var model = _collection.GetAll();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCompany(string id)
        {
            foreach (var userid in _collection.GetAllEmployees(_collection.GetCompany(id)))
            {
                var user = await _userManager.FindByIdAsync(userid);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            _collection.Delete(_collection.GetCompany(id));
            return RedirectToAction("ListCompanies");
        }

        public async Task<IActionResult> CompanyDetails(string companyId)
        {
            Company company = _collection.GetCompany(companyId);
            List<CompanyDetailEmployeeData> data = new List<CompanyDetailEmployeeData>();
            foreach (string id in _collection.GetAllEmployees(company))
            {
                data.Add(new CompanyDetailEmployeeData()
                {
                    User = await _userManager.FindByIdAsync(id)
                });
            }
            CompanyDetailsViewmodel model = new CompanyDetailsViewmodel()
            {
                Company = company,
                EmployeeData = data
            };
            model.Company.Employees = _collection.GetAllEmployees(company);
            return View(model);
        }

        [HttpGet]
        public IActionResult EditUsersInCompany(string companyId)
        {
            ViewBag.companyId = companyId;

            var company = _collection.GetCompany(companyId);
            if (company == null)
            {
                //Todo: implement error view
            }

            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                userRoleViewModel.IsSelected = _collection.CheckIfEmployeeIsInCompany(company, user.Id);
                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditCompany(string companyId)
        {
            var company = _collection.GetCompany(companyId);
            EditCompanyViewmodel model = new EditCompanyViewmodel()
            {
                CompanyID = company.CompanyID,
                CompanyName = company.CompanyName
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditCompany(EditCompanyViewmodel model)
        {
            var company = new Company(model.CompanyID, model.CompanyName);
            _collection.Update(company);
            return RedirectToAction("ListCompanies");
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInCompany(List<UserRoleViewModel> model, string companyId)
        {
            var company = _collection.GetCompany(companyId);
            if (company == null)
            {
                //Todo: implement error view
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                bool result = false;


                if (model[i].IsSelected && !_collection.CheckIfEmployeeIsInCompany(company, user.Id))
                {
                    result = _collection.AddEmployee(user.Id, company);
                }
                else if (!model[i].IsSelected && _collection.CheckIfEmployeeIsInCompany(company, user.Id))
                {
                    result = _collection.RemoveEmployee(user.Id);
                }
                else
                {
                    continue;
                }

                if (result)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("CompanyDetails", new {companyId });
                    }

                }
            }

            return RedirectToAction("CompanyDetails", new {companyId });
        }
    }
}
