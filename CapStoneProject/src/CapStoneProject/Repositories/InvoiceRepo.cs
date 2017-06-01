using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapStoneProject.Repositories
{
    public class InvoiceRepo : IInvoiceRepo
    {

        private ApplicationDbContext context;

        public InvoiceRepo(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Invoice> Invoices
        {
            get
            {
                return context.Invoices.Include(c => c.Client).ToList();
            }
        }

        public IQueryable<Invoice> GetAllInvoices()
        {
            return context.Invoices;
        }

        //TODO: change to get multiple invoices
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

        public int Create(Invoice invoice)
        {
            context.Invoices.Add(invoice);
            return context.SaveChanges();
        }

        public int Update(Invoice invoice)
        {
            context.Invoices.Update(invoice);
            return context.SaveChanges();
        }

        public int Delete(int invoiceId)
        {

            Invoice invoice = context.Invoices.FirstOrDefault(i => i.InvoiceID == invoiceId);
            if (invoice != null)
            {
                context.Invoices.Remove(invoice);
                return context.SaveChanges();
            }
            return invoiceId;
        }

        public List<Invoice> GetAllInvoicesByClient(Client client)
        {
            return (from t in context.Invoices
                    where t.Client == client
                    select t).ToList();
        }

        public List<Invoice> GetAllInvoicesByClientId(int i)
        {
            return (from t in context.Invoices
                    where t.Client.ClientID == i
                    select t).ToList();
        }

    }
}
