using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Entities.Models;
using sPlannedIt.Interface.DAL;
using sPlannedIt.Logic;
using sPlannedIt.Models;
using sPlannedIt.Viewmodels.Company_Viewmodels;
using sPlannedIt.Viewmodels.Role_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class CompanyController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICompanyHandler _companyHandler;

        public CompanyController(UserManager<IdentityUser> userManager, ICompanyHandler companyHandler)
        {
            _userManager = userManager;
            _companyHandler = companyHandler;
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
                if (!_companyHandler.CheckIfCompanyNameExists(company.CompanyName))
                {
                    _companyHandler.Create(ModelConverter.ConvertModelToCompanyDto(company));
                    return RedirectToAction("RegisterEmployer", "Account", new { id = company.CompanyId });
                }
                ModelState.AddModelError("", "Company name already exists");
            }

            return View();
        }

        public IActionResult ListCompanies()
        {
            var model = _companyHandler.GetAll();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCompany(string id)
        {
            foreach (var userid in _companyHandler.GetAllEmployees(_companyHandler.GetById(id).CompanyId))
            {
                var user = await _userManager.FindByIdAsync(userid);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            _companyHandler.Delete(_companyHandler.GetById(id).CompanyId);
            return RedirectToAction("ListCompanies");
        }

        public async Task<IActionResult> CompanyDetails(string companyId)
        {
            Company company = ModelConverter.ConvertCompanyDtoToModel(_companyHandler.GetById(companyId));
            List<CompanyDetailEmployeeData> data = new List<CompanyDetailEmployeeData>();
            foreach (string id in _companyHandler.GetAllEmployees(ModelConverter.ConvertModelToCompanyDto(company).CompanyId))
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
            model.Company.SetEmployees(_companyHandler.GetAllEmployees(company.CompanyId));
            return View(model);
        }

        [HttpGet]
        public IActionResult EditUsersInCompany(string companyId)
        {
            ViewBag.companyId = companyId;

            var company = ModelConverter.ConvertCompanyDtoToModel(_companyHandler.GetById(companyId));
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

                userRoleViewModel.IsSelected = _companyHandler.CheckIfEmployeeInCompany(user.Id, company.CompanyId);
                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditCompany(string companyId)
        {
            var company = _companyHandler.GetById(companyId);
            EditCompanyViewmodel model = new EditCompanyViewmodel()
            {
                CompanyID = company.CompanyId,
                CompanyName = company.CompanyName
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditCompany(EditCompanyViewmodel model)
        {
            var company = new Company(model.CompanyID, model.CompanyName);
            _companyHandler.Update(ModelConverter.ConvertModelToCompanyDto(company));
            return RedirectToAction("ListCompanies");
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInCompany(List<UserRoleViewModel> model, string companyId)
        {
            var company = ModelConverter.ConvertCompanyDtoToModel(_companyHandler.GetById(companyId));
            if (company == null)
            {
                //Todo: implement error view
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                bool result = false;


                if (model[i].IsSelected && !_companyHandler.CheckIfEmployeeInCompany(user.Id, companyId))
                {
                    result = _companyHandler.AddEmployee(user.Id, ModelConverter.ConvertModelToCompanyDto(company));
                }
                else if (!model[i].IsSelected && _companyHandler.CheckIfEmployeeInCompany(user.Id, companyId))
                {
                    result = _companyHandler.RemoveEmployee(user.Id);
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
