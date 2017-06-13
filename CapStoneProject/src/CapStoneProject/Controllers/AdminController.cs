using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CapStoneProject.Models.ViewModels;
using CapStoneProject.Models;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
       
        private UserManager<UserIdentity> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IReviewRepo reviewRepo;
        private IBidRequestRepo brRepo;
        private IBidRepo bidRepo;
        private readonly ApplicationDbContext context;
       

        /* private IUserValidator<UserIdentity> userValidator;
         private IPasswordValidator<UserIdentity> passwordValidator;
         private IPasswordHasher<UserIdentity> passwordHasher;*/


        public AdminController(RoleManager<IdentityRole> roleMgr, UserManager<UserIdentity> usrMgr, IReviewRepo repo, IBidRequestRepo brrepo, IBidRepo bidrepo, ApplicationDbContext Context)
        {
            userManager = usrMgr;
            roleManager = roleMgr;
            reviewRepo = repo;
            brRepo = brrepo;
            bidRepo = bidrepo;
            context = Context;
            /*userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;*/
        }

        public ViewResult Index() => View(userManager.Users);

        public ViewResult Create() => View();// Admin creates user

        [HttpPost]
        public async Task<IActionResult> Create(VMCreateUser model)// Admin creates user 
        {
            if (ModelState.IsValid)
            {
                UserIdentity user = new UserIdentity
                {
                    UserName = model.Name,
                    Email = model.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                string role = "User";

                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    if (result.Succeeded)
                    {                       
                        await userManager.AddToRoleAsync(user, role);
                        return RedirectToAction("User", "UserPage");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                        return RedirectToAction("User", "UserPage");
                    }
                }
            }
           
            return View(model);
         
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)// deletes the user
        {
            UserIdentity user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
              
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("Index", userManager.Users);
        }

        public async Task<IActionResult> Edit(string id)//Edits the user role
        {
            UserIdentity user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email,/*Edits the user role*/
                string password)
        {
            UserIdentity user = await userManager.FindByIdAsync(id);
            if (user != null)
            {

                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }              
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        //here----------------------------------------------------------------------------------
        public ActionResult ReviewPanel()
        {
           return View(reviewRepo.GetAllReviews().ToList());
        }

        public ViewResult EditReview(int ReviewID)
        {
            ViewBag.ReviewID = ReviewID;
           
            return View(reviewRepo.GetAllReviews().FirstOrDefault(m => m.ReviewID == ReviewID));

        }

        [HttpPost]
        public IActionResult EditReview(Review review)
        {
            if (ModelState.IsValid)
            {
                reviewRepo.Update(review);
                TempData["review"] = $"{review.ReviewID} has been saved";
                return RedirectToAction("AdminPage", "Admin");
            }
            else
            {
                return View(review);
            }
        }

        public IActionResult Comment() => RedirectToAction("Comment", "Review");

        [HttpPost]
        public IActionResult DeleteReview(int reviewID)
        {
            Review deletedMessage = reviewRepo.DeleteReview(reviewID);
            if (deletedMessage != null)
            {
                TempData["review"] = $"{deletedMessage.ReviewID} was deleted";
            }
            return RedirectToAction("AdminPage", "Admin");
        }


       [HttpPost]
        public async Task<IActionResult> ReviewPanel(string searchString)
        {
            
            var words = reviewRepo.GetAllReviews();
            
            if (!string.IsNullOrEmpty(searchString))
            { 
                words = words.Where(m => m.Subject.ToLower().Contains(searchString) || m.Body.ToLower().Contains(searchString) || m.From.FirstName.ToLower().Contains(searchString));
            }
            return View("ReviewPanel", await words.ToListAsync());
            
        }

        //filtering unfortantly redirects to a different page becuase refreshing the page cuases the panel to collapse
        [HttpPost]
        public async Task<IActionResult> BRFilter(string ss)
        {

            var words = brRepo.GetAllBidRequests();

            if (!string.IsNullOrEmpty(ss))
            {
                words = words.Where(br => br.User.LastName.ToLower().Contains(ss) || br.User.FirstName.ToLower().Contains(ss) || br.User.Email.ToLower().Contains(ss));
            }
            return RedirectToAction("AllBidRequests", "BidRequest", await words.ToListAsync());

        }

        //filtering
        [HttpPost]
        public async Task<IActionResult> BidFilter(string ss)
        {
            var words = bidRepo.GetAllBids();

            if (!string.IsNullOrEmpty(ss))
            {
                words = words.Where(b => b.User.LastName.ToLower().Contains(ss) || b.User.FirstName.ToLower().Contains(ss) || b.User.Email.ToLower().Contains(ss));
            }
            return View("AdminPage", await words.ToListAsync());

        }



        public ViewResult AdminPage()
        {
            return View();
        }
    }
}
