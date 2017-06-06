using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;

namespace CapStoneProject.Models.ViewModels
{
    public class VMInvoice
    {
        public List<Invoice> Invoices;

        public DateTime Date { get; set; }
    }
}
