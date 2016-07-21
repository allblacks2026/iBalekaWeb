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
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
namespace iBalekaWeb.Controllers
{
    public class EventController : Controller
    {
        private IEventService _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public EventController(IEventService _repo, UserManager<ApplicationUser> _user)
        {
            _context = _repo;
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

        // GET: Event/Create
        [HttpGet]
        public ActionResult CreateEvent()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        public IActionResult AddDetails()
        {

            if (ModelState.IsValid)
            {
                //return Json(newEvent);

                return RedirectToAction("AddRoutes");
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpGet(Name = "AddRoutes")]
        public ActionResult AddRoutes()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        public ActionResult SaveRoutes()
        {
            if (ModelState.IsValid)
            {
                //return Json(newEvent);
                return RedirectToAction("SaveRoutes");

            }
            else
            {
                return BadRequest();
            }
        }

        
        public ActionResult FinalizeEvent()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEvent([FromBody] EventViewModel newEvent)
        {
            if (ModelState.IsValid)
            {

                string userId = _userManager.GetUserId(User);
                newEvent.UserID = userId;
                _context.AddEvent(newEvent);
                _context.SaveEvent();
                Event savedEvent = _context.GetEvents(userId).Single(x => x.UserID == newEvent.UserID && x.Title == newEvent.Title && x.DateCreated == DateTime.Now.Date && x.Deleted == false);
                //return Json(newEvent);
                return RedirectToAction("EventDetails", new { id = savedEvent.EventId });

            }
            else
            {
                return BadRequest();
            }
        }


        // GET: Event/Edit/5
        public ActionResult EditEvent(int id)
        {
            EventViewModel evnt = _context.GetEventByIDView(id);
            if (evnt == null)
            {
                return NotFound();
            }
            return View(evnt);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventViewModel evnt)
        {

            if (ModelState.IsValid)
            {
                _context.UpdateEvent(evnt);
                _context.SaveEvent();
                return RedirectToAction("EditEvent", evnt.EventId);
            }
            else
            {
                return BadRequest();
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
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EventViewModel evnt)
        {
            if (ModelState.IsValid)
            {
                Event deleteEvent = _context.GetEventByID(evnt.EventId);
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