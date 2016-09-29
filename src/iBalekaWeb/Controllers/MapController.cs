using System.Linq;
using Microsoft.AspNetCore.Mvc;
using iBalekaWeb.Models;
using iBalekaWeb.Services;
using System.Collections.Generic;
using iBalekaWeb.Models.MapViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using iBalekaWeb.Data.iBalekaAPI;
using iBalekaWeb.Models.Responses;
using System.Threading.Tasks;
using iBalekaWeb.Data.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using iBalekaWeb.Models.Extensions;

//using prototypeWeb.Models;

namespace iBalekaWeb.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        private IMapClient _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MapController(IMapClient _repo, UserManager<ApplicationUser> userManger)
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
        [HttpGet(Name = "SavedRoutes")]
        public IActionResult SavedRoutes()
        {
            ListModelResponse<Route> routeResponse = _context.GetUserRoutes(_userManager.GetUserId(User));
            if (routeResponse.DidError == true || routeResponse == null)
            {
                if (routeResponse == null)
                    return View("Error");
                Error er = new Error(routeResponse.ErrorMessage);
                return View("Error");
            }
            if(routeResponse.Model!=null)
            {
                ViewBag.DistanceHighest = routeResponse.Model.OrderByDescending(p => p.Distance);
                ViewBag.DistanceLowest = routeResponse.Model.OrderBy(p => p.Distance);
            }
            string sourceCookie = HttpContext.Request.Cookies["SourcePageMap"];
            if (sourceCookie != null)
            {
                ViewBag.SourcePageMap = sourceCookie;
            }
            return View(routeResponse.Model);
        }


        [HttpGet(Name = "SearchRoute")]
        public IActionResult SearchRoute(string SearchTitle)
        {
            ListModelResponse<Route> routeResponse = _context.GetUserRoutes(_userManager.GetUserId(User));

            var route = from r in routeResponse.Model select r;
            if (!String.IsNullOrEmpty(SearchTitle))
            {
                route = route.Where(r => r.Title.Contains(SearchTitle));
            }
            ViewBag.SearchRoute = route;
            return View();


        }
        //// POST: Map/AddRoute
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoute([FromBody]RouteViewModel route)
        {
            if (ModelState.IsValid)
            {
                string userID = _userManager.GetUserId(User);
                SingleModelResponse<Route> routeResponse = await Task.Run(()=> _context.SaveRoute(route,userID));
                if (routeResponse.DidError == true || routeResponse == null)
                {
                    if (routeResponse == null)
                        return View("Error");
                    Error er = new Error(routeResponse.ErrorMessage);
                    return View("Error",er);
                }
                var CookieOption = new CookieOptions();
                CookieOption.Expires = DateTime.Now.AddMinutes(1);
                CookieOption.HttpOnly = true;

                string source = "Add";
                //set cookie
                HttpContext.Response.Cookies.Append("SourcePageMap", source, CookieOption);
                string url = Url.Action("SavedRoutes", "Map");
                return Json(new { Url = url });

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Debug.WriteLine("Errors found: " + errors + "\nEnd Errors found");
                return BadRequest(ModelState);
            }
        }

        
        // GET: Map/Edit/5
        [HttpGet(Name = "EditRoute")]
        public IActionResult EditRoute(int id)
        {
            SingleModelResponse<Route> routeResponse = _context.GetRoute(id);
            if (routeResponse.DidError == true || routeResponse == null)
            {
                if (routeResponse == null)
                    return View("Error");
                Error er = new Error(routeResponse.ErrorMessage);
                return View("Error");
            }
            return View(routeResponse.Model.ToRouteViewModel());
        }

        // POST: Map/Edit/5
        [HttpPost(Name = "Edit")]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit([FromBody]RouteViewModel route)
        {
            if (ModelState.IsValid)
            {
                route.UserID = _userManager.GetUserId(User);
                SingleModelResponse<Route> routeResponse = _context.UpdateRoute(route);
                if (routeResponse.DidError == true || routeResponse == null)
                {
                    if (routeResponse == null)
                        return View("Error");
                    Error er = new Error(routeResponse.ErrorMessage);
                    return View("Error");
                }
                var CookieOption = new CookieOptions();
                CookieOption.Expires = DateTime.Now.AddMinutes(1);
                CookieOption.HttpOnly = true;

                string source = "Edit";
                //set cookie
                HttpContext.Response.Cookies.Append("SourcePageMap", source, CookieOption);
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
            SingleModelResponse<Route> routeResponse = _context.GetRoute(id);
            if (routeResponse.DidError == true || routeResponse == null)
            {
                if (routeResponse == null)
                    return View("Error");
                Error er = new Error(routeResponse.ErrorMessage);
                return View("Error");
            }
            return View(routeResponse.Model.ToRouteViewModel());

        }
        // POST: Map/DeleteRoute/5
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete([FromBody]int id)
        {
            if (ModelState.IsValid)
            {
                SingleModelResponse<Route> routeResponse = _context.DeleteRoute(id);
                if (routeResponse.DidError == true || routeResponse == null)
                {
                    if (routeResponse == null)
                        return View("Error");
                    Error er = new Error(routeResponse.ErrorMessage);
                    return View("Error");
                }
                var CookieOption = new CookieOptions();
                CookieOption.Expires = DateTime.Now.AddMinutes(1);
                CookieOption.HttpOnly = true;

                string source = "Delete";
                //set cookie
                HttpContext.Response.Cookies.Append("SourcePageMap", source, CookieOption);

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
