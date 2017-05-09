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
    public class ClientTests
    {
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IClientRepo clientRepo;
        private ClientRepo cRepo;

        [Fact]
        public void GetCreateClient()
        {
            //Arrange
            var c = new Client { FirstName = "Oliver", LastName = "Bourland", Email = "oliver@gmail.com" };
            //Act

            //Assert
            Assert.Equal("Oliver", c.FirstName);
        }

        [Fact]
        public void GetGetClientByEmail()
        {
            //Arrange
            Client client = new Client();
            string email = "Hello1@gmail.com";
            //Act
            client = clientRepo.GetClientByEmail(email);
            //Assert
            Assert.Equal("Homes", client.LastName);
        }

        [Fact]
        public void GetGetClientByFirstName()
        {
            //Arrange
            Client client = new Client();
            string firstName = "Henry";
            //Act
            client = clientRepo.GetClientByFirstName(firstName);
            //Assert
            Assert.Equal("Henry", client.FirstName);
        }

        [Fact]
        public void GetGetClientById()
        {
            //Arrange
            Client client = new Client();
            int id = 2;
            //Act           
            client = clientRepo.GetClientById(id);
            //Assert
            Assert.Equal("Henry", client.FirstName);
        }

        //[Fact]
        //public void CreateClientController()
        //{
        //    //Arrange
        //    ClientController controller = new ClientController();
        //    VMRegister vm = new VMRegister { FirstName = "Tom", LastName = "Jones" };
        //    //Act
        //    Task<IActionResult> result = controller.Create(vm);
        //    //Assert
        //    Assert.Equal("Result", result.Result);
        //}

    }
}
