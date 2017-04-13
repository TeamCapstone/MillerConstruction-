using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;

namespace CapStoneProject.Repositories
{
    public class BidRepo : IBid
    {
        private ApplicationDbContext context;

        public BidRepo(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Bid> GetAllBids()
        {
            return context.Bids;
        }

        public Bid GetBidByUserID(int id)
        {
            return context.Bids.First(r => r.BidID == id);
        }

        public Bid GetBidByClientName(string name)
        {
            return context.Bids.First(n => n.User.Client.LastName == name);
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
