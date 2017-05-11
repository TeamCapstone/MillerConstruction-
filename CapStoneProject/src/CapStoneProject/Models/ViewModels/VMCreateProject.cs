using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Models.ViewModels
{
    public class VMCreateProject
    {
        /*When creating project, decide whether to instantiate client object by entering either
         client ID, client name, email, etc. Figure out also how to tie in bid object*/

        //temporarily using client last name and email to search for client in db
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public decimal Estimate { get; set; }
    }
}
