using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using Xunit;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CapStoneProject.Controllers;
namespace CapStoneProject.Tests
{
    public class ReviewTests
    {
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IReviewRepo reviewrepo;
        private ReviewRepo rRepo;

        [Fact]
        public void GetCreateUser()
        {
            {
                //Arrange
                var u = new UserIdentity { FirstName = "Joel", LastName = "Silva", Password = "Password1!", ClientCreated = true };
                //Act

                //Assert
                Assert.Equal("Joel", u.FirstName);
                Assert.Equal("Password1!", u.Password);
                Assert.True(u.ClientCreated);
            }

        }

        [Fact]
        public void GetCreateReview()
        {
            //Arrange
            var u = new UserIdentity {FirstName = "Joel", LastName = "Silva", Password = "Password1!", ClientCreated = true };
            var r = new Review { ReviewID = 1, Approved = false, Body = "This is a Nunit test", Subject = "Test", From = u };

            //Assert
            Assert.NotNull(r);
        }
        [Fact]
        public void GetReviewName()
        {
            //Arrange
            var u = new UserIdentity { FirstName = "Joel", LastName = "Silva", Password = "Password1!", ClientCreated = true };
            var r = new Review { ReviewID = 1, Approved = false, Body = "This is a Nunit test", Subject = "Test", From = u };

            //Assert
            Assert.Equal("Joel",r.From.FirstName);
        }

    }
}
