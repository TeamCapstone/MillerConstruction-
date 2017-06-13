using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Tests
{
    public interface FakeReviewRepo 
    {
        IQueryable<Review> GetAllReviews();

        List<Review> GetReviewBySubject(string subject);

        List<Review> GetReviewByUser(UserIdentity user);

        //IEnumerable<Review> Reviews { get; }

        int Update(Review review);

        Review DeleteReview(int reviewID);

        IQueryable<Review> GetAllApproved();

        IQueryable<Review> GetAllDisapproved();

        Review GetReviewById(int id);
    }
}
