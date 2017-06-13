using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CapStoneProject.Models.ViewModels
{
    public class VMEditProject
    {
        public int ProjectID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        public string ProjectName { get; set; }

        public decimal OriginalEstimate { get; set; }

        public decimal AdditionalCost { get; set; }

        public decimal CurrentTotal { get; set; }

        [Required]
        public string Status { get; set; }
        public DateTime StatusDate { get; set; }
    }
}
