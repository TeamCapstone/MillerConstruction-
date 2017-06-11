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

        public IEnumerable<Project> GetAllProjects()
        {
            return context.Projects;
        }

        public IEnumerable<Project> GetAllCurrentProjects()
        {
            return context.Projects.Where(p => p.ProjectStatus != "Completed");
        }

        public Project GetProjectByID(int projectID)
        {
            return context.Projects.Where(p => p.ProjectID == projectID).Include(p => p.Bid).Include(p => p.Client).FirstOrDefault();
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
            return context.Projects.Where(p => p.Client.ClientID == clientID
            && p.ProjectStatus != "Completed");
        }

        public int ProjectUpdate(Project project)
        {
            if (project.ProjectID == 0)
                context.Projects.Add(project);
            else
                context.Projects.Update(project);

            return context.SaveChanges();
        }

        public int ProjectDelete(Project project)
        {
            Project proj = context.Projects.Where(p => p.ProjectID == project.ProjectID).FirstOrDefault();

            if (proj != null)
            {
                context.Projects.Remove(project);
            }

            return context.SaveChanges();
        }

        public bool HasClient(Client client)
        {
            Project project = context.Projects.FirstOrDefault(p => p.Client == client);
            if (project != null)
            {
                return true;
            }

            return false;
        }

        public int ProjectUpdate(Client client)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetAllProjectsByClientId(int i)
        {
            return (from t in context.Projects
                    where t.Client.ClientID == i
                    select t).ToList();
        }
    }
}
