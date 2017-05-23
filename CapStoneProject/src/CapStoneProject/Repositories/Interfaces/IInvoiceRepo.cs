using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IInvoiceRepo
    {
        IQueryable<Invoice> GetAllInvoices();
        
        Invoice GetInvoiceByID(int id);
        Invoice GetInvoiceByClientName(string lastName);
        Invoice GetInvoiceByProjectName(string name);
        List<Invoice> GetAllInvoicesByClient(Client client);
        IEnumerable<Invoice> Invoices { get; }
        int Create(Invoice invoice);
        int Update(Invoice invoice);
        int Delete(int invoiceId);
    }
}
