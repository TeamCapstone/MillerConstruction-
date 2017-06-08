using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using CapStoneProject.Repositories;

namespace CapStoneProject.Components
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

        //filtering
        //[HttpPost]
        //public async Task<IActionResult> BidFilter(string ss)
        //{
        //    //var words = brRepo.GetAllBidRequests();
        //    //BidRepo bidList = new BidRepo(context);
        //    var words = GetAllBidsAsync();

        //    if (!string.IsNullOrEmpty(ss))
        //    {
        //        words = words.Where(b => b.User.LastName.ToLower().Contains(ss) || b.User.FirstName.ToLower().Contains(ss) || b.User.Email.ToLower().Contains(ss));
        //    }
        //    return View("AdminPage", await words.ToListAsync());

        //}

    }
}
