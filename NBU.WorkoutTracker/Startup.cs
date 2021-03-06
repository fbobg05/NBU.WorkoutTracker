﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NBU.WorkoutTracker.Core.Contracts;
using NBU.WorkoutTracker.Core.Services;
using NBU.WorkoutTracker.Infrastructure.Data.Contexts;
using NBU.WorkoutTracker.Infrastructure.Data.Contracts;
using NBU.WorkoutTracker.Infrastructure.Data.Models;
using NBU.WorkoutTracker.Infrastructure.Data.Repositores;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WorkoutTrackerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MSSqlConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<WorkoutTrackerDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IAdmin, AdminService>();
            services.AddScoped<IWorkoutHistory, WorkoutHistoryService>();
            services.AddScoped<IRDBERepository<Workout>, RDBRepository<Workout>>();
            services.AddScoped<IRDBERepository<Exercise>, RDBRepository<Exercise>>();
            services.AddScoped<IRDBERepository<WorkoutExercise>, RDBRepository<WorkoutExercise>>();
            services.AddScoped<IRDBERepository<CompletedWorkout>, RDBRepository<CompletedWorkout>>();
            services.AddScoped<IRDBERepository<CompletedExercise>, RDBRepository<CompletedExercise>>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "parentchild",
                    template: "{controller}/{action}/{idparent}/{idchild}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
