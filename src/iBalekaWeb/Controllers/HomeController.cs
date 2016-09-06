using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using iBalekaWeb.Models;
using iBalekaWeb.Models.HomeViewModels;
using iBalekaWeb.Data.iBalekaAPI;
using iBalekaWeb.Models.Responses;

namespace iBalekaWeb.Controllers
{

    public class HomeController : Controller
    {
        private IEventClient _context;
        private IMapClient _routeContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(IEventClient _repo, UserManager<ApplicationUser> _user, IMapClient _rContext, ILoggerFactory factory)
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
            ListModelResponse<Event> routeResponse = _context.GetUserEvents(_userManager.GetUserId(User));
            if (routeResponse.DidError == true || routeResponse == null)
            {
                if (routeResponse == null)
                    return View("Error");
                Error er = new Error(routeResponse.ErrorMessage);
                return View("Error");
            }
            ListModelResponse<Route> eventResponse = _routeContext.GetUserRoutes(_userManager.GetUserId(User));
            if (eventResponse.DidError == true || eventResponse == null)
            {
                if (eventResponse == null)
                    return View("Error");
                Error er = new Error(eventResponse.ErrorMessage);
                return View("Error");
            }
            int nrEvents = eventResponse.Model.Count();
            int nrRoutes = routeResponse.Model.Count();
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

        public IActionResult EmailUs()
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
