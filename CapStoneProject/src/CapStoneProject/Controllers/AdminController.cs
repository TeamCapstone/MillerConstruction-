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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<UserIdentity> userManager;
        private IReviewRepo reviewRepo;
        private readonly ApplicationDbContext context;

        /* private IUserValidator<UserIdentity> userValidator;
         private IPasswordValidator<UserIdentity> passwordValidator;
         private IPasswordHasher<UserIdentity> passwordHasher;*/


        public AdminController(UserManager<UserIdentity> usrMgr, IReviewRepo repo, ApplicationDbContext Context)
        {
            userManager = usrMgr;
            reviewRepo = repo;
            context = Context;
            /*userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;*/
        }

        public ViewResult Index() => View(userManager.Users);

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(VMCreateUser model)
        {
            if (ModelState.IsValid)
            {
                UserIdentity user = new UserIdentity
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                IdentityResult result
                    = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
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

        public async Task<IActionResult> Edit(string id)
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
        public async Task<IActionResult> Edit(string id, string email,
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

        public IActionResult EditReview(int ReviewID)
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
                return RedirectToAction("ReviewPanel");
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
            return RedirectToAction("ReviewPanel");
        }

        //here

       [HttpPost]
        public async Task<IActionResult> ReviewPanel(string searchString)
        {
            var words = reviewRepo.GetAllReviews();
            if (!string.IsNullOrEmpty(searchString))
            { 
                words = words.Where(m => m.Subject.Contains(searchString) || m.Body.Contains(searchString) || m.From.FirstName.Contains(searchString));
            }
            return View(await words.ToListAsync());
        }
    }
}
