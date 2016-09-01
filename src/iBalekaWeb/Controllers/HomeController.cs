using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using iBalekaWeb.Services;
using Microsoft.AspNetCore.Identity;
using iBalekaWeb.Models;
using iBalekaWeb.Models.HomeViewModels;

namespace iBalekaWeb.Controllers
{

    public class HomeController : Controller
    {
        private IEventService _context;
        private IRouteService _routeContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(IEventService _repo, UserManager<ApplicationUser> _user, IRouteService _rContext, ILoggerFactory factory)
        {
            if (_logger == null)
            {
                _logger = factory.CreateLogger("Unhandled Error");
            }
            _context = _repo;
            _routeContext = _rContext;
            _userManager = _user;
        }
        static ILogger _logger;
        [Authorize]
        public IActionResult Default()
        {
            HomeViewModel model = new HomeViewModel();
            int nrEvents = int.Parse(_context.GetEvents(_userManager.GetUserId(User)).Count().ToString());
            int nrRoutes = int.Parse(_routeContext.GetRoutes(_userManager.GetUserId(User)).Count().ToString());
            model.NumberOfEvents = nrEvents;
            model.NumberOfRoutes = nrRoutes;
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult About()
        {

            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
    
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

       
       
        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;
            _logger.LogError("Opps", error);
            return View("~/Views/Shared/Error.cshtml",error);
        }


    }
}
