using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using iBalekaWeb.Models;
using iBalekaWeb.Services;
using iBalekaWeb.Data.Configurations;
using iBalekaWeb.Data.Repositories;
using iBalekaWeb.Data.Infastructure;

namespace iBalekaWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
           services.AddDbContext<iBalekaDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AspNetUsers, IdentityRole>()
                .AddEntityFrameworkStores<iBalekaDBContext>()
                .AddDefaultTokenProviders();
            
            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            
            //repos
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAthleteRepository, AthleteRepository>();
            services.AddScoped<IClubMemberRepository, ClubMemberRepository>();
            services.AddScoped<IClubRepository, ClubRepository>();
            services.AddScoped<IEventRegRepository, EventRegistrationRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IRunRepository, RunRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services
            services.AddScoped<IAthleteService, AthleteService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IClubMemberService, ClubMemberService>();
            services.AddScoped<IEventRegService, EventRegistrationService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IRunService, RunService>();
            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRouteService, RouteService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
