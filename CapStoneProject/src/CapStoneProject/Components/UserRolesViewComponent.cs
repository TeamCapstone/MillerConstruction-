﻿using CapStoneProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Components
{
    [ViewComponent(Name = "UserRolesVC")]
    public class UserRolesViewComponent : ViewComponent
    {       
        private RoleManager<IdentityRole> roleManager;
        private UserManager<UserIdentity> userManager;

        public UserRolesViewComponent(RoleManager<IdentityRole> roleMgr,
                                   UserManager<UserIdentity> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;
        }

        public IViewComponentResult Invoke()
        {
            
            return View(roleManager.Roles);
        }
    }
}
