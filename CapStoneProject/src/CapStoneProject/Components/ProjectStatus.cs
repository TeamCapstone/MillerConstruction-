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
            Client client = new Models.Client();
            client = clientRepo.GetClientByEmail(email);
            List<Project> projects = new List<Project>();
            projects = repository.GetAllProjectsByClientId(client.ClientID);
            return View(projects);
        }
    }
}
