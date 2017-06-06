using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CapStoneProject.Models
{
    public class Bid
    {
        public int BidID { get; set; }
       
        public UserIdentity User { get; set; }

        public BidRequest BidReq { get; set; }

        public string RevisedProjectDescription { get; set; }

        public string MaterialsDescription { get; set; } //Possible future add on, supply table, with edit, add, delete

        public decimal SupplyCost { get; set; }

        public decimal LaborCost { get; set; }

        public decimal TotalEstimate { get; set; }// So this is intial amount a customer will back

        public string ProjectedTimeFrame { get; set; }

        public DateTime ProposedStartDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
