using CapStoneProject.Models;
using CapStoneProject.Models.ViewModels;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Components
{
    [ViewComponent(Name = "UserVC")]
    public class AdminUserViewComponent : ViewComponent
    {
        private IUserRepo userRepo;
        private IClientRepo clientRepo;

        public AdminUserViewComponent(IClientRepo clRepo, IUserRepo usrRepo)
        {
            userRepo = usrRepo;
            clientRepo = clRepo;
        }

        public IViewComponentResult Invoke()
        {
            List<UserIdentity> users = userRepo.GetAllUsers();
            VMUsersClients list = new VMUsersClients();
            foreach (UserIdentity u in users)
            {
                if (clientRepo.ContainsClient(u.Email))
                {
                    u.ClientCreated = true;
                }             
            }

            return View(users);

        }
    }
}
