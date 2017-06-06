using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Infrastructure
{
    public class UserInvoice : ViewComponent
    {
            private IInvoiceRepo repository;

            public UserInvoice(IInvoiceRepo repo)
            {
                repository = repo;
            }
            public IViewComponentResult Invoke(int id)
            {
                List<Invoice> invoices = new List<Invoice>();
                invoices = repository.GetAllInvoicesByClientId(id);
                return View(invoices);
            }

    }
}



