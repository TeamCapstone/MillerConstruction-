using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Infrastructure
{
    public class ClientInfo : ViewComponent
    {
        private IClientRepo repository;
        private IUserRepo userRepo;

        public ClientInfo(IClientRepo repo, IUserRepo usrRepo)
        {
            repository = repo;
            userRepo = usrRepo;
        }
        public IViewComponentResult Invoke(string email)
        {
            UserIdentity user = userRepo.GetUser(email);
            Client c = repository.GetClientByEmail(email);
            if (c != null)
            {
                Client client = new Client();
                client = repository.GetClientByEmail(email);
                return View(client);
            }
            else
            {
                c = new Client
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
                repository.Create(c);
                return View(c);
            }
            
        }

    }
}
