using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CapStoneProject.Models
{
    public class Comment
    {
        //Admin will leave a comment after they 
        //approve it 
        public int CommentID { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }
    }
}
