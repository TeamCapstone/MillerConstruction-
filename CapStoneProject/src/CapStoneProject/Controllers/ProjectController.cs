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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateProject(int bidID)
        {
            VMCreateProject projectVM = new VMCreateProject(); //creates vm to pass to view
            Bid b = bidrepo.GetBidByID(bidID); //gets appropriate bid from database
            Client c = clientRepo.GetClientByEmail(b.User.Email); //looks for client from database
            if (c == null) //if client not found
            {
                //create client
                Client altC = new Client();
                altC.Email = b.User.Email;
                altC.FirstName = b.User.FirstName;
                altC.LastName = b.User.LastName;
                altC.PhoneNumber = b.User.PhoneNumber;
                altC.UserIdentity = b.User;
                clientRepo.Create(altC);

                //finds created client, now in database
                Client createdClient = clientRepo.GetClientByEmail(altC.Email);

                b.User.ClientCreated = true; //confirms client now exists

                //adds bid and client info to project object
                projectVM.BidID = bidID;               
                projectVM.LastName = b.User.LastName;
                projectVM.ClientID = createdClient.ClientID;
                projectVM.Email = b.User.Email;

            }
            else //if client is found
            {
                //adds client and bid to project
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
                            ProjectStatus = "Started", StatusDate = DateTime.Today
                        };
                        project.TotalCost = project.OriginalEstimate;

                        //if client and bid are valid
                        if (project.Client != null && project.Bid != null)
                        {
                            //add project to database
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
            //pulls project from database using passed-in ProjectID
            Project project = projectRepo.GetProjectByID(projectID);
            //posts necessary project attributes to viewmodel to pass to view
            VMEditProject projectVM = new VMEditProject {LastName = project.Client.LastName,
                FirstName = project.Client.FirstName, Email = project.Client.Email,
                ProjectName = project.ProjectName, OriginalEstimate = project.OriginalEstimate,
                StartDate = project.StartDate, Status = project.ProjectStatus,
                StatusDate = project.StatusDate,
                ProjectID = project.ProjectID, AdditionalCost = project.AdditionalCosts,
                CurrentTotal = project.TotalCost};

            return View(projectVM);
        }

        [HttpPost]
        public IActionResult EditProject(VMEditProject projectVM)
        {
            //double-checks database and pulls same project from database again
            Project project = projectRepo.GetProjectByID(projectVM.ProjectID);

            //if all necessary fields are filled in
            if (ModelState.IsValid && projectVM.Status != null)
            {
                //pass new values to appropriate fields
                project.ProjectID = projectVM.ProjectID;
                project.AdditionalCosts = projectVM.AdditionalCost;
                project.TotalCost = projectVM.AdditionalCost + project.OriginalEstimate;
                project.StartDate = projectVM.StartDate;
                project.ProjectStatus = projectVM.Status;
                project.StatusDate = DateTime.Today;

                //update project back into database
                projectRepo.ProjectUpdate(project);

                return RedirectToAction("AdminPage", "Admin");
            }
            else
            {
                ModelState.AddModelError("Status", "This field cannot be left blank");
            }

            return View(projectVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteProject(int projectID) //for when the admin edits a project
        {
            //creates viewmodel to hold projectid
            VMDeleteProject deleteProjVM = new VMDeleteProject();
            deleteProjVM.ProjectID = projectID;

            return View(deleteProjVM);
        }

        [HttpPost]
        public IActionResult DeleteProject(VMDeleteProject deleteProjVM)
        {
            //looks for project in db based on passed-in projectid from viewmodel
            Project project = projectRepo.GetProjectByID(deleteProjVM.ProjectID);
            
            //if project found
            if(project != null)
            {
                projectRepo.ProjectDelete(projectRepo.GetProjectByID(project.ProjectID));
            }


            return RedirectToAction("AdminPage", "Admin");
        }
    }
}
