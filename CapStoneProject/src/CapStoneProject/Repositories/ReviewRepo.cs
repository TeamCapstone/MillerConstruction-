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
        private User user;
        public ReviewRepo(ApplicationDbContext ctx)//going to take a instants of context
        {
            context = ctx;
        }

        public IQueryable<Review> GetAllReview()
        {
            return context.Reviews.Include(m => m.From).Include(m => m.Comments);

        }

        public List<Review> GetReviewBySubject(string subject)
        {
            return context.Reviews.Where(m => m.Subject == subject).Include(m => m.From).ToList();
        }

        public List<Review> GetReviewByUser(User user)
        {
            return context.Reviews.Where(m => m.From.UserID == user.UserID).ToList();
        }

        public void ReviewApporved(Review review)//What do you guys think
        {
            if (review.Approved == true)
            {
                Update(review);
                
            }
            else if (review.Approved == false)
            {
                DeleteReview(review.ReviewID);
                
            }
            else
            {
                if (review.Approved != true && review.Approved != false)
                {
                    throw new System.ArgumentNullException();
                  
                }
                else
                {
                    throw new System.ArgumentOutOfRangeException();
                }
            }
        }

        public int Update(Review review)
        {
            if (review.ReviewID == 0)
                context.Reviews.Add(review);
            else
                context.Reviews.Update(review);

            return context.SaveChanges();
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
        public IEnumerable<Review> Reviews => new List<Review>
        {
            //a constructor from what I understood.
        };
    }
}
