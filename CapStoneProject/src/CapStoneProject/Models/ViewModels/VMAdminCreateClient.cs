using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Models.ViewModels
{
    public class VMAdminCreateClient
    {

            public int ClientID { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            public string CompanyName { get; set; }

            public string Street { get; set; }

            [RegularExpression(@"^(\(\d{3}\)\s*)?\d{3}[\s-]?\d{4}$", ErrorMessage = "Accepted format (999) 999-9999")]
            public string PhoneNumber { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            [RegularExpression(@"^\d{5}$", ErrorMessage = "Zipcode not valid")]
            public string Zipcode { get; set; }


            [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is not Valid")]
            public string Email { get; set; }

            //[Required]
            //[RegularExpression("^.*(?=.{10,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Must be ten characters long upper and lower case, digit and special character")]
            public string Password { get; set; }

            public UserIdentity User { get; set; }
        }
    }


