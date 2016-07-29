using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iBalekaWeb.Models;
using iBalekaWeb.Services;
using iBalekaWeb.Models.EventViewModels;
using Microsoft.AspNetCore.Identity;
using iBalekaWeb.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iBalekaWeb.Controllers
{
    public class EventController : Controller
    {
        private IEventService _context;
        private IRouteService _routeContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public EventController(IEventService _repo, UserManager<ApplicationUser> _user, IRouteService _rContext)
        {
            _context = _repo;
            _routeContext = _rContext;
            _userManager = _user;
        }

        // GET: Event/Events
        [HttpGet(Name = "Events")]
        public IActionResult Events()
        {
            IEnumerable<Event> events = _context.GetEvents(_userManager.GetUserId(User));
            return View(events);
        }

        // GET: Event/Details/5
        [HttpGet(Name = "EventDetails")]
        public ActionResult EventDetails(int id)
        {
            Event evnt = _context.GetEventByID(id);
            if (evnt == null)
            {
                return NotFound();
            }
            EventViewModel evntView = _context.GetEventByIDView(id);
            return View(evntView);
        }

        //create event
        [HttpGet]
        public IActionResult CreateEvent()
        {

            if (_routeContext.GetRoutes(_userManager.GetUserId(User)).Any())
            {
                ViewBag.UserRoutes = GetRoutes(null);
            }
            else
            {
                ViewBag.UserRoutes = new SelectListItem { Text = "No Routes Created", Value = "0" };
            }

            EventViewModel model = new EventViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEventReload([FromBody]EventViewModel eventModel)
        {

            if (_routeContext.GetRoutes(_userManager.GetUserId(User)).Any())
            {
                string[] selectedValues = new string[eventModel.EventRoutes.Count];
                for (int i = 0; i > eventModel.EventRoutes.Count; i++)
                {
                    selectedValues[i] = eventModel.EventRoutes[i].RouteId.ToString();
                }
                ViewBag.UserRoutes = GetRoutes(selectedValues);
            }
            else
            {
                ViewBag.UserRoutes = new SelectListItem { Text = "No Routes Created", Value = "0" };
            }

            return View("CreateEvent", eventModel);
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

                    model.EventRoutes.Add(new EventRouteViewModel(_routeContext.GetRouteByID(id)));
                }

                TempData["currentModel"] = model.ToJson();

                TempData.Keep();
                return RedirectToAction("FinalizeEvent");
            }
            else
            {
                return View(model);
            }


        }
        private MultiSelectList GetRoutes(string[] selectedValues)
        {
            IEnumerable<Route> routes = _routeContext.GetRoutes(_userManager.GetUserId(User)).ToList();
            return new MultiSelectList(routes, "RouteId", "Title", selectedValues);
        }


        //save event
        [HttpGet]
        public ActionResult FinalizeEvent()
        {

            if (TempData["currentModel"] != null)
            {
                string currentModelJson = TempData.Peek("currentModel") as string;
                EventViewModel currentModel = new EventViewModel(currentModelJson.ToString().FromJson<EventViewModel>());
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
            if (ModelState.IsValid)            {
                
                    string userId = _userManager.GetUserId(User);
                    currentModel.UserID = userId;
                    _context.AddEvent(currentModel);                   
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
            EventViewModel evnt = _context.GetEventByIDView(id);
            if (evnt == null)
            {
                return NotFound();
            }
            if (_routeContext.GetRoutes(_userManager.GetUserId(User)).Any())
            {
                string[] selectedValues = new string[evnt.EventRoutes.Count];
                for (int i = 0; i < evnt.EventRoutes.Count; i++)
                {
                    selectedValues[i] = evnt.EventRoutes[i].RouteId.ToString();
                }
                ViewBag.UserRoutes = GetRoutes(selectedValues);
            }
            else
            {
                ViewBag.UserRoutes = new SelectListItem { Text = "No Routes Created", Value = "0" };
            }

            
            return View(evnt);
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
                evnt.EventRoutes = new List<EventRouteViewModel>();

                foreach (int id in evnt.RouteId)
                {
                    evnt.EventRoutes.Add(new EventRouteViewModel(_routeContext.GetRouteByID(id)));
                }
                _context.UpdateEvent(evnt);
                _context.SaveEvent();
                //return Json(new { id = evnt.EventId });
                return RedirectToAction("EventDetails", new { id = evnt.EventId });
                //return Ok();
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
            EventViewModel evnt = _context.GetEventByIDView(id);
            if (evnt == null)
            {
                return NotFound();
            }
            return View(evnt);
        }

        // POST: Event/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete([FromBody]int id)
        {
            if (ModelState.IsValid)
            {
                Event deleteEvent = _context.GetEventByID(id);
                _context.Delete(deleteEvent);
                _context.SaveEvent();
                return RedirectToAction("Events");
            }
            else
            {
                return BadRequest();
            }

        }
    }
}