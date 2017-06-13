using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapStoneProject.Models;
using CapStoneProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    //TODO Need to finish the ResetPassword View Page
    //TODO Need to test if the reset password works
    public class AccountController : Controller
    {
        // GET: /<controller>/
        private UserManager<UserIdentity> userManager;
        private SignInManager<UserIdentity> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IUserRepo userRepo;
        private IClientRepo clientRepo;
        private string Email = "postmaster@millercustomconstructioninc.com";
        private string Password = "Capstone132.";
        private const string Server = "m05.internetmailserver.net";
        private const int Port = 587;

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
                    //Everything that is commented in this controller is for email confirmation

                    /*if (!userManager.IsEmailConfirmedAsync(user).Result)
                    {
                        ModelState.AddModelError("", "Account not confirmed!");
                        return View(details);
                    }
                    else if (userManager.IsEmailConfirmedAsync(user).Result)
                    {*/
                        await signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result =
                                await signInManager.PasswordSignInAsync(
                                    user, details.Password, false, false);
                        if (result.Succeeded)
                        {
                            if (await userManager.IsInRoleAsync(user, "Admin"))
                                return RedirectToAction("AdminPage", "Admin");
                            else
                                return RedirectToAction("UserPage", "User");

                        }
                    /*}
                    else
                    {
                        ModelState.AddModelError(nameof(VMLoginModel.Email), "Account Not Verified");
                    }*/
                }
                ModelState.AddModelError(nameof(VMLoginModel.Email),
                    "Username or password does not match our records.");
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
                    //Added email confirmation validation
                    //Everything that is commented in this controller is for email confirmation

                    /*if (!userManager.IsEmailConfirmedAsync(user).Result)
                    {
                        ModelState.AddModelError("", "Account not confirmed!");
                        return View(vm);
                    }
                    else if(userManager.IsEmailConfirmedAsync(user).Result)
                    {*/
                    await signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result =
                                await signInManager.PasswordSignInAsync(
                                    user, vm.Password, false, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("LoggedInBidRequest", "BidRequest");
                        }
                    /*}
                    else
                    {
                        ModelState.AddModelError(nameof(VMLoginModel.Email), "Account Not Verified");
                    }*/
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

                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(vm);
            }
        }

        //This takes you to the confirm email page
        public IActionResult ConfirmEmail(string userid, string token)
        {
            UserIdentity user = userManager.FindByIdAsync(userid).Result;
            IdentityResult result = userManager.
                        ConfirmEmailAsync(user, token).Result;
            if (result.Succeeded)
            {
                ViewBag.Message = "Email confirmed successfully!";
                return View("ConfirmEmail");
            }
            else
            {
                ViewBag.Message = "Error while confirming your email!";
                return View("Error");
            }
        }

        /// 
        /// ////////////////////////////////////////////////////////
        /// Here is what I added below is all resetting the password
        ///

        [Route("Reset")]
        public IActionResult ForgotPassword()
        {
            return View("Reset");
        }

        [Route("Reset/Send")]
        public IActionResult SendPasswordResetLink(string username)
        {
            UserIdentity user = userManager.FindByNameAsync(username).Result;

            if (user == null)
            {
                return View("Reset");
            }

            var token = userManager.
                  GeneratePasswordResetTokenAsync(user).Result;

            var resetLink = Url.Action("ResetPassword",
                            "Account", new { token = token },
                             protocol: HttpContext.Request.Scheme);

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("MCCInc", Email));
            email.Subject = "Password Reset";
            email.Body = new TextPart("plain")
            {
                Text = "Click the link to reset your password " + resetLink
            };
            email.To.Add(new MailboxAddress(user.Email));

            using (var client = new SmtpClient())
            {

                client.Connect(Server, Port);


                client.Authenticate(Email, Password);

                client.Send(email);

                client.Disconnect(true);
            }

            //TODO: Pop up Alert box saying the reset link has been sent to their email
            return View("Login");
        }

        [Route("Reset/New/Password")]
        public IActionResult ResetPassword(string token)
        {
            return View("ResetPassword");
        }

        [HttpPost]
        [Route("Reset/New/Password")]
        public IActionResult ResetPassword(VMRestPassword vm)
        {
            if (vm.Password == vm.ConfirmPassword)
            {
                UserIdentity user = userManager.
                             FindByNameAsync(vm.UserName).Result;

                IdentityResult result = userManager.ResetPasswordAsync
                          (user, vm.Token, vm.Password).Result;
                if (result.Succeeded)
                {
                    return View("Login");
                }
                else
                {
                    return View("ResetPassword");
                }
            }
            return View("ResetPassword");
        }
    }
}
