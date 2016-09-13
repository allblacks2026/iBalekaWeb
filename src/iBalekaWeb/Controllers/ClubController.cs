using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iBalekaWeb.Data.iBalekaAPI;
using iBalekaWeb.Models;
using Microsoft.AspNetCore.Identity;
using iBalekaWeb.Models.Responses;
using iBalekaWeb.Models.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace iBalekaWeb.Controllers
{
    [Authorize]
    public class ClubController : Controller
    {
        private IClubClient _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ClubController(IClubClient api, UserManager<ApplicationUser> _user)
        {
            _context = api;
            _userManager = _user;
        }
        // GET: Club
        [HttpGet]
        public IActionResult Clubs()
        {
            if (ModelState.IsValid)
            {
                ListModelResponse<Club> clubResponse = _context.GetUserClubs(_userManager.GetUserId(User));
                if (clubResponse.DidError == true || clubResponse == null)
                {
                    if (clubResponse == null)
                        return View("Error");
                    Error er = new Error(clubResponse.ErrorMessage);
                    return View("Error", er);
                }
                return View(clubResponse.Model);
            }
            else
            {
                return BadRequest();
            }
        }
        // GET: Club/Details/5
        [HttpGet]
        public IActionResult ClubDetails(int id)
        {
            if (ModelState.IsValid)
            {
                SingleModelResponse<Club> clubResponse = _context.GetClub(id);
                if (clubResponse.DidError == true || clubResponse == null)
                {
                    if (clubResponse == null)
                        return View("Error");
                    Error er = new Error(clubResponse.ErrorMessage);
                    return View("Error", er);
                }
                return View(clubResponse.Model);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: Club/Create
        [HttpGet]
        public IActionResult EditClub(int id)
        {
            if (ModelState.IsValid)
            {
                SingleModelResponse<Club> clubResponse = _context.GetClub(id);
                if (clubResponse.DidError == true || clubResponse == null)
                {
                    if (clubResponse == null)
                        return View("Error");
                    Error er = new Error(clubResponse.ErrorMessage);
                    return View("Error", er);
                }
                return View(clubResponse.Model);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult Edit([FromBody]Club ClubForm)
        {
            if (ModelState.IsValid)
            {
                Club club = new Club
                {
                    ClubId = ClubForm.ClubId,
                    Description = ClubForm.Description,
                    Name = ClubForm.Name,
                    Location = ClubForm.Location,
                    DateCreated=ClubForm.DateCreated,
                    UserId = _userManager.GetUserId(User)
                };
                SingleModelResponse<Club> clubResponse = _context.UpdateClub(club);
                if (clubResponse.DidError == true || clubResponse == null)
                {
                    if (clubResponse == null)
                        return View("Error");
                    Error er = new Error(clubResponse.ErrorMessage);
                    return View("Error", er);
                }
                return RedirectToAction("ClubDetails", new { id = clubResponse.Model.ClubId });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult DeleteClub(int id)
        {
            if (ModelState.IsValid)
            {
                SingleModelResponse<Club> clubResponse = _context.GetClub(id);
                if (clubResponse.DidError == true || clubResponse == null)
                {
                    if (clubResponse == null)
                        return View("Error");
                    Error er = new Error(clubResponse.ErrorMessage);
                    return View("Error", er);
                }
                return View(clubResponse.Model);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult Delete([FromBody]int ClubId)
        {
            if (ModelState.IsValid)
            {
                SingleModelResponse<Club> clubResponse = _context.DeleteClub(ClubId);
                if (clubResponse.DidError == true || clubResponse == null)
                {
                    if (clubResponse == null)
                        return View("Error");
                    Error er = new Error(clubResponse.ErrorMessage);
                    return View("Error", er);
                }
                return RedirectToAction("Clubs");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult AddClub()
        {
            string clubCookie = HttpContext.Request.Cookies["NewClub"];
            Club club = new Club();
            if (clubCookie != null)
            {
                club = clubCookie.FromJson<Club>();
            }
            return View(club);
        }
        [HttpPost]
        public IActionResult AddClub([FromForm]Club ClubForm)
        {
            if (ModelState.IsValid)
            {
                Club club = new Club
                {
                    Name = ClubForm.Name,
                    Description = ClubForm.Description,
                    DateCreated = DateTime.Now.ToString(),
                    Location = ClubForm.Location,
                    UserId = _userManager.GetUserId(User),
                    Deleted = false
                };
                var CookieOption = new CookieOptions();
                CookieOption.Expires = DateTime.Now.AddMinutes(10);
                CookieOption.HttpOnly = true;

                //set cookie
                HttpContext.Response.Cookies.Append("NewClub", club.ToJson(), CookieOption);

                return RedirectToAction("FinalizeClub");
            }
            else
            {
                return View(ClubForm);
            }
        }

        [HttpGet]
        public IActionResult FinalizeClub()
        {
            string clubCookie = HttpContext.Request.Cookies["NewClub"];
            if (clubCookie != null)
            {
                Club currentModel = clubCookie.FromJson<Club>();
                return View(currentModel);
            }
            else
            {
                return RedirectToAction("AddClub");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateClub()
        {
            if (ModelState.IsValid)
            {
                Club saveClub = new Club();
                string clubCookie = HttpContext.Request.Cookies["NewClub"];
                if (clubCookie != null)
                {
                    saveClub = clubCookie.FromJson<Club>();
                }
                else
                    return RedirectToAction("AddClub");
                SingleModelResponse<Club> clubResponse = await Task.Run(() => _context.SaveClub(saveClub));

                if (clubResponse.DidError == true || clubResponse == null)
                {
                    if (clubResponse == null)
                        return View("Error");
                    Error er = new Error(clubResponse.ErrorMessage);
                    return View("Error");
                }

                var CookieOption = new CookieOptions();
                CookieOption.Expires = DateTime.Now.AddDays(-1);
                CookieOption.HttpOnly = true;

                //set cookie
                HttpContext.Response.Cookies.Append("NewEvent", saveClub.ToJson(), CookieOption);
                return RedirectToAction("Clubs");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}