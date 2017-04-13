using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;

namespace CapStoneProject.Repositories
{
    public class InvoiceRepo : IInvoice
    {

        private ApplicationDbContext context;

        public InvoiceRepo(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Invoice> GetAllInvoices()
        {
            return context.Invoices;
        }

        public Invoice GetInvoiceByClientName(string lastName)
        {
            return context.Invoices.First(i => i.Client.LastName == lastName);
        }

        public Invoice GetInvoiceByID(int id)
        {
            return context.Invoices.First(i => i.InvoiceID == id);
        }

        public Invoice GetInvoiceByProjectName(string name)
        {
            return context.Invoices.First(i => i.Project.ProjectName == name);
        }

    }
}
