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

        //Figure we'll need this to be required but not setting it [Required] yet just in case
        public int ClientID { get; set; }

        [Required]
        public int QuoteID { get; set; }

        //Company name (if applicable)
        public string CompanyName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal StartFee { get; set; }

        public decimal OtherFees { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public string ProjectStatus { get; set; }

        //Date when status update was made
        public DateTime StatusDate { get; set; }
    }
}
