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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;




// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class ClientController : Controller
    {
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IClientRepo clientRepo;
        private IHostingEnvironment environment;
        private IInvoiceRepo invoiceRepo;
        private IUserRepo userRepo;
        private string Email = "postmaster@millercustomconstructioninc.com";
        private string Password = "Capstone132.";
        private const string Server = "m05.internetmailserver.net";
        private const int Port = 465;

        public ClientController(IUserRepo uerRepo, IHostingEnvironment env, IInvoiceRepo invRepo, RoleManager<IdentityRole> roleMgr, UserManager<UserIdentity> usrMgr, SignInManager<UserIdentity> sim, IClientRepo clRepo)
        {
            userManager = usrMgr;
            signInManager = sim;
            roleManager = roleMgr;
            clientRepo = clRepo;
            environment = env;
            invoiceRepo = invRepo;
            userRepo = uerRepo;
        }

        public IActionResult AdminCreateClient(string email) // For the admin to create a client
        {
            UserIdentity user = new UserIdentity();
            user = userRepo.GetUser(email);

            VMAdminCreateClient client = new VMAdminCreateClient
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                User = user,
                Email = user.Email
            };

            return View(client);
        }

        [HttpPost]
        public IActionResult AdminCreateClient(VMAdminCreateClient vm)
        {
            if (ModelState.IsValid)
            {

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
                    UserIdentity = vm.User,
                    Email = vm.Email,
                    
                };

                clientRepo.Create(client);
                return RedirectToAction("AdminPage", "Admin");
            }
            else
            {
                return View(vm);
            }
        }

        public IActionResult Create()
        {
         
            return View(new VMRegister());
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMRegister vm) // for the user to create a client
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

        public async Task<IActionResult> ClientInfo() // returns current personal information of user for user
        {
            UserIdentity user = new UserIdentity();
            string name = HttpContext.User.Identity.Name;
            user = await userManager.FindByNameAsync(name);
            Client client = new Models.Client();
            client = clientRepo.GetClientByEmail(user.Email);
            return View(client);
        }

        public async Task<IActionResult> UserEdit(int id) // not in use, was used before panels were added
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

        public IActionResult Edit(int id)  // The Admin uses this to edit client information
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
            return RedirectToAction("AdminPage", "Admin");
        }

        public ViewResult AllClients() // gets all the clients for filtering
        {            
            return View(clientRepo.GetAllClients().ToList());
        }

        //OLD code

        //public ViewResult Searchby()
        //{
        //    return View(new VMClientSearch());
        //}

        //[HttpPost]
        //public IActionResult SearchBy(VMClientSearch cs)
        //{
        //    Client c = new Client();
        //    if (cs.SearchCategory == "Email")
        //        c = clientRepo.GetClientByEmail(cs.SearchValue);
        //    else if (cs.SearchCategory == "FirstName")
        //        c = clientRepo.GetClientByFirstName(cs.SearchValue);
        //    else if (cs.SearchCategory == "LastName")
        //        c = clientRepo.GetClientByLastName(cs.SearchValue);

        //    if (c == null)
        //        return View();
        //    else
        //        return RedirectToAction("Edit", new { id = c.ClientID });
        //}

            //TODO: rename and move to admin controller
        [HttpPost]
        public async Task<IActionResult> AllClients(string searchString) //Filters the Clients
        {
            var clients = clientRepo.GetAllClients();
            if (!string.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(m => m.FirstName.Contains(searchString) || m.LastName.Contains(searchString) || m.Email.Contains(searchString) || m.CompanyName.Contains(searchString));
            }
            return View(await clients.ToListAsync());
        }

        public ActionResult ModalAction(int Id) // Modal for Invoice Upload
        {
            Client client = new Client();
            client = clientRepo.GetClientById(Id);
            ViewBag.Id = Id;
            return PartialView("ModalContent");
        }

        [HttpGet]
        public IActionResult InvoiceUpload(int clientId) // not in use, was used before modal was added
        {
            Client client = new Client();
            client = clientRepo.GetClientById(clientId);
            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> InvoiceUpload(ICollection<IFormFile> files, int id) // Admin uploads invoice for client
        {
            Invoice invoice = new Invoice();
            Client c = new Client();
            c = clientRepo.GetClientById(id);
            var uploads = Path.Combine(environment.WebRootPath, "invoices");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    invoice.InvoiceFilename = file.FileName;
                    invoice.Client = c;
                    invoice.Date = DateTime.Today;
                    invoiceRepo.Create(invoice);

                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("MCCInc", Email));
            email.Subject = "Invoice Uploaded";
            email.Body = new TextPart("plain")
            {
                Text = "Your Invoice has been uploaded."
            };
            email.To.Add(new MailboxAddress(c.Email));

            using (var client_c = new SmtpClient())
            {

                client_c.Connect(Server, Port, SecureSocketOptions.SslOnConnect);

                client_c.AuthenticationMechanisms.Remove("XOAUTH2");

                client_c.Authenticate(Email, Password);

                client_c.Send(email);

                client_c.Disconnect(true);
            }
            return RedirectToAction("AdminPage", "Admin");
        }

        public FileResult Download(string fname) // For the user when they download their invoice
        {
            var filename = fname;
            var filepath = "wwwroot/invoices/" + filename;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", filename);
        }

    }
}

