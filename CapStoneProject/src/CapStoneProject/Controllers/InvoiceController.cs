using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class InvoiceController : Controller
    {
        private IHostingEnvironment environment;
        private IInvoiceRepo invoiceRepo;
        private IClientRepo clientRepo;

        public InvoiceController(IHostingEnvironment env, IInvoiceRepo invRepo, IClientRepo clRepo)
        {
            environment = env;
            invoiceRepo = invRepo;
            clientRepo = clRepo;
        }

        [HttpGet]
        public IActionResult Index(int clientId)
        {
            Client client = new Client();
            client = clientRepo.GetClientById(clientId);
            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ICollection<IFormFile> files, Client client)
        {
            Invoice invoice = new Invoice();
            Client c = clientRepo.GetClientById(client.ClientID);
            
            var uploads = Path.Combine(environment.WebRootPath, "invoices");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    invoice.InvoiceFilename = file.FileName;
                    invoice.Client = c;
                    invoiceRepo.Create(invoice);

                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return RedirectToAction("AllClients", "Client");
        }

        public FileResult Download(string fname)
        {
            var filename = fname;
            var filepath = "wwwroot/invoices/" + filename;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", filename);
        }
    }
}
