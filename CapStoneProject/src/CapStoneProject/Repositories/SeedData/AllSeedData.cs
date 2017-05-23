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
                UserIdentity user = new UserIdentity
                {
                    FirstName = "Henry",
                    LastName = "Homes",
                    Email = "Hello1@gmail.com",
                    Password = "Capstone1!",
                    UserName = "Hello1@gmail.com"
                };

                string role = "User";
                await roleManager.CreateAsync(new IdentityRole(role));
                IdentityResult result = await userManager.CreateAsync(user, user.Password);
                await userManager.AddToRoleAsync(user, role);

                

                Client client = new Client
                {
                    FirstName = "Henry",
                    LastName = "Homes",
                    CompanyName = "Murder Company Inc.",
                    Street = "999 Elm Street",
                    City = "Eugene",
                    State = "OR",
                    Zipcode = "97405",
                    PhoneNumber = "541-548-9852",
                    UserIdentity = user
                };
                context.Clients.Add(client);

                Review message = new Review
                {
                    Subject = "Loved it!",
                    Body = "it them a bit more time than I though it would be but it was worth the wait!!",
                    From = user,
                    Approved = true,
                    Date = new DateTime(2006, 8, 22),

                };

                context.Add(message);

                UserIdentity user2 = new UserIdentity
                {
                    FirstName = "Sherlock",
                    LastName = "Homes",
                    Email = "bstreet@gmail.com",
                    Password = "Capstone1!",
                    UserName = "bstreet@gmail.com"

                };

                IdentityResult result2 = await userManager.CreateAsync(user2, user2.Password);
                await userManager.AddToRoleAsync(user2, role);
                //if (await roleManager.FindByNameAsync(role) == null)
                //{
                    
                //    if (result2.Succeeded)
                //    {
                //        await userManager.AddToRoleAsync(user2, role);
                //    }
                //}
                //else
                //{
                //    await roleManager.CreateAsync(new IdentityRole(role));
                //    if (result2.Succeeded)
                //    {
                //        await userManager.AddToRoleAsync(user2, role);
                //    }
                //}

                Client client2 = new Client
                {
                    FirstName = "Sherlock",
                    LastName = "Homes",
                    CompanyName = "Murder Company Inc.",
                    Street = "999 Elm Street",
                    City = "Eugene",
                    State = "OR",
                    Zipcode = "97405",
                    PhoneNumber = "541-548-9852",
                    UserIdentity = user2
                };

                context.Clients.Add(client2);

                UserIdentity user3 = new UserIdentity
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@gmail.com",
                    Password = "Capstone1!",
                    UserName = "john@gmail.com"

                };

                IdentityResult result3 = await userManager.CreateAsync(user3, user3.Password);
                await userManager.AddToRoleAsync(user3, role);
                //if (await roleManager.FindByNameAsync(role) == null)
                //{
                //    await roleManager.CreateAsync(new IdentityRole(role));
                //    if (result.Succeeded)
                //    {
                //        await userManager.AddToRoleAsync(user2, role);
                //    }
                //}
                //else
                //{
                //    await roleManager.CreateAsync(new IdentityRole(role));
                //    if (result3.Succeeded)
                //    {
                //        await userManager.AddToRoleAsync(user2, role);
                //    }
                //}

                message = new Review
                {
                    Subject = "Hated it!",
                    Body = "it them a bit more time than I though and it was not worth it!!",
                    From = user2,
                    Approved = false,
                    Date = new DateTime(2010, 8, 10),

                };

                context.Add(message);

                
                

                context.SaveChanges();
            }
        }
    }
}
