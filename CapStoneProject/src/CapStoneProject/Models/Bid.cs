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

        [Required]
        public int BidRequestID { get; set; }

        //I am not sure we need a userid here since its in the bidrequest model but just incase
        public int UserID { get; set; }

        public string Supplies { get; set; } //List?

        public decimal SupplyCost { get; set; }

        public decimal LaborCost { get; set; }

        public decimal TotalEstimate { get; set; }

        public string ProjectedTimeFrame { get; set; }

        public DateTime ProposedStartDate { get; set; }
    }
}
