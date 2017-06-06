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

        public ClientInfo(IClientRepo repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke(int id)
        {
            Client client = new Client();
            client = repository.GetClientById(id);
            return View(client);
            
        }

    }
}
