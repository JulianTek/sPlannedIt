using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Interface.DAL;
using sPlannedIt.Logic;
using sPlannedIt.Models;
using sPlannedIt.Viewmodels;
using sPlannedIt.Viewmodels.Account_Viewmodels;

namespace sPlannedIt.Controllers
{
    // The following methods just create accounts, i will have to rewrite these to support companies so you can actually use the app
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICompanyHandler _companyHandler;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICompanyHandler companyHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _companyHandler = companyHandler;
        }

        [HttpGet]
        public IActionResult RegisterEmployer(string id)
        {
            RegisterEmployerViewmodel model = new RegisterEmployerViewmodel()
            {
                User = new UserData
                {
                    Email = null,
                    Password = null,
                    ConfirmPwd = null,
                    RoleName = "Employer"
                },
                CompanyId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployer(RegisterEmployerViewmodel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.User.Email,
                    Email = model.User.Email
                };
                var result = await _userManager.CreateAsync(user, model.User.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employer");
                    var addToCompanyResult = _companyHandler.AddEmployee(user.Id, _companyHandler.GetById(model.CompanyId));
                    if (addToCompanyResult)
                    {
                        return RedirectToAction("ListCompanies", "Company");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel
            {
                User = null,
                Roles = new RolesData
                {
                    Roles = Logic.RoleData_Logic.GetRoleNames()
                },
            };
            return View(model);
        }


        //Todo: Add support for name so username can be overhauled to be remembered easier. Add support for roles
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            // VERY MUCH A TEMP SOLUTION TO A PROBLEM I DEFINITELY CREATED
            model.Roles = new RolesData
            {
                Roles = RoleData_Logic.GetRoleNames()
            };
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.User.Email,
                    Email = model.User.Email
                };

                var result = await _userManager.CreateAsync(user, model.User.Password);


                if (result.Succeeded)
                {
                    foreach (var role in model.Roles.Roles)
                    {
                        if (model.User.RoleName == role)
                        {
                            await _userManager.AddToRoleAsync(user, role);
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction(string.Concat($"index{role}"), role);
                        }
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var role = await _userManager.GetRolesAsync(user);
                if (role.Count > 1)
                {
                    // todo: add view and action if user has more roles
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // Since UserRole is a list, it will use the first index of the list
                    return RedirectToAction(string.Concat($"index{role[0]}"), role[0]);
                }

                ModelState.AddModelError(string.Empty, "Invalid credentials");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
