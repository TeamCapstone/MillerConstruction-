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

        public IEnumerable<User> Users
        {
            get
            {
                return context.Users.ToList();
            }
        }

        public int Create(User user)
        {
            context.Users.Add(user);
            return context.SaveChanges();
        }

        public List<string> GetAllEmails()
        {
            List<string> email = new List<string>();
            foreach (User u in Users)
            {            
                email.Add(u.Email);
            }
            return email;
        }

        public List<User> GetAllUsers()
        {
            return Users.ToList(); 
        }

        public User GetUser(string username)
        {
            return Users.First(u => u.Email == username);
        }

        public User GetUser(int id)
        {
            return Users.First(u => u.UserID == id);
        }
    }
}
