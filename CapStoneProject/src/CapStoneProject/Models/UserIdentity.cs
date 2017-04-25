using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Models
{
    public class UserIdentity : IdentityUser
    {
<<<<<<< HEAD
=======
        public int UserIndentityID { get; set; }
>>>>>>> 66adf4fb08027c6121ba0a77f8f52af9b1f2890b

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //[Required]
        //[RegularExpression("^.*(?=.{10,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Must be ten characters long upper and lower case, digit and special character")]
        public string Password { get; set; }
    }
}
