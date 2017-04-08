using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CapStoneProject.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        [Required]
        public int ClientID { get; set; }

        [Required]
        public int ProjectID { get; set; }

        [Required]
        [Range(typeof(Decimal), "5", "1,000,000,000")]
        public decimal TotalPrice { get; set; }


    }
}
