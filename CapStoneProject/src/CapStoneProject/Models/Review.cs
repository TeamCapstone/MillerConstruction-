﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CapStoneProject.Models
{
    public class Review
    {
        //Users will be able to leave a review that
        //the admin will approve then it will post 
        //and may leave a comment 
        public int ReviewID { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set;}

        public UserIdentity From { get; set; }

        public bool Approved { get; set; }

        private List<Comment> comments = new List<Comment>();

        public List<Comment> Comments { get {return comments;} }

    }
}
