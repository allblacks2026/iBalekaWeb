using System.Linq;
using Microsoft.AspNetCore.Mvc;
using iBalekaWeb.Models;
using iBalekaWeb.Services;
using System.Collections.Generic;
using iBalekaWeb.Models.MapViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

//using prototypeWeb.Models;

namespace iBalekaWeb.Controllers
{
    public class MapController : Controller
    {
        private IRouteService _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MapController(IRouteService _repo,UserManager<ApplicationUser> userManger)
        {
            _userManager = userManger;
            _context = _repo;
        }

        [Authorize]
        public IActionResult Map()
        {
            ViewData["Message"] = "Map A Route";
            return View();
        }
        
        //// GET: Map/SavedRoutes
        [HttpGet(Name ="SavedRoutes")]
        public IActionResult SavedRoutes()
        {
            IEnumerable<Route> routes = _context.GetRoutes(_userManager.GetUserId(User));
            return View(routes);
        }

        //// POST: Map/AddRoute
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult AddRoute([FromBody]RouteViewModel route)
        {
            RouteViewModel newRoute = route;
            if (ModelState.IsValid)
            {
                route.UserID = _userManager.GetUserId(User);
                _context.AddRoute(route);
                _context.SaveRoute();
                string url = Url.Action("SavedRoutes", "Map");
                return Json(new { Url = url});
             
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Debug.WriteLine("Errors found: "+ errors+"\nEnd Errors found");
                return BadRequest(ModelState);
            }
        }
 

        // GET: Map/Edit/5
        [HttpGet(Name = "EditRoute")]
        public IActionResult EditRoute(int id)
        {
            Route route = _context.GetRouteByID(id);
            if (route == null)
            {
                return NotFound();
            }
            RouteViewModel routeView = _context.GetRouteByIDView(route.RouteId);
            
            return View(routeView);
        }

        // POST: Map/Edit/5
        [HttpPost(Name = "Edit")]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit([FromBody]RouteViewModel route)
        {
            if (ModelState.IsValid)
            {
                _context.UpdateRoute(route);

                _context.SaveRoute();
                return RedirectToAction("SavedRoutes");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet]
        public IActionResult DeleteRoute(int id)
        {
            Route route = _context.GetRouteByID(id);
            if (route == null)
            {
                return NotFound();
            }
            RouteViewModel routeView = _context.GetRouteByIDView(route.RouteId);

            return View(routeView);

        }
        // POST: Map/DeleteRoute/5
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete([FromBody]int id)
        {
            if (ModelState.IsValid)
            {
                
                Route route = _context.GetRouteByID(id);
                _context.DeleteRoute(route);
                _context.SaveRoute();
                return RedirectToAction("SavedRoutes"); 
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Debug.WriteLine("Errors found: " + errors + "\nEnd Errors found");
                return BadRequest(ModelState);
            }
        }
       
    }
}
