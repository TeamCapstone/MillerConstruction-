using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using CapStoneProject.Repositories;


namespace CapStoneProject.Components
{
    [ViewComponent(Name = "ProjectVC")]
    public class AdminProjViewComponent : ViewComponent
    {
        private ApplicationDbContext context;

        public AdminProjViewComponent(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await GetAllProjectsAsync());
        }

        private Task<List<Project>> GetAllProjectsAsync()
        {
            ProjectRepo projList = new ProjectRepo(context);
            return Task.FromResult(projList.GetAllProjects().ToList());
        }

    }
}
