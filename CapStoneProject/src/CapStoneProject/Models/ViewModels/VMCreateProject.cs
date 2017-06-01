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
   
        //using client and email to search for client in db

        public int ProjectID { get; set; }
            
        //to link
        public int BidID { get; set; }

        //to link client to project
        public int ClientID { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        public string LastName { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public decimal Estimate { get; set; } //To be editable by EditProject

        public string Status { get; set; } //To be editable by EditProject

        public DateTime StatusDate { get; set; } //To be editable by EditProject
    }
}
