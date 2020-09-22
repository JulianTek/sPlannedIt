using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Models;
using sPlannedIt.Viewmodels;
using sPlannedIt.Viewmodels.Account_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                Roles = Logic.RoleData_Logic.GetRoleNames()
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
                        for (int i = 0; i < model.Roles.Roles.Count; i++)
                        {
                            if (model.User.RoleName == model.Roles.Roles[i])
                            {
                               await _userManager.AddToRoleAsync(user, model.Roles.Roles[i]);
                               await _signInManager.SignInAsync(user, isPersistent: false);
                               return RedirectToAction(string.Concat("index" + model.Roles.Roles[i]), "home");
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

        [HttpPost]
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
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Invalid credentials");
            }
            return View(model);
        }
    }
}
