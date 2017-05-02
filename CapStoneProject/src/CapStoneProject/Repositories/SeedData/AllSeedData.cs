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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CapStoneProject.Repositories.SeedData
{
    public class AllSeedData
    {
        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<UserIdentity> userManager = app.ApplicationServices.GetRequiredService<UserManager<UserIdentity>>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
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
                    Password = "Capstone1!",
                    UserName = "Hello1@gmail.com"

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

                foreach (UserIdentity m in context.Users)
                {
                    string password = m.Password;
                    string role = "User";
                    IdentityResult result = await userManager.CreateAsync(m, password);
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(m, role);
                        }
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
