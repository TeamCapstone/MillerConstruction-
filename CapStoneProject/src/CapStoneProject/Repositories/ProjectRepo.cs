using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;

namespace CapStoneProject.Repositories
{
    public class ProjectRepo : IProjectRepo
    {
        private ApplicationDbContext context;

        public ProjectRepo(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Project> GetProjectsByClient(int clientID)
        {
            return context.Projects.Where(p => p.Client.ClientID == clientID);
        }

        public Project GetLatestClientProject(int clientID)
        {
            return context.Projects.Where(p => p.Client.ClientID == clientID)
                .LastOrDefault<Project>();
        }

        public IEnumerable<Project> GetCurrentProjects(int clientID)
        {
            //TODO: finish
            return context.Projects.Where(p => p.Client.ClientID == clientID
            && p.ProjectStatus != "Completed");
        }
    }
}
