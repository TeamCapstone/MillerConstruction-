using System.Collections.Generic;
using System.Linq;
using CapStoneProject.Models;
using Microsoft.EntityFrameworkCore;
using CapStoneProject.Repositories.Interfaces;
using System;

namespace CapStoneProject.Repositories
{
    public class ReviewRepo : IReviewRepo
    {
        private ApplicationDbContext context;
        private UserIdentity user;
        public ReviewRepo(ApplicationDbContext ctx)//going to take a instants of context
        {
            context = ctx;
        }

        public IQueryable<Review> GetAllReviews()
        {
            return context.Reviews.Include(m => m.From).Include(m => m.Comments);
        }

        public IQueryable<Review> GetAllApproved()
        {
            return context.Reviews.Include(m => m.Approved == true);
        }

        public IQueryable<Review> GetAllDisapproved()
        {
            return context.Reviews.Include(m => m.Approved == false);
        }

        public List<Review> GetReviewBySubject(string subject)
        {
            return context.Reviews.Where(m => m.Subject == subject).Include(m => m.From).ToList();
        }

        public List<Review> GetReviewByUser(UserIdentity user)
        {
            return context.Reviews.Where(m => m.From.Id == user.Id).ToList();
        }


        public int Update(Review review)
        {
            if (review.Approved == true)
            {
                if (review.ReviewID == 0)
                    context.Reviews.Add(review);
                else
                    context.Reviews.Update(review);

                return context.SaveChanges();
            }
            else
            {
                return review.ReviewID;
            }
        }

        public Review DeleteReview(int reviewID)
        {
            
            Review dbEntry = context.Reviews
                .FirstOrDefault(m => m.ReviewID == reviewID);
            if (dbEntry != null)
            {
                context.Reviews.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
        /*public IEnumerable<Review> Reviews => new List<Review>
        {
            //a constructor from what I understood.
        };*/
    }
}
