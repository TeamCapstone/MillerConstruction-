using System;
using System.Collections.Generic;
using System.Linq;
using CapStoneProject.Models;
using CapStoneProject.Repositories;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IReviewRepo
    {
        IQueryable<Review> GetAllReviews();

        List<Review> GetReviewBySubject(string subject);

        List<Review> GetReviewByUser(UserIdentity user);

        //IEnumerable<Review> Reviews { get; }

        int Update(Review review);

        Review DeleteReview(int reviewID);

        IQueryable<Review> GetAllApproved();

        IQueryable<Review> GetAllDisapproved();

    }
}
