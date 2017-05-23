using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Models.ViewModels
{
    public class VMBid
    {
        public int BidRequestID { get; set; }
        public int BidID { get; set; }
        public string CustomerFirst { get; set; }
        public string CustomerLast { get; set; }
        public string ProjectDescription { get; set; }
        public string RevisedProjectDescription { get; set; }
        public string MaterialsDescription { get; set; }
        public decimal SupplyCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal TotalEstimate { get; set; }// So this is intial amount a customer will get back
        public string ProjectedTimeFrame { get; set; }
        public DateTime ProposedStartDate { get; set; }
    }
}
