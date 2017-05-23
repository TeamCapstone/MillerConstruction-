using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CapStoneProject.Models
{
    public class BidRequest
    {
        public int BidRequestID { get; set; }

        public UserIdentity User { get; set; }//need this because we will have 2 forms one for members and other that will become members 

        public string ProjectDescription { get; set; }

        public string ProjectLocation { get; set; }

        public bool NewBuild { get; set; }

        public bool Remodel { get; set; }

        public bool Concrete { get; set; }

        public bool FrameWork { get; set; }
    }
}
