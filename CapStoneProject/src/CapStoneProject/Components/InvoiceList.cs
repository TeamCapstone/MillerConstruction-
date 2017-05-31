using CapStoneProject.Models;
using CapStoneProject.Models.ViewModels;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Components
{
    public class InvoiceList : ViewComponent
    {
        private IInvoiceRepo repository;

        public InvoiceList(IInvoiceRepo repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke(Client c)
        {
            return View(new VMInvoice { Invoices = repository.GetAllInvoicesByClient(c) });
        }

    }
}
