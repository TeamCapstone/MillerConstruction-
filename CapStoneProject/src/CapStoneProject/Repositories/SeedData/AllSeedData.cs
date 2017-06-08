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
                    PhoneNumber = "(541) 548-9852",
                    Email = "Hello1@gmail.com",
                    UserIdentity = user
                };
                context.Clients.Add(client);

                BidRequest bidRequest = new BidRequest
                {
                    User = user,
                    Concrete = false,
                    FrameWork = false,
                    NewBuild = true,
                    ProjectDescription = "Big deck",
                    ProjectLocation = "Lane county",
                    Remodel = false
                };
                context.BidRequests.Add(bidRequest);

                Bid bid = new Bid
                {
                    ProposedStartDate = DateTime.Now,
                    TotalEstimate = 6548.06M,
                    RevisedProjectDescription = "Wood deck with railing",
                    BidReq = bidRequest,
                    User = user
                };
              
              
                context.Bids.Add(bid);

                Project project1 = new Project
                {
                    Client = client,
                    Bid = bid,
                    ProjectName = "Wood Deck",
                    OriginalEstimate = 6548.06M,
                    StartDate = DateTime.Now,
                    ProjectStatus = "Framing Complete",
                    StatusDate = DateTime.Now,
                    AdditionalCosts = 1.94M,
                    TotalCost = 6550M
                };
                context.Projects.Add(project1);


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
                    PhoneNumber = "(541) 548-9852",
                    Email = "bstreet@gmail.com",
                    UserIdentity = user2
                };

                context.Clients.Add(client2);

                BidRequest bidRequest2 = new BidRequest
                {
                    User = user2,
                    Concrete = false,
                    FrameWork = false,
                    NewBuild = false,
                    ProjectDescription = "Remodel Bathroom",
                    ProjectLocation = "Linn county",
                    Remodel = true
                };
                context.BidRequests.Add(bidRequest2);

                Bid bid2 = new Bid
                {
                    ProposedStartDate = DateTime.Now,
                    TotalEstimate = 2058.06M,
                    RevisedProjectDescription = "Test bid for test project",
                    BidReq = bidRequest2,
                    User = user2
                    
                };
               
                context.Bids.Add(bid2);

                Project project2 = new Project
                {
                    Client = client2,
                    Bid = bid2,
                    ProjectName = "Bathroom Remodel",
                    OriginalEstimate = 2058.06M,
                    StartDate = DateTime.Now,
                    ProjectStatus = "Started",
                    StatusDate = DateTime.Now,
                    AdditionalCosts = 2.94M,
                    TotalCost = 2061.00M
                };
                context.Projects.Add(project2);

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







                /*this adds a test project to seeddata. this needs to create a 
                client, bid and bidrequest in order to do so*/

                UserIdentity user4 = new UserIdentity
                {
                    FirstName = "Ricky",
                    LastName = "Bobby",
                    Email = "bobby@gmail.com",
                    Password = "Capstone1!",
                    UserName = "bobby@gmail.com"

                };

                IdentityResult result4 = await userManager.CreateAsync(user4, user4.Password);
                await userManager.AddToRoleAsync(user4, role);

                UserIdentity user5 = new UserIdentity
                {
                    FirstName = "Blue",
                    LastName = "Sue",
                    Email = "sue@gmail.com",
                    Password = "Capstone1!",
                    UserName = "sue@gmail.com"
                };

                
                await roleManager.CreateAsync(new IdentityRole(role));
                IdentityResult result5 = await userManager.CreateAsync(user5, user5.Password);
                await userManager.AddToRoleAsync(user5, role);

                ////finds user
                //UserIdentity bidUser = await userManager.FindByEmailAsync("bobby@gmail.com");

                ////creates bidrequest from
                //BidRequest bidReq3 = new BidRequest
                //{
                //    User = bidUser,
                //    Concrete = false,
                //    FrameWork = false,
                //    NewBuild = true,
                //    ProjectDescription = "Test bidrequest",
                //    ProjectLocation = "Your house",
                //    Remodel = false
                //};
                //context.BidRequests.Add(bidReq3);

                ////creates bid from user Henry Holmes
                //Bid bid3 = new Bid
                //{
                //    ProposedStartDate = DateTime.Now,
                //    TotalEstimate = 6548.06M,
                //    RevisedProjectDescription = "Test bid for test project",
                //    BidReq = bidReq3
                //};
                //bid.User = bidUser;
                ////if (bid.User != null)
                ////{
                //    context.Bids.Add(bid3);
                ////}

                ////creates client from Henry
                //Client client4 = new Client();
                //UserIdentity clientUser = bidUser;
                //client4.UserIdentity = clientUser;
                ////if (client.UserIdentity != null)
                ////{
                //    client4.FirstName = clientUser.FirstName;
                //    client4.LastName = clientUser.LastName;
                //    client4.Email = clientUser.Email;
                ////}
                ////else
                ////{
                ////    client.FirstName = "Ricky";
                ////    client.LastName = "Bobby";
                ////    //client.Email = "TalladegaNights@gmail.com";
                ////}
                //context.Clients.Add(client4);

                ////creates project
                //Project project = new Project
                //{
                //    Client = client4,
                //    Bid = bid3,
                //    ProjectName = "Test Project",
                //    OriginalEstimate = 6548.06M,
                //    StartDate = DateTime.Now,
                //    ProjectStatus = "Waiting for permit.",
                //    StatusDate = DateTime.Now,
                //    AdditionalCosts = 1.94M,
                //    TotalCost = 6550M
                //};
                //context.Projects.Add(project);

                context.SaveChanges();
            }
        }
    }
}
