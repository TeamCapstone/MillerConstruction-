using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CapStoneProject.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }// form will require a view model

        
        public Client Client { get; set; }

        
        public Project Project { get; set; }

        [Required]
        [Range(typeof(Decimal), "1", "1,000,000,000")]
        public decimal TotalPrice { get; set; }


    }
}
