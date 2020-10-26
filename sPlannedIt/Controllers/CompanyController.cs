using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Logic.Models;
using sPlannedIt.Viewmodels.Company_Viewmodels;
using sPlannedIt.Viewmodels.Role_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class CompanyController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CompanyController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
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

                if (Logic.CompanyManager_Logic.CheckIfEmployeeIsInCompany(userRoleViewModel.UserId, companyId))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }

            return View(model);
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
                        return RedirectToAction("CompanyDetails", new { companyId = companyId });
                    }

                }
            }

            return RedirectToAction("CompanyDetails", new { companyId = companyId });
        }
    }
}
