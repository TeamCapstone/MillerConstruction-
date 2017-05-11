using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapStoneProject.Models;
using CapStoneProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: /<controller>/
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IUserRepo userRepo;
        private IClientRepo clientRepo;

        public AccountController(UserManager<UserIdentity> userMgr,
                SignInManager<UserIdentity> signinMgr, RoleManager<IdentityRole> roleMgr,
                IClientRepo crepo, IUserRepo urepo)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            roleManager = roleMgr;
            clientRepo = crepo;
            userRepo = urepo;
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(VMLoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserIdentity user = await userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await signInManager.PasswordSignInAsync(
                                user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(VMLoginModel.Email),
                    "Invalid user or password");
            }
            return View(details);
        }

        [AllowAnonymous]
        public IActionResult BRLogin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BRLogin(VMLoginModel vm)
        {
            if (ModelState.IsValid)
            {
                UserIdentity user = await userManager.FindByEmailAsync(vm.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await signInManager.PasswordSignInAsync(
                                user, vm.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("LoggedInBidRequest", "BidRequest");
                    }
                }
                ModelState.AddModelError(nameof(VMLoginModel.Email),
                    "Invalid user or password");
            }
            return View(vm);

         }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Register()//Creates a Client not a user links to a user
        {
            return View(new VMRegister());
        }

        [HttpPost]
        public async Task<IActionResult> Register(VMRegister vm)//Creates a client not a user links to a user
        {
            if (ModelState.IsValid)
            {
                UserIdentity user = new UserIdentity();
                string name = HttpContext.User.Identity.Name;
                user = await userManager.FindByNameAsync(name);

                Client client = new Client
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    CompanyName = vm.CompanyName,
                    Street = vm.Street,
                    City = vm.City,
                    State = vm.State,
                    Zipcode = vm.Zipcode,
                    PhoneNumber = vm.PhoneNumber,
                    UserIdentity = user,
                    Email = user.Email
                    
                };

                clientRepo.Create(client);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(vm);
            }
        }
    }
}
