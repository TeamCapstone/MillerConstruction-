using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;


namespace CapStoneProject.Repositories.Interfaces
{
    public interface IBidRepo
    {
        IQueryable<Bid> GetAllBids();

        Bid GetBidByID(int id);

        Bid GetBidByUserEmail(string email);

        int Update(Bid req);

        Bid DeleteBid(int id);

    }
}
