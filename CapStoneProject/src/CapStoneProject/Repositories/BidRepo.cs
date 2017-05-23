using System.Collections.Generic;
using System.Linq;
using CapStoneProject.Models;
using Microsoft.EntityFrameworkCore;
using CapStoneProject.Repositories.Interfaces;
using System;

namespace CapStoneProject.Repositories
{
    public class BidRepo : IBidRepo
    {
        private ApplicationDbContext context;

        public BidRepo(ApplicationDbContext ctx)
        {
            context = ctx;
        }
      
        public IQueryable<Bid> GetAllBids()
        {
            return context.Bids.Include(r => r.BidReq);
        }

        public Bid GetBidByUserID(int id)
        {
            return context.Bids.First(r => r.BidID == id);
        }

        //public BidRequest GetBidReqByID(int id)
        //{
        //    return context.Bids.FirstOrDefault(r => r.BidReq.BidRequestID == id);
        //}
       

        public Bid GetBidByClientName(string name)
        {
            return context.Bids.First(n => n.User.LastName == name);
        }

        public int Update(Bid bid)
        {
            if (bid.BidID == 0)
                context.Bids.Add(bid);
            else
                context.Bids.Update(bid);

            return context.SaveChanges();
        }

        public Bid DeleteBR(int id)
        {
            Bid dbBid = context.Bids.FirstOrDefault(br => br.BidID == id);
            if (dbBid != null)
            {
                context.Bids.Remove(dbBid);
                context.SaveChanges();
            }
            return dbBid;
        }
    }
}
