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

        public ProjectStatus(IProjectRepo repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke(int id)
        {
            List<Project> projects = new List<Project>();
            projects = repository.GetAllProjectsByClientId(id);
            return View(projects);
        }
    }
}
