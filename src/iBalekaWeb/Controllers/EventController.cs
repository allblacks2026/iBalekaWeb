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
        private IEventRegistration _evntReg;
        private IClubClient _clubContext;
        private IMapClient _routeContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public EventController(IClubClient _club, IEventClient _repo,IEventRegistration _reg, UserManager<ApplicationUser> _user, IMapClient _rContext)
        {
            _context = _repo;
            _routeContext = _rContext;
            _userManager = _user;
            _clubContext = _club;
            _evntReg = _reg;
        }

  
        // GET: Event/Events
        [HttpGet(Name = "Events")]
        public IActionResult Events()
        {
            ListModelResponse<Event> eventResponse = _context.GetUserEvents(_userManager.GetUserId(User));
            if (eventResponse.DidError == true || eventResponse == null)
            {
                if (eventResponse == null)
                    return View("Error");
                Error er = new Error(eventResponse.ErrorMessage);
                return View("Error");
            }
            IEnumerable<Event> events = eventResponse.Model;
            ViewBag.ActiveEvents = GetActiveEvents(events);
            ViewBag.OpenEvents = GetOpenEvents(events);
            ViewBag.ClosedEvents = GetClosedEvents(events);
           
            string sourceCookie = HttpContext.Request.Cookies["SourcePageEvent"];
            if (sourceCookie != null)
            {
                ViewBag.SourcePageEvent = sourceCookie;
            }
            return View(eventResponse.Model);
        }

   

        [HttpGet(Name = "SearchEvent")]
        public IActionResult SearchEvent(string SearchLocation)
        {
            ListModelResponse<Event> eventResponse = _context.GetUserEvents(_userManager.GetUserId(User));

            var events = from e in eventResponse.Model select e;
            if (!String.IsNullOrEmpty(SearchLocation))
            {
                events = events.Where(e => e.Location.Contains(SearchLocation));
            }
            ViewBag.SearchEvents = events;
            return View();


        }
        private IEnumerable<Event> GetClosedEvents(IEnumerable<Event> events)
        {
            return events.Where(p => p.EventStatus == EventType.Closed);
        }
        private IEnumerable<Event> GetOpenEvents(IEnumerable<Event> events)
        {
            return events.Where(p => p.EventStatus == EventType.Open);
        }
        private IEnumerable<Event> GetActiveEvents(IEnumerable<Event> events)
        {
            return events.Where(p => p.EventStatus == EventType.Active);
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
            //get registered athletes
            ListModelResponse<EventRegistration> registredAthletesResponse = _evntReg.GetEventRegistrations(id);
            if (registredAthletesResponse.DidError == true || registredAthletesResponse == null)
            {
                if (registredAthletesResponse == null)
                    return View("Error");
                Error er = new Error(registredAthletesResponse.ErrorMessage);

                return View("Error");
            }
            
            if (registredAthletesResponse.Model != null)
            {
                ViewBag.RegisteredAthletes = registredAthletesResponse.Model;
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
            ListModelResponse<Club> clubResponse = _clubContext.GetUserClubs(_userManager.GetUserId(User));
            if (clubResponse.DidError == true || routeResponse == null)
            {
                if (routeResponse == null)
                    return View("Error");
                Error er = new Error(routeResponse.ErrorMessage);
                return View("Error");
            }
            if (clubResponse.Model.Any())
            {
                ViewBag.UserClubs = GetClubs(null);
            }
            else
            {
                ViewBag.UserClubs = null;
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
                if (model.ClubId != 0)
                {
                    SingleModelResponse<Club> clubResponse = _clubContext.GetClub(model.ClubId);
                    if (clubResponse.DidError == true || clubResponse == null)
                    {
                        if (clubResponse == null)
                            return View("Error");
                        Error er = new Error(clubResponse.ErrorMessage);
                        return View("Error");
                    }
                    model.ClubName = clubResponse.Model.Name;
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
            return new MultiSelectList(routeResponse.Model, "RouteId", "Title", selectedValues);
        }
        private SelectList GetClubs(string selectedValue)
        {
            ListModelResponse<Club> clubResponse = _clubContext.GetUserClubs(_userManager.GetUserId(User));
            return new SelectList(clubResponse.Model, "ClubId", "Name", selectedValue);
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
                string eventCookie = HttpContext.Request.Cookies["NewEvent"];
                if (eventCookie != null)
                {
                    EventViewModel memoryModel = eventCookie.FromJson<EventViewModel>();
                    if (memoryModel.ClubId != 0)
                    {
                        currentModel.ClubId = memoryModel.ClubId;
                        currentModel.UserID = _userManager.GetUserId(User);
                    }
                    else
                        currentModel.UserID = _userManager.GetUserId(User);
                }

                currentModel.DateCreated = DateTime.Now.ToString();
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
                CookieOption = new CookieOptions();
                CookieOption.Expires = DateTime.Now.AddMinutes(1);
                CookieOption.HttpOnly = true;

                string source = "Add";
                //set cookie

                HttpContext.Response.Cookies.Append("SourcePageEvent", source, CookieOption);

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
                return View("Error", er);
            }

            if (eventResponse.Model.EventRoutes.Count > 0)
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
        // POST: Event/Edit/5
        [HttpPost]
        public IActionResult Edit([FromBody]EventViewModel evnt)
        {
            if (ModelState.IsValid)
            {
                evnt.EventRoutes = new List<EventRouteViewModel>();
                foreach (int id in evnt.RouteId)
                {
                    SingleModelResponse<Route> routeResponse = _routeContext.GetRoute(id);
                    if (routeResponse.DidError == true || routeResponse == null)
                    {
                        if (routeResponse == null)
                            return View("Error");
                        Error er = new Error(routeResponse.ErrorMessage);
                        return View("Error");
                    }
                    evnt.EventRoutes.Add(routeResponse.Model.ToEventRouteViewModel());
                }
                SingleModelResponse<Event> eventResponse = _context.UpdateEvent(evnt);
                if (eventResponse.DidError == true)
                {
                    Error er = new Error(eventResponse.ErrorMessage);
                    return View("Error");
                }
                var CookieOption = new CookieOptions();
                CookieOption.Expires = DateTime.Now.AddMinutes(1);
                CookieOption.HttpOnly = true;

                string source = "Edit";
                //set cookie

                HttpContext.Response.Cookies.Append("SourcePageEvent", source, CookieOption);

                return RedirectToAction("EventDetails", new { id = evnt.EventId });

            }
            else
            {
                return View("EditEvent", evnt);
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
                var CookieOption = new CookieOptions();
                CookieOption.Expires = DateTime.Now.AddMinutes(1);
                CookieOption.HttpOnly = true;

                string source = "Delete";
                //set cookie

                HttpContext.Response.Cookies.Append("SourcePageEvent", source, CookieOption);

                return RedirectToAction("Events");
            }
            else
            {
                return BadRequest();
            }

        }
    }
}