using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CapStoneProject.Models.ViewModels;
using CapStoneProject.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<UserIdentity> userManager;
       /* private IUserValidator<UserIdentity> userValidator;
        private IPasswordValidator<UserIdentity> passwordValidator;
        private IPasswordHasher<UserIdentity> passwordHasher;*/


        public AdminController(UserManager<UserIdentity> usrMgr/*,
                IUserValidator<UserIdentity> userValid,
                IPasswordValidator<UserIdentity> passValid,
                IPasswordHasher<UserIdentity> passwordHash*/)
        {
            userManager = usrMgr;
            /*userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;*/
        }

        public ViewResult Index() => View(userManager.Users);

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(VMCreateModel model)
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
    }
}
