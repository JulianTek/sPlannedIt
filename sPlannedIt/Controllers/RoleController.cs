using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sPlannedIt.Migrations;
using sPlannedIt.Viewmodels;
using sPlannedIt.Viewmodels.Role_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Role");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


           return View(model);
        }

        public async Task<IActionResult> EditRole(string roleId)
        {
           var role = await _roleManager.FindByIdAsync(roleId);
           if (role == null)
           {
               //Todo: add error view if role is null
           }

           var model = new EditRoleViewModel()
           {
               RoleId = roleId,
               RoleName = role.Name,
           };
           foreach (var user in await _userManager.Users.ToListAsync())
           {
               if (await _userManager.IsInRoleAsync(user, role.Name))
               {
                   model.Users.Add(user.Email);
               }
           }

           return View(model);
        }
    }
}
