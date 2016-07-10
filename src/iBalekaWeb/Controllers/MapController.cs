using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using prototypeWeb.Models;

namespace iBalekaWeb.Controllers
{
    public class MapController : Controller
    {

        public MapController()
        {
    
        }

        public IActionResult Map()
        {
            ViewData["Message"] = "Map A Route";
            return View();
        }
        //// GET: Map/Details/5
        //public IActionResult Details(int? id)
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

        //// GET: Map/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Map/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(route route)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.route.Add(route);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(route);
        //}

        //// GET: Map/Edit/5
        //public IActionResult Edit(int? id)
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

        //// POST: Map/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(route route)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(route);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
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
