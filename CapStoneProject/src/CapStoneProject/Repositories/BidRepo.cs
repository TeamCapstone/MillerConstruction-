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
            return context.Bids.Include(r => r.BidReq).Include(u => u.User);
        }

        public Bid GetBidByID(int id)
        {
            return context.Bids.Include(u => u.User).Include(br => br.BidReq).First(b => b.BidID == id);
        }


        public Bid GetBidByUserEmail(string email)
        {
            return context.Bids.First(n => n.User.Email == email);
        }

        public int Update(Bid bid)
        {
            if (bid.BidID == 0 || bid.Version > 0)
                context.Bids.Add(bid);
            else
                context.Bids.Update(bid);

            return context.SaveChanges();
        }


        public Bid DeleteBid(int id)
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
