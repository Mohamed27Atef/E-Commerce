using E_Commerce.ViewModel.RoleViewModle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce.Controllers
{

    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> RoleManger;

        public RoleController(RoleManager<IdentityRole> _roleManger)
        {
            RoleManger = _roleManger;
        }


        //add new role
        public IActionResult addRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> addRole(RoleViewModle roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole { Name = roleVM.Role };
                var result = await RoleManger.CreateAsync(role);
                if (result.Succeeded)
                {
                    return View();
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(roleVM);
        }

    }
}