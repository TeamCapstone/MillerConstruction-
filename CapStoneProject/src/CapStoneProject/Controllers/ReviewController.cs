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
    public class ReviewController : Controller
    {
        private IReviewRepo reviewRepo;
        protected UserManager<UserIdentity> UserManager;

        public ReviewController(UserManager<UserIdentity> userMgr,
            IReviewRepo repo)
        {
            reviewRepo = repo;
            UserManager = userMgr;
        }
        //This controller will be for the admin
        
        public ViewResult AllReviews()
        { 
            return View(reviewRepo.GetAllReviews().ToList());
        }

        //This controller will be for public
        public ViewResult AllApprovedReviews()
        {  
            return View(reviewRepo.GetAllApproved().ToList());
        }

        //only admin
        public ViewResult AllDisapprovedReviews()
        {
            return View(reviewRepo.GetAllDisapproved().ToList());
        }

        //admin and maybe public
        public ViewResult ReviewBySubject(string subject)
        {
            return View(reviewRepo.GetAllApproved().Where(m => m.Subject == subject).ToList());
        }

        //only admin
        public ViewResult ReviewByUser(string email)
        {
            return View(reviewRepo.GetAllReviews().Where(m => m.From.Email == email).ToList());
        }

        //User and admin
        [HttpGet]
        public ViewResult MakeReview()
        {
            return View();
        }

        //User and admin
        [HttpPost]
        public async Task<IActionResult> MakeReview(string subject, string body)
        {
            if (ModelState.IsValid)
            {
                var review = new Review();
                UserIdentity user = await UserManager.FindByNameAsync(User.Identity.Name);
                review.Subject = subject;
                review.Body = body;
                //review.Approved = false;
                review.Date = DateTime.Now;
                if (user != null)
                    review.From = user;
                reviewRepo.Update(review);
                return RedirectToAction("Reviews", "Review");
            }
            else
            {
                return View();
            }
        }

        //Admin only
        [HttpGet]
        public ViewResult Comment(string subject, int id)
        {
            var Crev = new VMComment();
            Crev.Subject = subject;
            Crev.ReviewID = id;
            Crev.Cmmt = new Comment();
            return View(Crev);
        }

        //Admin only
        [HttpPost]
        public IActionResult Comment(VMComment vmc)
        {
            Review review = (from m in reviewRepo.GetAllReviews()
                             where m.ReviewID == vmc.ReviewID
                             select m).FirstOrDefault<Review>();

            review.Comments.Add(vmc.Cmmt);
            reviewRepo.Update(review);
            return RedirectToAction("Reviews", "Review");
        }
    }
}
