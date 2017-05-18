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
using Microsoft.Extensions.DependencyInjection;





// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class ClientController : Controller
    {
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IClientRepo clientRepo;

        public ClientController(RoleManager<IdentityRole> roleMgr, UserManager<UserIdentity> usrMgr, SignInManager<UserIdentity> sim, IClientRepo clRepo)
        {
            userManager = usrMgr;
            signInManager = sim;
            roleManager = roleMgr;
            clientRepo = clRepo;
        }

        public IActionResult Create()
        {
         
            return View(new VMRegister());
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMRegister vm)
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
                return RedirectToAction("AllClients", "Client");
            }
            else
            {
                return View(vm);
            }
        }

        public async Task<IActionResult> ClientInfo()
        {
            UserIdentity user = new UserIdentity();
            string name = HttpContext.User.Identity.Name;
            user = await userManager.FindByNameAsync(name);
            Client client = new Models.Client();
            client = clientRepo.GetClientByEmail(user.Email);
            return View(client);
        }

        public async Task<IActionResult> UserEdit(int id)
        {
            UserIdentity user = new UserIdentity();
            string name = HttpContext.User.Identity.Name;
            user = await userManager.FindByNameAsync(name);
            Client client = new Client();
            client = clientRepo.GetClientByEmail(user.Email);
            if (client == null)
                return RedirectToAction("Create", "Client");
            else
            return View(client);
        }

        [HttpPost]
        public IActionResult UserEdit(Client client)
        {
            clientRepo.Update(client);
            return RedirectToAction("ClientInfo", "Client");
        }

        public IActionResult Edit(int id)
        {
            Client client = new Client();
            client = clientRepo.GetClientById(id);
            return View(client);
        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                clientRepo.Update(client);
                return RedirectToAction("AllClients", "Client");
            }
            else
            {
                return View(client);
            }
        }

        [HttpPost]
        public IActionResult Delete(Client client)
        {
            int id = client.ClientID;
            clientRepo.Delete(id);
            return RedirectToAction("AllClients", "Client");
        }

        public ViewResult AllClients()
        {
            return View(clientRepo.GetAllClients().ToList());
        }

        //TODO: Ad Search by email, first name, last name

        public ViewResult Searchby()
        {
            return View(new VMClientSearch());
        }

        [HttpPost]
        public IActionResult SearchBy(VMClientSearch cs)
        {
            Client c = new Client();
            if (cs.SearchCategory == "Email")
                c = clientRepo.GetClientByEmail(cs.SearchValue);
            else if (cs.SearchCategory == "FirstName")
                c = clientRepo.GetClientByFirstName(cs.SearchValue);
            else if (cs.SearchCategory == "LastName")
                c = clientRepo.GetClientByLastName(cs.SearchValue);

            if (c == null)
                return View();
            else
                return RedirectToAction("Edit", new { id = c.ClientID });
        }

    }
}

