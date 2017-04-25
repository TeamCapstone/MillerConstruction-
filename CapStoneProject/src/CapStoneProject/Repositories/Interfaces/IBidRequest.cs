using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IBidRequest
    {
        IQueryable<BidRequest> GetAllBidRequests();

        BidRequest GetBidRequestByUserID(int id);
        
        BidRequest GetBidRequestByClientName(string lastName);

        BidRequest DeleteBR(int id);
    }
}
