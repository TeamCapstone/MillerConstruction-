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
    [ViewComponent(Name = "BidRequestsVC")]
    public class AdminBRViewComponent : ViewComponent
    {
        private ApplicationDbContext context;

        public AdminBRViewComponent(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await GetAllBidRequestsAsync());
        }

        private Task<List<BidRequest>> GetAllBidRequestsAsync()
        {
            BidRequestRepo bidReqList = new BidRequestRepo(context);
            return Task.FromResult(bidReqList.GetAllBidRequests().ToList());
        }

    }
}
