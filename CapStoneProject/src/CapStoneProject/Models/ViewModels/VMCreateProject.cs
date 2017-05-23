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
         client ID, client name, email, etc*/
   
        //temporarily using client last name and email to search for client in db

        //To be used by EditProject
        public int ProjectID { get; set; } 

        //to link
        public int BidID { get; set; }

        public string LastName { get; set; }

        //to link client to project
        public int ClientID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public decimal Estimate { get; set; } //To be editable by EditProject

        public string Status { get; set; } //To be editable by EditProject

        public DateTime StatusDate { get; set; } //To be editable by EditProject
    }
}
