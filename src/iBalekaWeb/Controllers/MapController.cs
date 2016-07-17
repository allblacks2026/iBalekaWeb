using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBalekaWeb.Models;
using iBalekaWeb.Services;
using System.Collections.Generic;
using iBalekaWeb.Models.MapViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;

//using prototypeWeb.Models;

namespace iBalekaWeb.Controllers
{
    public class MapController : Controller
    {
        private IRouteService _context;
        public MapController(IRouteService _repo)
        {
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
            IEnumerable<Route> routes = _context.GetRoutes();
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
        public IActionResult Edit(int id)
        {
            Route route = _context.GetRouteByID(id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
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
            return RedirectToAction("SavedRoutes");
        }

        ////// GET: Map/GetRoute/5
        //[HttpGet("{id}", Name = "GetRoute")]
        //public IActionResult GetRoute(int id)
        //{
        //    Route route = _context.GetRouteByID(id);
        //    if (route == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(route);
        //}



        //// GET: Map/Delete/5
        //[ActionName("Delete")]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    route route = _context.route.Single(m => m.RouteID == id);
        //    if (route == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(route);
        //}

        //// POST: Map/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    route route = _context.route.Single(m => m.RouteID == id);
        //    _context.route.Remove(route);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
