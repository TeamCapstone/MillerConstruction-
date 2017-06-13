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
        private IInvoiceRepo invoiceRepo;
        private IBidRepo bidRepo;
        private IProjectRepo projectRepo;

        public UserController(IProjectRepo prRepo, IBidRepo bRepo, IInvoiceRepo invRepo, IUserRepo usrRepo, RoleManager<IdentityRole> roleMgr, UserManager<UserIdentity> usrMgr, SignInManager<UserIdentity> sim, IClientRepo clRepo)
        {
            userManager = usrMgr;
            signInManager = sim;
            roleManager = roleMgr;
            clientRepo = clRepo;
            userRepo = usrRepo;
            invoiceRepo = invRepo;
            bidRepo = bRepo;
            projectRepo = prRepo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> InvoiceList(int id) // Displays all invoices for user on UserPage
        {
            UserIdentity user = new UserIdentity();
            string name = HttpContext.User.Identity.Name;
            user = await userManager.FindByNameAsync(name);
            Client client = new Client();
            client = clientRepo.GetClientByEmail(user.Email);
            List<Invoice> invoices = new List<Invoice>();
            invoices = invoiceRepo.GetAllInvoicesByClient(client);
            if (invoices == null)
            {
                TempData["ErrorMessage"] = "No Invoices";
                return RedirectToAction("UserPage", "User");
            }
            else
            {               
                return View(invoices);
            }            
        }

        

        public async Task<IActionResult> UserPage() // get all the data for the user
        {
            UserIdentity user = new UserIdentity();
            string name = HttpContext.User.Identity.Name;
            user = await userManager.FindByNameAsync(name);
            VMUserPage vm = new VMUserPage
            {
                User = user,
            };
            vm.Client = clientRepo.GetClientByEmail(user.Email);
            vm.Bids = bidRepo.GetBidsByUserEmail(user.Email);
            if (vm.Client != null)
            {
                vm.Projects = projectRepo.GetAllProjectsByClientId(vm.Client.ClientID);
                vm.Invoices = invoiceRepo.GetAllInvoicesByClient(vm.Client);
            }
            return View(vm);
        }

        public IActionResult CreateClient()
        {

            return View(new VMRegister());
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(VMRegister vm) // Gets users identity then creates a client
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
        public async Task<IActionResult> EditClient(Client client) // Edits Client Information and Updates User email/username as well
        {
            UserIdentity user = new UserIdentity();
            string name = HttpContext.User.Identity.Name;
            user = await userManager.FindByNameAsync(name);
            Client c = clientRepo.GetClientByEmail(client.Email);
            if (c == null)
            {
                clientRepo.Create(client);
            }
            else
            {
                clientRepo.Update(client);
            }
            
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

        [HttpPost]
        public IActionResult FilterUsers(string searchString) // for the Admin to search users
        {
            var users = userRepo.GetAllUsersFilter();
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(m => m.FirstName.Contains(searchString) || m.LastName.Contains(searchString) || m.Email.Contains(searchString));
            }
            return View(users.ToList());
        }
    }

}
