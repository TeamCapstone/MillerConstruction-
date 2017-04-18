using CapStoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IUserRepo
    {
        List<User> GetAllUsers();
        List<string> GetAllEmails();
        IEnumerable<User> Users { get; }
        User GetUser(int id);
        User GetUser(string username);
        int Create(User user);
    }
}
