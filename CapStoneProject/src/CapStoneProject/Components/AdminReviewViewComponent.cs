using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using CapStoneProject.Repositories;

namespace CapStoneProject.Components
{
    [ViewComponent(Name = "ReviewVC")]
    public class AdminReviewViewComponent : ViewComponent
    {
        private ApplicationDbContext context;

        public AdminReviewViewComponent(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await GetAllReviewsAsync());
        }

        private Task<List<Review>> GetAllReviewsAsync()
        {
            ReviewRepo revList = new ReviewRepo(context);
            return Task.FromResult(revList.GetAllReviews().ToList());
        }
    }
}
