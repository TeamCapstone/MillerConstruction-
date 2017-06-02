using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using CapStoneProject.Repositories;

namespace CapStoneProject.Infrastructure
{
    [ViewComponent(Name = "ClientVC")]
    public class AdminClientViewComponent : ViewComponent
    {
        private ApplicationDbContext context;

        public AdminClientViewComponent(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await GetAllClientsAsync());
        }

        private Task<List<Client>> GetAllClientsAsync()
        {
            ClientRepo clientList = new ClientRepo(context);
            return Task.FromResult(clientList.GetAllClients().ToList());
        }
    }
}
