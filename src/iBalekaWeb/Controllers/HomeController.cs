using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;

namespace iBalekaWeb.Controllers
{
    public class HomeController : Controller
    {
        static ILogger _logger;
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public HomeController(ILoggerFactory factory)
        {
            if (_logger == null)
            {
                _logger = factory.CreateLogger("Unhandled Error");
            }
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
