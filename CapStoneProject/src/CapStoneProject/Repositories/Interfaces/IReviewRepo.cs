using System;
using System.Collections.Generic;
using System.Linq;
using CapStoneProject.Models;
using CapStoneProject.Repositories;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IReviewRepo
    {
        IQueryable<Review> GetAllReview();

        List<Review> GetReviewBySubject(string subject);

        List<Review> GetReviewByUser(User user);

        IEnumerable<Review> Reviews { get; }

        int Update(Review review);

        Review DeleteReview(int reviewID);

        void ReviewApporved(Review review);
    }
}
