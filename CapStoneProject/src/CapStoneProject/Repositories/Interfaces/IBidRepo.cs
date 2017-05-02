﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;


namespace CapStoneProject.Repositories.Interfaces
{
    public interface IBidRepo
    {
        IQueryable<Bid> GetAllBids();

        Bid GetBidByUserID(int id);

        Bid GetBidByClientName(string lastName);

        int Update(Bid req);

        Bid DeleteBR(int id);
    }
}