using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Models.ViewModels
{
    public class VMLoggedBR
    {
        public int BidRequestID { get; set; }
        public int UserID { get; set; }
        
        public string CustomerFirst { get; set; }
        public string CustomerLast { get; set; }
        public string ProjectDescription { get; set; }        
        public string ProjectLocation { get; set; }
        public bool NewBuild { get; set; }
        public bool Remodel { get; set; }
        public bool Concrete { get; set; }
        public bool FrameWork { get; set; }
        public bool Responded { get; set; }
    }
}
