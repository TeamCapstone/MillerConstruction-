using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Repositories;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IReviewRepo
    {
        IQueryable<Review> GetAllMessages();

        List<Review> GetMessageBySubject(string subject);

        List<Review> GetMessageByMember(User user);

        IEnumerable<Review> Messages { get; }

        int Update(Review message);

        Review DeleteMessage(int messageID);
    }
}
