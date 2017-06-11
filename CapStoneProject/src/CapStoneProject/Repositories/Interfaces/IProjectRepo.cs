using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IProjectRepo
    {
        IEnumerable<Project> GetAllProjects(); //Gets ALL projects in database
        IEnumerable<Project> GetAllCurrentProjects(); //Gets all ongoing projects in database
        IEnumerable<Project> GetProjectsByClient(int clientID); //Gets client's projects by client id
        Project GetLatestClientProject(int clientID); //Gets latest client project
        IEnumerable<Project> GetCurrentProjects(int clientID); //gets current client projects
        int ProjectUpdate(Project project); //Updates db context
        Project GetProjectByID(int projectID);
        List<Project> GetAllProjectsByClientId(int id);
        int ProjectDelete(Project project);
        bool HasClient(Client client);
    }
}
