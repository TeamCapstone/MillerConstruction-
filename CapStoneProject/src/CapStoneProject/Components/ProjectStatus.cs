using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Components
{
    public class ProjectStatus : ViewComponent
    {
        private IProjectRepo repository;
        private IClientRepo clientRepo;

        public ProjectStatus(IProjectRepo repo, IClientRepo clRepo)
        {
            repository = repo;
            clientRepo = clRepo;
        }
        public IViewComponentResult Invoke(string email)
        {
            List<Project> projects = new List<Project>();
            Client client = new Models.Client();
            client = clientRepo.GetClientByEmail(email);
            if (client == null)
            {
                return View(projects);
            }
            else
            {
                
                projects = repository.GetAllProjectsByClientId(client.ClientID);
                return View(projects);
            }
            
        }
    }
}
