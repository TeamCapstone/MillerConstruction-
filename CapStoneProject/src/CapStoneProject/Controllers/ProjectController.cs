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
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;

        public ProjectController(ApplicationDbContext ctx, IProjectRepo projRepo, 
            UserManager<UserIdentity> usrMgr, SignInManager<UserIdentity> signInMgr)
        {
            context = ctx;
            projectRepo = projRepo;
            userManager = usrMgr;
            signInManager = signInMgr;
        }

        [Authorize]
        public ViewResult Index(string projStatus) //This one is for admin to view currentprojects
        {
            return View(projectRepo.GetAllCurrentProjects(projStatus));
        }

        [Authorize]
        public IActionResult ClientProjects(int clientID) //This one is for a client to view his/her projects
        {
            return View("ClientProjects", projectRepo.GetProjectsByClient(clientID));
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateProject() //for when the admin creates a project
        {
            var projectVM = new VMCreateProject();

            return View(projectVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(VMCreateProject projectVM)
        {
            //find user in db by email
            UserIdentity user = await userManager.FindByEmailAsync(projectVM.Email);
            //if user is found
            if (user != null)
            {
                //if model requirements fulfilled
                if (ModelState.IsValid)
                {
                    //finds user in Clients based on Identity

                    //TODO: Fill in Client
                    Project project = new Project {
                        ProjectName = projectVM.ProjectName,
                        StartDate = projectVM.StartDate, OriginalEstimate = projectVM.Estimate};
                }
                else
                {
                    //if model not valid
                }
            }
            else
            {
                //if user not found
            }
            

            return View();
        }
    }
}
