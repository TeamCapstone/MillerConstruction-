using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Models.ViewModels
{
    public class VMRegister
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is not Valid")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^.*(?=.{10,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Must be ten characters long upper and lower case, digit and special character")]
<<<<<<< HEAD
=======

>>>>>>> 66adf4fb08027c6121ba0a77f8f52af9b1f2890b
        public string Password { get; set; }
    }
}
