using CapStoneProject.Models;
using CapStoneProject.Models.ViewModels;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Components
{
    public class UserInvoice : ViewComponent
    {
        private IInvoiceRepo repository;
        private IClientRepo clientRepo;

        public UserInvoice(IInvoiceRepo repo, IClientRepo clRepo)
        {
            repository = repo;
            clientRepo = clRepo;
        }
        public IViewComponentResult Invoke(string email)
        {
            Client client = new Models.Client();
            client = clientRepo.GetClientByEmail(email);
            List<Invoice> invoices = new List<Invoice>();
            if (client == null)
                return View(invoices);
            else { 
            invoices = repository.GetAllInvoicesByClientId(client.ClientID);
            return View(invoices);
            }
        }

    }
}
