﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Models
{
    public class Client
    {
        public int ClientID { get; set; }

        public User User { get; set; }

        public Bid Bid { get; set; }

        public string CompanyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Zipcode { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

    }
}
