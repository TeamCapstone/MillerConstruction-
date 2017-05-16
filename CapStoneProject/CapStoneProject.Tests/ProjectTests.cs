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
using CapStoneProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CapStoneProject.Tests
{
    public class ProjectTests
    {
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IProjectRepo projectRepo;
        private IClientRepo clientRepo;
        private IBidRepo bidRepo;
        private ClientRepo cRepo;

        [Fact]
        public void CreateProject()
        {
            //Arrange client
            var client = new Client {FirstName = "Cody", LastName = "Noteboom", Email = "cnote@gmail.com"};
            //Arrange bid
            var bid = new Bid { TotalEstimate = 4500.67M };
            //Arrange project
            var project = new Project {ProjectName = "Kitchen Remodel" };

            //Act
            project.Client = client;
            project.Bid = bid;

            //Assert
            Assert.Equal("Cody", project.Client.FirstName);
        }
    }
}
