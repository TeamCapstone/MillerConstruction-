using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CapStoneProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CapStoneProject.Models.ViewModels;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class AuthenticationController : Controller
    {
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private UserRepo userRepo;

        public AuthenticationController(RoleManager<IdentityRole> roleMgr, UserManager<UserIdentity> usrMgr, SignInManager<UserIdentity> sim, IUserRepo repo)
        {
            userManager = usrMgr;
            signInManager = sim;
            //userRepo = repo;
            roleManager = roleMgr;
        }

        public IActionResult Register()
        {
            return View(new VMRegister());
        }

        [HttpPost]
        public async Task<IActionResult> Register(VMRegister vm)
        {
            if (ModelState.IsValid)
            {
                UserIdentity user = new UserIdentity
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Email = vm.Email,
                    
                };
                
                UserIdentity userI = new UserIdentity { UserName = vm.Email };
                IdentityResult result = await userManager.CreateAsync(userI, vm.Password);

                string role = "User";

                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    if (result.Succeeded)
                    {
                        userRepo.Create(user);
                        await userManager.AddToRoleAsync(userI, role);
                        return RedirectToAction("Login", "Authentication");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    if (result.Succeeded)
                    {
                        userRepo.Create(user);
                        await userManager.AddToRoleAsync(userI, role);
                        return RedirectToAction("Login", "Authentication");
                    }
                }


            }
            // We get here either if the model state is invalid or if xreate user fails
            return View(vm);
        }


        public ViewResult Login()
        {
            return View(new VMLogin());
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin vm)
        {
            if (ModelState.IsValid)
            {
                UserIdentity user = await userManager.FindByNameAsync(vm.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await signInManager.PasswordSignInAsync(
                                user, vm.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Task");
                    }
                }
                ModelState.AddModelError(nameof(VMLogin.UserName),
                    "Invalid user or password");
            }
            return View(vm);
        }
    }
}
