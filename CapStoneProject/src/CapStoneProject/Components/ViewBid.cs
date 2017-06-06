using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Components
{
    public class ViewBid : ViewComponent
    {
        private IBidRepo repository;
        private UserManager<UserIdentity> userManager;

        public ViewBid(IBidRepo repo, UserManager<UserIdentity> usrMgr)
        {
            repository = repo;
            userManager = usrMgr;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserIdentity user = new UserIdentity();
            string name = HttpContext.User.Identity.Name;
            user = await userManager.FindByNameAsync(name);

            Bid bid = new Models.Bid();
            bid = repository.GetBidByUserEmail(user.Email);
            return View(bid);
        }
    }
}
