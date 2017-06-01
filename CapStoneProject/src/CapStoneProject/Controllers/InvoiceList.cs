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
    public class InvoiceList : ViewComponent
    {
        private IInvoiceRepo invoiceRepo;

        public InvoiceList(IInvoiceRepo invRepo)
        {
            invoiceRepo = invRepo;
        }
        public IViewComponentResult Invoke(int i)
        {
            List<Invoice> invoices = new List<Invoice>();
            invoices = invoiceRepo.GetAllInvoicesByClientId(i);
            return View(invoices);
        }

    }
}
