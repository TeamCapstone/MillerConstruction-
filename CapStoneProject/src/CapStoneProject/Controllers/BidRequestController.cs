using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;
using CapStoneProject.Models;
using CapStoneProject.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class BidRequestController : Controller
    {
        private IBidRequestRepo bidReqRepo;
        private IBidRepo bidRepo;
        protected UserManager<UserIdentity> UserManager;



        public BidRequestController(UserManager<UserIdentity> userMgr, IBidRequestRepo repo, IBidRepo bRepo)
        {
            bidReqRepo = repo;
            bidRepo = bRepo;
            UserManager = userMgr;

        }

        [HttpGet]
        public IActionResult BidRequest()
        {
            return View(new BidRequest());
        }

        [HttpPost]
        public async Task<IActionResult> BidRequest(BidRequest bidreq)
        {
            //TODO: check to see if email is alread in the database
            if (ModelState.IsValid)
            {

                UserIdentity user = new UserIdentity
                {
                    UserName = bidreq.User.Email,
                    Email = bidreq.User.Email,
                    FirstName = bidreq.User.FirstName,
                    LastName = bidreq.User.LastName
                };
                IdentityResult result = await UserManager.CreateAsync(user, bidreq.User.Password);
                

                if (result.Succeeded)
                {
                    bidReqRepo.Update(bidreq);
                    return RedirectToAction("Success");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(bidreq);
        }


        //[HttpGet]
        //public ViewResult Bid(int brID) => View(bidReqRepo.GetAllBidRequests()
        //    .FirstOrDefault(r => r.BidRequestID == brID));

        [HttpGet]
        public ViewResult Bid(int brID) => View(bidRepo.GetAllBids()
            .FirstOrDefault(r => r.BidReq.BidRequestID == brID));

        [HttpPost]
        public ViewResult Bid(Bid bid)
        {
            bidRepo.Update(bid);
            return View("AllBidRequests", bidReqRepo.GetAllBidRequests().ToList());
        }

      

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoggedInBidRequest()
        {
            return View(new BidRequest());
        }

        [HttpPost]
        public IActionResult LoggedInBidRequest(BidRequest bidreq)
        {
            //get the user
            var userID = UserManager.GetUserId(HttpContext.User);

            if (ModelState.IsValid)
            {
                bidReqRepo.Update(bidreq);
                return RedirectToAction("Success");
            }
            return View(bidreq);
        }

        public IActionResult AllBidRequests()
        {
            return View(bidReqRepo.GetAllBidRequests().ToList());
        }

        
    }
}
