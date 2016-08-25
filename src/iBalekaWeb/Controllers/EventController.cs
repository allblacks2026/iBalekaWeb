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
        public async Task<IActionResult> Events()
        {
            ListModelResponse<Event> eventResponse = await _context.GetUserEvents(_userManager.GetUserId(User));
            if (eventResponse.DidError == true)
            {
                Error er = new Error(eventResponse.ErrorMessage);
                return View("Error");
            }
            return View(eventResponse.Model);
        }

        // GET: Event/Details/5
        [HttpGet(Name = "EventDetails")]
        public async Task<IActionResult> EventDetails(int id)
        {
            SingleModelResponse<Event> eventResponse = await _context.GetEvent(id);
            if (eventResponse.DidError == true)
            {
                Error er = new Error(eventResponse.ErrorMessage);
                return View("Error");
            }
            return View(eventResponse.Model);
        }

        //create event
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateEvent()
        {
            ListModelResponse<Route> routeResponse = await _routeContext.GetUserRoutes(_userManager.GetUserId(User));
            if (routeResponse.DidError == true)
            {
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
        public async Task<IActionResult> CreateEvent(EventViewModel model, int[] RouteId)
        {
            if (ModelState.IsValid)
            {
                if (model.EventRoutes == null)
                {
                    model.EventRoutes = new List<EventRouteViewModel>();
                }
                foreach (int id in RouteId)
                {
                    SingleModelResponse<Route> routeResponse = await _routeContext.GetRoute(id);
                    if (routeResponse.DidError == true)
                    {
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
        private async Task<MultiSelectList> GetRoutes(string[] selectedValues)
        {
            ListModelResponse<Route> routeResponse = await _routeContext.GetUserRoutes(_userManager.GetUserId(User));
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
        public async Task<ActionResult> FinalizeEvent([FromBody]EventViewModel currentModel)
        {
            if (ModelState.IsValid)
            {

                SingleModelResponse<Event> eventResponse = await _context.SaveEvent(currentModel);
                if (eventResponse.DidError == true)
                {
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
        public async Task<ActionResult> EditEvent([FromBody]EventViewModel currentModel)
        {
            SingleModelResponse<Event> eventResponse = await _context.UpdateEvent(currentModel);
            if (eventResponse.DidError == true)
            {
                Error er = new Error(eventResponse.ErrorMessage);
                return View("Error");
            }

            string[] selectedValues = new string[eventResponse.Model.EventRoute.Count];
            int count = 0;
            foreach (EventRoute route in eventResponse.Model.EventRoute)
            {
                selectedValues[count] = route.RouteID.ToString();
                count++;
            }
            ViewBag.UserRoutes = GetRoutes(selectedValues);


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
        public async Task<ActionResult> Edit([FromBody]EventViewModel evnt)
        {

            if (ModelState.IsValid)
            {
                SingleModelResponse<Event> eventResponse = await _context.UpdateEvent(evnt);
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
        public async Task<ActionResult> DeleteEvent(int id)
        {
            if (ModelState.IsValid)
            {
                SingleModelResponse<Event> eventResponse = await _context.GetEvent(id);
                if (eventResponse.DidError == true)
                {
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
        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                SingleModelResponse<Event> eventResponse = await _context.DeleteEvent(id);
                if (eventResponse.DidError == true)
                {
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