using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CapStoneProject.Models;
using CapStoneProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext context;
        private IProjectRepo projectRepo;
        private IClientRepo clientRepo;
        private IBidRepo bidrepo;
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;

        public ProjectController(ApplicationDbContext ctx, IProjectRepo projRepo, IClientRepo cliRepo,
            IBidRepo bdRepo, UserManager<UserIdentity> usrMgr, SignInManager<UserIdentity> signInMgr)
        {
            context = ctx;
            projectRepo = projRepo;
            clientRepo = cliRepo;
            bidrepo = bdRepo;
            userManager = usrMgr;
            signInManager = signInMgr;
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Index() //This one is for admin to view currentprojects
        {
            return View(projectRepo.GetAllCurrentProjects());
        }

        [Authorize]
        public IActionResult ClientProjects(int clientID) //This one is for a client to view his/her projects
        {
            return View("ClientProjects", projectRepo.GetProjectsByClient(clientID));
        }

        //
        //[HttpGet]
        //public IActionResult CreateProject(int bidID, int clientID) //for when the admin creates a project
        //{
        //    var projectVM = new VMCreateProject();
        //    projectVM.BidID = bidID;
        //    projectVM.ClientID = clientID;

        //    return View(projectVM);
        //}

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateProject(int bidID)
        {
            VMCreateProject projectVM = new VMCreateProject();
            Bid b = bidrepo.GetBidByID(bidID);
            Client c = clientRepo.GetClientByEmail(b.User.Email);
            if (c == null)
            {
                Client altC = new Client();
                projectVM.ClientID = altC.ClientID;
                projectVM.BidID = bidID;               
                projectVM.LastName = b.User.LastName;
                projectVM.Email = b.User.Email;

            }
            else
            {
                projectVM.BidID = bidID;
                projectVM.ClientID = c.ClientID;
                projectVM.LastName = b.User.LastName;
                projectVM.Email = b.User.Email;
            }
            

            return View(projectVM);
        }

      


        [HttpPost]
        public IActionResult CreateProject(VMCreateProject projectVM)
        {
            //if model requirements fulfilled
            if (ModelState.IsValid)
            {
                //finds user in Clients based on Email from user
                Client client = clientRepo.GetClientByEmail(projectVM.Email);

                if (clientRepo.ContainsClient(client) == true) //(client != null)
                {
                    //if client is entered and found

                    //searches for open bid for given client
                    Bid bid = bidrepo.GetBidByID(projectVM.BidID);
                    
                    if(bid != null)
                    {
                        //create project
                        Project project = new Project
                        {
                            ProjectID = projectVM.ProjectID,
                            Client = clientRepo.GetClientById(projectVM.ClientID),
                            ProjectName = projectVM.ProjectName,
                            StartDate = projectVM.StartDate,
                            OriginalEstimate = projectVM.Estimate,
                            Bid = bidrepo.GetBidByID(projectVM.BidID)
                        };

                        if(project.Client != null && project.Bid != null)
                        {
                            projectRepo.ProjectUpdate(project);
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "Either the attributed Client or Bid is invalid");
                        }

                        return RedirectToAction("AdminPage", "Admin");
                    }
                    else
                    {
                        //if bid not found
                        ModelState.AddModelError("Email", "Could not find open bid for client tied to that email");
                    }
                }
                else
                {
                    //if user not found
                    ModelState.AddModelError("LastName", "There is no client found in the system with that e-mail");
                }
            }
            else
            {
                //if model not valid
                ModelState.AddModelError("Email", "Please make sure all fields are filled");
            }

            return View(projectVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditProject(int projectID, DateTime date) //for when the admin edits a project
        {
            var project = projectRepo.GetProjectByID(projectID);
            var projectVM = new VMEditProject {LastName = project.Client.LastName,
                Email = project.Client.Email, ProjectName = project.ProjectName,
                Estimate = project.TotalCost, StartDate = project.StartDate,
                Status = project.ProjectStatus, StatusDate = date,
                ProjectID = project.ProjectID};

            return View(projectVM);
        }

        [HttpPost]
        public IActionResult EditProject(VMEditProject projectVM)
        {
            Project project = projectRepo.GetProjectByID(projectVM.ProjectID);

            if (ModelState.IsValid && projectVM.StartDate != null && projectVM.Status != null
                && projectVM.StatusDate != null)
            {
                project.TotalCost = projectVM.Estimate;
                project.StartDate = projectVM.StartDate;
                project.ProjectStatus = projectVM.Status;
                project.StatusDate = projectVM.StatusDate;

                projectRepo.ProjectUpdate(project);

                return RedirectToAction("AdminPage", "Admin");
            }
            else
            {
                ModelState.AddModelError("StartDate", "This field cannot be left blank");
            }

            return View(projectVM);
        }
    }
}
