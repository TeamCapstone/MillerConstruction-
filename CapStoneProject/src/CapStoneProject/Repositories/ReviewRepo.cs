﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CapStoneProject.Repositories
{
    public class ReviewRepo
    {
        private ApplicationDbContext context;
        private User user;

        public ReviewRepo(ApplicationDbContext ctx)//going to take a instants of context
        {
            context = ctx;
        }


        public IQueryable<Review> GetAllMessages()
        {
            return context.Reviews.Include(m => m.From).Include(m => m.Comments);
        }

        public List<Review> GetMessageBySubject(string subject)
        {
            return context.Reviews.Where(m => m.Subject == subject).Include(m => m.From).ToList();
        }
        public List<Review> GetMessageByMember(User user)
        {
            return context.Reviews.Where(m => m.From.UserID == user.UserID).ToList();
        }

        public int Update(Review review)
        {
            if (review.ReviewID == 0)
                context.Reviews.Add(review);
            else
                context.Reviews.Update(review);

            return context.SaveChanges();
        }
        public Review DeleteMessage(int reviewID)
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
    }
}
