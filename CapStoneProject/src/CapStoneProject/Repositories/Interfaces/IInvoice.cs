using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IInvoice
    {
        IQueryable<Invoice> GetAllInvoices();
        
        Invoice GetInvoiceByID(int id);
        Invoice GetInvoiceByClientName(string lastName);
        Invoice GetInvoiceByProjectName(string name);
    }
}
