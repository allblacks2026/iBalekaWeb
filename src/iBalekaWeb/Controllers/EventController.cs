using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iBalekaWeb.Models;
using iBalekaWeb.Data.iBalekaAPI;
using iBalekaWeb.Models.EventViewModels;
using Microsoft.AspNetCore.Identity;
using iBalekaWeb.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using iBalekaWeb.Models.Responses;
using iBalekaWeb.Data.Extensions;

namespace iBalekaWeb.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private IEventClient _context;
        private IMapClient _routeContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public EventController(IEventClient _repo, UserManager<ApplicationUser> _user, IMapClient _rContext)
        {
            _context = _repo;
            _routeContext = _rContext;
            _userManager = _user;
        }

        // GET: Event/Events
        [HttpGet(Name = "Events")]
        public IActionResult Events()
        {
            ListModelResponse<Event> eventResponse = _context.GetUserEvents(_userManager.GetUserId(User));
            if (eventResponse.DidError == true || eventResponse == null)
            {
                if(eventResponse == null)
                    return View("Error");
                Error er = new Error(eventResponse.ErrorMessage);
                return View("Error");
            }
            return View(eventResponse.Model);
        }

        // GET: Event/Details/5
        [HttpGet(Name = "EventDetails")]
        public IActionResult EventDetails(int id)
        {
            SingleModelResponse<EventViewModel> eventResponse = _context.GetEvent(id);
            if (eventResponse.DidError == true || eventResponse == null)
            {
                if (eventResponse == null)
                    return View("Error");
                Error er = new Error(eventResponse.ErrorMessage);
                return View("Error");
            }
            return View(eventResponse.Model);
        }

        //create event
        [Authorize]
        [HttpGet]
        public IActionResult CreateEvent()
        {
            ListModelResponse<Route> routeResponse = _routeContext.GetUserRoutes(_userManager.GetUserId(User));
            if (routeResponse.DidError == true || routeResponse == null)
            {
                if (routeResponse == null)
                    return View("Error");
                Error er = new Error(routeResponse.ErrorMessage);
                return View("Error");
            }
            if (routeResponse.Model.Any())
            {
                ViewBag.UserRoutes = GetRoutes(null);
            }
            else
            {
                ViewBag.UserRoutes = null;
            }
            string eventCookie = HttpContext.Request.Cookies["NewEvent"];
            EventViewModel model = new EventViewModel();
            if (eventCookie != null)
            {
                model = eventCookie.FromJson<EventViewModel>();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEvent(EventViewModel model, int[] RouteId)
        {
            if (ModelState.IsValid)
            {
                if (model.EventRoutes == null)
                {
                    model.EventRoutes = new List<EventRouteViewModel>();
                }
                foreach (int id in RouteId)
                {
                    SingleModelResponse<Route> routeResponse = _routeContext.GetRoute(id);
                    if (routeResponse.DidError == true || routeResponse == null)
                    {
                        if (routeResponse == null)
                            return View("Error");
                        Error er = new Error(routeResponse.ErrorMessage);
                        return View("Error");
                    }
                    model.EventRoutes.Add(routeResponse.Model.ToEventRouteViewModel());
                }

                //example of using cookie
                var CookieOption = new CookieOptions();
                CookieOption.Expires = DateTime.Now.AddMinutes(5);
                CookieOption.HttpOnly = true;

                //set cookie
                HttpContext.Response.Cookies.Append("NewEvent", model.ToJson(), CookieOption);

                return RedirectToAction("FinalizeEvent");
            }
            else
            {
                return View(model);
            }


        }
        private MultiSelectList GetRoutes(string[] selectedValues)
        {
            ListModelResponse<Route> routeResponse = _routeContext.GetUserRoutes(_userManager.GetUserId(User));

            //Get suitable error message
            //if (routeResponse.DidError == true)
            //{
            //    Error er = new Error(routeResponse.ErrorMessage);
            //    return View("Error");
            //}
            return new MultiSelectList(routeResponse.Model, "RouteId", "Title", selectedValues);
        }


        //save event
        [HttpGet]
        public ActionResult FinalizeEvent()
        {
            string eventCookie = HttpContext.Request.Cookies["NewEvent"];
            if (eventCookie != null)
            {
                EventViewModel currentModel = eventCookie.FromJson<EventViewModel>();
                return View(currentModel);
            }
            else
            {
                return RedirectToAction("CreateEvent");
            }


        }
        [HttpPost]
        public ActionResult FinalizeEvent([FromBody]EventViewModel currentModel)
        {
            if (ModelState.IsValid)
            {

                SingleModelResponse<Event> eventResponse = _context.SaveEvent(currentModel);
                if (eventResponse.DidError == true || eventResponse == null)
                {
                    if (eventResponse == null)
                        return View("Error");
                    Error er = new Error(eventResponse.ErrorMessage);
                    return View("Error");
                }



                var CookieOption = new CookieOptions();
                CookieOption.Expires = DateTime.Now.AddDays(-1);
                CookieOption.HttpOnly = true;

                //set cookie
                HttpContext.Response.Cookies.Append("NewEvent", currentModel.ToJson(), CookieOption);
                return RedirectToAction("Events");
            }
            else
            {
                return BadRequest();
            }
        }


        // GET: Event/Edit/5
        [HttpGet]
        public ActionResult EditEvent(int id)
        {
            SingleModelResponse<EventViewModel> eventResponse = _context.GetEvent(id);
            if (eventResponse.DidError == true || eventResponse == null)
            {
                if (eventResponse == null)
                    return View("Error");
                Error er = new Error(eventResponse.ErrorMessage);
                return View("Error");
            }

            if (eventResponse.Model.EventRoutes.Count>0)
            {
                string[] selectedValues = new string[eventResponse.Model.EventRoutes.Count];
                int count = 0;
                foreach (EventRouteViewModel route in eventResponse.Model.EventRoutes)
                {
                    selectedValues[count] = route.RouteId.ToString();
                    count++;
                }
                ViewBag.UserRoutes = GetRoutes(selectedValues);
            }
            else
            {
                ViewBag.UserRoutes = GetRoutes(null);
            }
            


            return View(eventResponse.Model);
        }
        [HttpPost]
        public ActionResult UpdatedEditEvent([FromBody]int id)
        {

            return RedirectToAction("EventDetails", new { Id = id });


        }
        // POST: Event/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([FromBody]EventViewModel evnt)
        {

            if (ModelState.IsValid)
            {
                SingleModelResponse<Event> eventResponse = _context.UpdateEvent(evnt);
                if (eventResponse.DidError == true)
                {
                    Error er = new Error(eventResponse.ErrorMessage);
                    return View("Error");
                }

                return RedirectToAction("EventDetails", new { id = evnt.EventId });

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // GET: Event/Delete/5
        [HttpGet]
        public ActionResult DeleteEvent(int id)
        {
            if (ModelState.IsValid)
            {
                SingleModelResponse<EventViewModel> eventResponse = _context.GetEvent(id);
                if (eventResponse.DidError == true || eventResponse == null)
                {
                    if (eventResponse == null)
                        return View("Error");
                    Error er = new Error(eventResponse.ErrorMessage);
                    return View("Error");
                }

                return View(eventResponse.Model);

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST: Event/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete([FromBody]int id)
        {
            if (ModelState.IsValid)
            {
                SingleModelResponse<Event> eventResponse = _context.DeleteEvent(id);
                if (eventResponse.DidError == true || eventResponse == null)
                {
                    if (eventResponse == null)
                        return View("Error");
                    Error er = new Error(eventResponse.ErrorMessage);
                    return View("Error");
                }
                return RedirectToAction("Events");
            }
            else
            {
                return BadRequest();
            }

        }
    }
}