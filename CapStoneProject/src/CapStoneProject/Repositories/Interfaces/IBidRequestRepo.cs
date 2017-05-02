using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IBidRequestRepo
    {
        IQueryable<BidRequest> GetAllBidRequests();

        BidRequest GetBidRequestByUserID(int id);
        
        BidRequest GetBidRequestByClientName(string lastName);

        int Update(BidRequest req);

        BidRequest DeleteBR(int id);
    }
}
