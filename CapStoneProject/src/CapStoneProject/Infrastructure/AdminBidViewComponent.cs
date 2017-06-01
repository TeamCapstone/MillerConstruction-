using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using CapStoneProject.Repositories;

namespace CapStoneProject.Infrastructure
{
    [ViewComponent(Name = "BidVC")]
    public class AdminBidViewComponent : ViewComponent
    {
        private ApplicationDbContext context;

        public AdminBidViewComponent(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await GetAllBidsAsync());
        }

        private Task<List<Bid>> GetAllBidsAsync()
        {
            BidRepo bidList = new BidRepo(context);
            return Task.FromResult(bidList.GetAllBids().ToList());
        }

    }
}
