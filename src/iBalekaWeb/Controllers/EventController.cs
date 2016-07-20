using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using iBalekaWeb.Models;
using iBalekaWeb.Data.Configurations;
using iBalekaWeb.Services;
using iBalekaWeb.Models.EventViewModels;

namespace iBalekaWeb.Controllers
{
    public class EventController : Controller
    {
        private IEventService _event;
        public EventController(IEventService _iEvent)
        {
            _event = _iEvent;
        }
        [Authorize]
        public IActionResult Event()
        {
            ViewData["Message"] = "Create an Event";
            return View();
        }
        //
        //GET
        [HttpGet("{id}",Name="GetEvent")]
        public IActionResult GetEvent(int id)
        {
            Event ev = _event.GetEventByID(id);
            if (ev == null)
            {
                return NotFound();
            }

            return View(ev);
        }
        //
        //GET events created
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateEvent(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        //
        //POST
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEvent(Event evnt, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), "Events");
            }
            return View();
        }
        

        //GET
        public ActionResult AllEvents()
        {
            return View();
        }
        //POST
        public ActionResult AllEvents(EventRoute er)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), "Events");
            }
            return View();
                
        }

    }
}