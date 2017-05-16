using System.Collections.Generic;
using System.Linq;
using CapStoneProject.Models;
using Microsoft.EntityFrameworkCore;
using CapStoneProject.Repositories.Interfaces;
using System;

namespace CapStoneProject.Repositories
{
    public class BidRequestRepo : IBidRequestRepo
    {
        private ApplicationDbContext context;

        public BidRequestRepo(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<BidRequest> GetAllBidRequests()
        {
            return context.BidRequests.Include(r => r.User);
        }

        public BidRequest GetBidRequestByUserID(int id)
        {
            return context.BidRequests.First(r => r.BidRequestID == id);
        }

        public BidRequest GetBidRequestByClientName(string name)
        {


            return context.BidRequests.First(n => n.User.LastName == name);
        }
        //TODO: write method to check if email is in database

        public int Update(BidRequest req)
        {
            if (req.BidRequestID == 0)
                context.BidRequests.Add(req);
            else
                context.BidRequests.Update(req);

            return context.SaveChanges();
        }


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
