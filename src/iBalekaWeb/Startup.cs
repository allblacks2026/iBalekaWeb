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
using Hangfire;
using iBalekaWeb.Models;
using iBalekaWeb.Services;
using iBalekaWeb.Data.Configurations;
using iBalekaWeb.Data.Infastructure;
using Microsoft.AspNetCore.Mvc;
using iBalekaWeb.Data.Infrastructure;
using iBalekaWeb.Data.iBalekaAPI;
using iBalekaWeb.Controllers.Filters;
using Microsoft.AspNetCore.Http;
using Hangfire.Dashboard;

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
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("ServerConnection")));
            services.AddMvc()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
            services.AddCors();
            // Add framework services.
            services.AddDbContext<iBalekaDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ServerConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(i => {
                     i.SecurityStampValidationInterval = TimeSpan.FromDays(7);
                    })
                .AddEntityFrameworkStores<iBalekaDBContext>()
                .AddDefaultTokenProviders();
            services.AddDistributedMemoryCache();
            services.AddSession();
            

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            //repos
            services.AddScoped<IHangfireTasks, HangFireTasks>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApiClient, ApiClient>();
            services.AddScoped<IMapClient, MapClient>();
            services.AddScoped<IClubClient, ClubClient>();
            services.AddScoped<IClubMemberClient, ClubMemberClient>();
            services.AddScoped<IEventClient, EventClient>();
            services.AddScoped<IEventRegistration, EventRegistrationClient>();
            //services
            

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider svp)
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
                
                app.UseExceptionHandler("/Shared/Error");
            }
            app.UseCors(builder =>
                builder.WithOrigins("http://https://ibalekaapi.azurewebsites.net/")
                    .AllowAnyHeader()
                );            
            
            app.UseStaticFiles();
            app.UseSession();
            //app.UseIISPlatformHandler();
            app.UseIdentity();            
            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = Configuration["Authentication:Facebook:AppId"],
                AppSecret = Configuration["Authentication:Facebook:AppSecret"]
            });


            app.UseGoogleAuthentication(new GoogleOptions()
            {
                ClientId = Configuration["Authentication:Google:AppId"],
                ClientSecret = Configuration["Authentication:Google:AppSecret"]
            });
            
            var hangFireOptions = new DashboardOptions { Authorization = Enumerable.Empty<IDashboardAuthorizationFilter>() };
            if (env.IsDevelopment())
                app.UseHangfireDashboard("/dashboard");
            //else
            //    app.UseHangfireDashboard("/dashboard", hangFireOptions);
            app.UseHangfireServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
