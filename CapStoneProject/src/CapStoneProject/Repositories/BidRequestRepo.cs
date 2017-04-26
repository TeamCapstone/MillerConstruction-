using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;

namespace CapStoneProject.Repositories
{
    public class BidRequestRepo : IBidRequest
    {
        private ApplicationDbContext context;

        public BidRequestRepo(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<BidRequest> GetAllBidRequests()
        {
            return context.BidRequests;
        }

        public BidRequest GetBidRequestByUserID(int id)
        {
            return context.BidRequests.First(r => r.BidRequestID == id);
        }

        //public BidRequest GetBidRequestByClientName(string name)
        //{


        //    return context.BidRequests.First(n => n.User.Client.LastName == name);
      //}

        public BidRequest DeleteBR(int id)
        {
            BidRequest bReq = context.BidRequests.FirstOrDefault(br => br.BidRequestID == id);
            if (bReq != null)
            {
                context.BidRequests.Remove(bReq);
                context.SaveChanges();
            }
            return bReq;
        }
       
    }
}
