using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;


namespace CapStoneProject.Repositories.Interfaces
{
    public interface IBid
    {
        IQueryable<Bid> GetAllBids();

        Bid GetBidByUserID(int id);

        Bid GetBidByClientName(string lastName);

        Bid DeleteBR(int id);
    }
}
