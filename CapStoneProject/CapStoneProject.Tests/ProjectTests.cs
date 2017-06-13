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
        private IClientRepo cRepo;
        private FakeProjectRepo pRepo;

        [Fact]
        public void CreateProject() //TODO: add a moq framework
        {
            //Arrange client
            var client = new Client {FirstName = "Cody", LastName = "Noteboom", Email = "cnote@gmail.com"};
            //Arrange bid
            var bid = new Bid { TotalEstimate = 4500.67M };
            //Arrange project
            var project = new Project {ProjectName = "Kitchen Remodel" };
            var project2 = new Project();

            //Act
            project.Client = client;
            project.Bid = bid;
            projectRepo.ProjectUpdate(project);
            project2 = projectRepo.GetProjectsByClient(client.ClientID).FirstOrDefault();

            //Assert
            Assert.Equal("Cody", project.Client.FirstName);
            Assert.NotEqual(project.ProjectID, project2.ProjectID);
        }

        [Fact]
        public void EditProject()
        {
            //arrange
            var project = projectRepo.GetProjectByID(1);
            //act
            project.ProjectName = "waka waka";
            projectRepo.ProjectUpdate(project);
            var project1 = projectRepo.GetProjectByID(1);
            //assert
            Assert.Equal("waka waka", project1.ProjectName);
        }
    }
}
