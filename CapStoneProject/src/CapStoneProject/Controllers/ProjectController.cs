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
                //create client
                Client altC = new Client();
                altC.Email = b.User.Email;
                altC.FirstName = b.User.FirstName;
                altC.LastName = b.User.LastName;
                altC.PhoneNumber = b.User.PhoneNumber;
                altC.UserIdentity = b.User;
                clientRepo.Create(altC);

                Client createdClient = clientRepo.GetClientByEmail(altC.Email);

                b.User.ClientCreated = true;
                
                projectVM.BidID = bidID;               
                projectVM.LastName = b.User.LastName;
                projectVM.ClientID = createdClient.ClientID;
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
                            Bid = bidrepo.GetBidByID(projectVM.BidID),
                            ProjectStatus = "Started"
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
        public IActionResult EditProject(int projectID) //for when the admin edits a project
        {
            Project project = projectRepo.GetProjectByID(projectID);
            VMEditProject projectVM = new VMEditProject {LastName = project.Client.LastName,
                FirstName = project.Client.FirstName, Email = project.Client.Email,
                ProjectName = project.ProjectName, OriginalEstimate = project.OriginalEstimate,
                StartDate = project.StartDate, Status = project.ProjectStatus,
                ProjectID = project.ProjectID, AdditionalCost = project.AdditionalCosts};

            return View(projectVM);
        }

        [HttpPost]
        public IActionResult EditProject(VMEditProject projectVM)
        {
            Project project = projectRepo.GetProjectByID(projectVM.ProjectID);

            if (ModelState.IsValid && projectVM.StartDate != null && projectVM.Status != null)
            {
                project.ProjectID = projectVM.ProjectID;
                project.AdditionalCosts = projectVM.AdditionalCost;
                project.TotalCost = projectVM.AdditionalCost + projectVM.OriginalEstimate;
                project.StartDate = projectVM.StartDate;
                project.ProjectStatus = projectVM.Status;
                project.StatusDate = DateTime.Today;

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
