using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public int ClientID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
