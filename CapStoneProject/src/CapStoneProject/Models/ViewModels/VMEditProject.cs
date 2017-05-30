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

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public decimal Estimate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }
    }
}
