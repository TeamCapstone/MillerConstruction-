using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CapStoneProject.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set;}

        //public User from { get; set; }

        public bool Approved { get; set; }

        private List<Comment> comments = new List<Comment>();

        public List<Comment> comment  { get {return comments;} }

    }
}
