using CapStoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IUserRepo
    {
        List<UserIdentity> GetAllUsers();
        List<string> GetAllEmails();
        IEnumerable<UserIdentity> Users { get; }
        UserIdentity GetUser(int id);
        UserIdentity GetUser(string username);
        int Create(UserIdentity user);
    }
}
