using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CapStoneProject.Models
{
    public class Project
    {
        public int ProjectID { get; set; }

        [Required]
        public Client Client { get; set; }

        [Required]
        public Bid Bid { get; set; } // Bid is a Quote and a Quote is a Bid

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal OriginalEstimate { get; set; }

        public decimal AdditionalCosts { get; set; }

        public decimal TotalCost { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public string ProjectStatus { get; set; }

        //Date when status update was made
        public DateTime StatusDate { get; set; }
    }
}
