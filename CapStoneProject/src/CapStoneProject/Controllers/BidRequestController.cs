using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;
using CapStoneProject.Models;
using CapStoneProject.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using MailKit.Net.Smtp;
using MailKit.Security;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CapStoneProject.Controllers
{
    public class BidRequestController : Controller
    {
        private IBidRequestRepo bidReqRepo;
        private IBidRepo bidRepo;
        protected UserManager<UserIdentity> UserManager;
        private CancellationToken taskCancellationToken;


        public BidRequestController(UserManager<UserIdentity> userMgr, IBidRequestRepo repo, IBidRepo bRepo)
        {
            bidReqRepo = repo;
            bidRepo = bRepo;
            UserManager = userMgr;

        }

        [HttpGet]
        public IActionResult BidRequest()
        {
            return View(new BidRequest());
        }

        [HttpPost]
        public async Task<IActionResult> BidRequest(BidRequest bidreq)
        {
            //TODO: check to see if email is alread in the database
            if (ModelState.IsValid)
            {

                UserIdentity user = new UserIdentity
                {
                    UserName = bidreq.User.Email,
                    Email = bidreq.User.Email,
                    FirstName = bidreq.User.FirstName,
                    LastName = bidreq.User.LastName
                };
                IdentityResult result = await UserManager.CreateAsync(user, bidreq.User.Password);

                /*var certificate = new X509Certificate2(@"C:\Users\silva\Desktop\ProjectStone\Credentials\CapstoneJOCA-7e6ff15dda38.p12", "notasecret", X509KeyStorageFlags.Exportable);
                var credential = new ServiceAccountCredential(new ServiceAccountCredential
                    .Initializer("capstonejoca@capstonejoca.iam.gserviceaccount.com")
                {
                    // Note: other scopes can be found here: https://developers.google.com/gmail/api/auth/scopes
                    Scopes = new[] { "https://mail.google.com/" },
                    User = "capstonejoca@capstonejoca.iam.gserviceaccount.com"//Domain Name
                }.FromCertificate(certificate));

                //You can also use FromPrivateKey(privateKey) where privateKey
                // is the value of the fiel 'private_key' in your serviceName.json file


                bool success = credential.RequestAccessTokenAsync(System.Threading.CancellationToken.None).Result;*/


                if (result.Succeeded)
                {


                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Admin", "jocaproject6@gmail.com"));
                    message.To.Add(new MailboxAddress("Admin", "jocaproject6@gmail.com"));
                    message.Subject = "bid Request Requested";

                    message.Body = new TextPart("plain")
                    {
                        Text = @"Hey Admin, A new bid request was created please look at it and respond."
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587,false);
                        client.AuthenticationMechanisms.Remove("XOAUTH2"); // Must be removed for Gmail SMTP
                        client.Authenticate("jocaproject6@gmail.com", "Admin1@gmail.com");
                        client.Send(message);
                        client.Disconnect(true);
                    }

                    /*using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587);

                        // use the OAuth2.0 access token obtained above as the password
                        client.Authenticate("jocaproject6@gmail.com", credential.Token.AccessToken);//need a domain name.

                        client.Send(message);
                        client.Disconnect(true);
                    }*/

                    bidReqRepo.Update(bidreq);
                    return RedirectToAction("Success");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(bidreq);
        }


        //[HttpGet]
        //public ViewResult Bid(int brID) => View(bidReqRepo.GetAllBidRequests()
        //    .FirstOrDefault(r => r.BidRequestID == brID));

        [HttpGet]
        public ViewResult Bid(int brID) => View(bidRepo.GetAllBids()
            .FirstOrDefault(r => r.BidReq.BidRequestID == brID));

        [HttpPost]
        public ViewResult Bid(Bid bid)
        {
            bidRepo.Update(bid);
            return View("AllBidRequests", bidReqRepo.GetAllBidRequests().ToList());
        }

      

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoggedInBidRequest()
        {
            return View(new BidRequest());
        }

        [HttpPost]
        public IActionResult LoggedInBidRequest(BidRequest bidreq)
        {
            //get the user
            var userID = UserManager.GetUserId(HttpContext.User);

            if (ModelState.IsValid)
            {
                bidReqRepo.Update(bidreq);
                return RedirectToAction("Success");
            }
            return View(bidreq);
        }

        public IActionResult AllBidRequests()
        {
            return View(bidReqRepo.GetAllBidRequests().ToList());
        }

        
    }
}
