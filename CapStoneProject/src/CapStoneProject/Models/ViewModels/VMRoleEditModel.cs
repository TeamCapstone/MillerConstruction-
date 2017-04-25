using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CapStoneProject.Models.ViewModels
{
    public class VMRoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<UserIdentity> Members { get; set; }
        public IEnumerable<UserIdentity> NonMembers { get; set; }
    }
}
