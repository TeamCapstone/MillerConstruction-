using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IProjectRepo
    {
        IEnumerable<Project> GetProjectsByClient(int clientID); //Gets client's projects by client id
        Project GetLatestClientProject(int clientID); //Gets latest client project
        IEnumerable<Project> GetCurrentProjects(int clientID); //gets current client projects
    }
}
