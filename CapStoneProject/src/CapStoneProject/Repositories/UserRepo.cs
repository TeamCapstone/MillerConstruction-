using CapStoneProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;

namespace CapStoneProject.Repositories
{
    public class UserRepo : IUserRepo
    {
        private ApplicationDbContext context;

        public UserRepo(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<UserIdentity> Users
        {
            get
            {
                return context.Users.ToList();
            }
        }

        public int Create(UserIdentity user)
        {
            context.Users.Add(user);
            return context.SaveChanges();
        }

        public int Edit(UserIdentity user)
        {
            context.Users.Update(user);
            return context.SaveChanges();
        }

        public List<string> GetAllEmails()
        {
            List<string> email = new List<string>();
            foreach (UserIdentity u in Users)
            {            
                email.Add(u.Email);
            }
            return email;
        }

        public List<UserIdentity> GetAllUsers()
        {
            return Users.ToList(); 
        }

        public IQueryable<UserIdentity> GetAllUsersFilter()
        {

            return context.Users;

        }

        public UserIdentity GetUser(string username)
        {
            return Users.First(u => u.Email == username);
        }

        public UserIdentity GetUserid(string id)
        {
            return Users.First(u => u.Id == id);
        }
    }
}
