 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CapStoneProject.Models;
using CapStoneProject.Repositories;
using CapStoneProject.Repositories.SeedData;
using CapStoneProject.Repositories.Interfaces;

namespace CapStoneProject
{
    public class Startup
    {
        IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration["Data:CapstoneAppItems:ConnectionString"]));
            services.AddIdentity<UserIdentity, IdentityRole>(opts => {

                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<IReviewRepo, ReviewRepo>();
            services.AddTransient<IClientRepo, ClientRepo>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IBidRequestRepo, BidRequestRepo>();
            services.AddTransient<IBidRepo, BidRepo>();
            services.AddMemoryCache();
            services.AddSession();
<<<<<<< HEAD
            services.AddTransient<IBidRequestRepo, BidRequestRepo>();
            services.AddTransient<IBidRepo, BidRepo>();
            services.AddTransient<IProjectRepo, ProjectRepo>();
=======
            services.AddMvc();

>>>>>>> master

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            loggerFactory.AddConsole();
            app.UseStaticFiles();
            app.UseSession();
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();

            ApplicationDbContext.CreateAdminAccount(app.ApplicationServices,
            Configuration).Wait();

<<<<<<< HEAD
            AllSeedData.EnsurePopulated(app).Wait();
=======
             AllSeedData.EnsurePopulated(app).Wait();
>>>>>>> master
        }
    }
}
