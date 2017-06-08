using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Models.ViewModels
{
    public class VMUserPage
    {
        public UserIdentity User { get; set; }
        public Client Client { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Bid> Bids { get; set; }
        public List<Project> Projects { get; set; }
    }
}
