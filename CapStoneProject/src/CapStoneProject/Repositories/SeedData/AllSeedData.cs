using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Models.ViewModels;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CapStoneProject.Repositories.SeedData
{
    public class AllSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();

            if (!context.Reviews.Any() )
            {

                //////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////
                UserIdentity user = new UserIdentity
                {
                    FirstName = "Henry",
                    LastName = "Homes",
                    Email = "Hello1@gmail.com",
                    //we need to add a password

                };

                context.Users.Add(user);

                Review message = new Review
                {
                    Subject = "Loved it!",
                    Body = "it them a bit more time than I though it would be but it was worth the wait!!",
                    From = user,
                    Approved = true,
                    Date = new DateTime(2006, 8, 22),

                };

                context.Add(message);

                user = new UserIdentity
                {
                    FirstName = "Sherlock",
                    LastName = "Homes",
                    Email = "bstreet@gmail.com",
                    //we need to add a password

                };

                context.Users.Add(user);

                message = new Review
                {
                    Subject = "Hated it!",
                    Body = "it them a bit more time than I though and it was not worth it!!",
                    From = user,
                    Approved = false,
                    Date = new DateTime(2006, 8, 22),

                };

                context.Add(message);

                context.SaveChanges();
            }
        }
    }
}
