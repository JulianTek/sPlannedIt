using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public CompanyController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private readonly CompanyContainer _container = new CompanyContainer();
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
                if (!Logic.CompanyManager_Logic.CheckIfNameExists())
                {
                    Logic.CompanyManager_Logic.AddCompanyDto(company.CompanyID, company.CompanyName);
                    return RedirectToAction("RegisterEmployer", "Account", new { id = company.CompanyID });
                }
                ModelState.AddModelError("", "Company name already exists");
            }

            return View();
        }

        public IActionResult ListCompanies()
        {
            var model = _container.AllCompanies;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCompany(string id)
        {
            foreach (var userid in Logic.CompanyManager_Logic.GetEmployeesFromCompany(id))
            {
                var user = await _userManager.FindByIdAsync(userid);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            Logic.CompanyManager_Logic.DeleteCompany(id);
            return RedirectToAction("ListCompanies");
        }

        public async Task<IActionResult> CompanyDetails(string companyId)
        {
            List<CompanyDetailEmployeeData> data = new List<CompanyDetailEmployeeData>();
            foreach (string id in Logic.CompanyManager_Logic.GetEmployeesFromCompany(companyId))
            {
                data.Add(new CompanyDetailEmployeeData()
                {
                    Role = await _roleManager.FindByIdAsync(Logic.CompanyManager_Logic.GetRole(id)),
                    User = await _userManager.FindByIdAsync(id)
                });
            }
            CompanyDetailsViewmodel model = new CompanyDetailsViewmodel()
            {
                Company = _container.FindCompany(companyId),
                EmployeeData = data
            };
            model.Company.Employees = Logic.CompanyManager_Logic.GetEmployeesFromCompany(companyId);
            return View(model);
        }

        [HttpGet]
        public IActionResult EditUsersInCompany(string companyId)
        {
            ViewBag.companyId = companyId;

            var company = _container.FindCompany(companyId);
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

                userRoleViewModel.IsSelected = Logic.CompanyManager_Logic.CheckIfEmployeeIsInCompany(userRoleViewModel.UserId, companyId);
                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditCompany(string companyId)
        {
            var company = _container.FindCompany(companyId);
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
            Logic.CompanyManager_Logic.EditCompany(model.CompanyID, model.CompanyName);
            return RedirectToAction("ListCompanies");
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInCompany(List<UserRoleViewModel> model, string companyId)
        {
            var company = _container.FindCompany(companyId);
            if (company == null)
            {
                //Todo: implement error view
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                bool result = false;


                if (model[i].IsSelected && !Logic.CompanyManager_Logic.CheckIfEmployeeIsInCompany(user.Id, companyId))
                {
                    result = Logic.CompanyManager_Logic.AddEmployeeToCompany(user.Id, companyId);
                }
                else if (!model[i].IsSelected && Logic.CompanyManager_Logic.CheckIfEmployeeIsInCompany(user.Id, companyId))
                {
                    result = Logic.CompanyManager_Logic.RemoveEmployeeFromCompany(user.Id, companyId);
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
