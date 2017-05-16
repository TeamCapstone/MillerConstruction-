using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CapStoneProject.Models;
using CapStoneProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class UserController : Controller
    {
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IClientRepo clientRepo;
        private IUserRepo userRepo;

        public UserController(IUserRepo usrRepo, RoleManager<IdentityRole> roleMgr, UserManager<UserIdentity> usrMgr, SignInManager<UserIdentity> sim, IClientRepo clRepo)
        {
            userManager = usrMgr;
            signInManager = sim;
            roleManager = roleMgr;
            clientRepo = clRepo;
            userRepo = usrRepo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult UserPage()
        {
            return View();
        }

        public IActionResult CreateClient()
        {

            return View(new VMRegister());
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(VMRegister vm)
        {
            if (ModelState.IsValid)
            {
                UserIdentity user = new UserIdentity();
                string name = HttpContext.User.Identity.Name;
                user = await userManager.FindByNameAsync(name);

                Client client = new Client
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    CompanyName = vm.CompanyName,
                    Street = vm.Street,
                    City = vm.City,
                    State = vm.State,
                    Zipcode = vm.Zipcode,
                    PhoneNumber = vm.PhoneNumber,
                    UserIdentity = user,
                    Email = user.Email
                };

                clientRepo.Create(client);
                return RedirectToAction("UserPage", "User");
            }
            else
            {
                return View(vm);
            }
        }

        public async Task<IActionResult> EditClient(int id)
        {
            UserIdentity user = new UserIdentity();
            string name = HttpContext.User.Identity.Name;
            user = await userManager.FindByNameAsync(name);
            Client client = new Client();
            client = clientRepo.GetClientByEmail(user.Email);
            if (client == null)
                return RedirectToAction("CreateClient", "User");
            else
                return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> EditClient(Client client)
        {
            UserIdentity user = new UserIdentity();
            string name = HttpContext.User.Identity.Name;
            user = await userManager.FindByNameAsync(name);
            clientRepo.Update(client);
            user.Email = client.Email;
            user.UserName = client.Email;
            await userManager.UpdateAsync(user);
            await signInManager.SignOutAsync();
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, user.Password, false, false);
                if (result.Succeeded)
                {
                    if (await userManager.IsInRoleAsync(user, "Admin"))
                        return RedirectToAction("AdminPage", "Admin");
                    else
                        return RedirectToAction("UserPage", "User");

                }
            
            return RedirectToAction("UserPage", "User");
        }
    }

}
