using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Models
{
    public class User
    {
        public int UserID { get; set; }

        public Project Project { get; set; }

        public Client Client { get; set; }

    }
}
