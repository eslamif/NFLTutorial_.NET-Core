using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFLTutorial.Models;

namespace NFLTutorial.Controllers
{
    public class FavoritesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var session = new NFLSession(HttpContext.Session);

            var model = new TeamListViewModel
            {
                ActiveConference = session.GetActiveConference(),
                ActiveDivision = session.GetActiveDivision(),
                Teams = session.GetMyTeams()
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Delete()
        {
            var session = new NFLSession(HttpContext.Session);
            var cookies = new NFLCookies(Response.Cookies);

            session.RemoveMyTeams();
            cookies.RemoveMyTeamIds();

            TempData["Message"] = $"Favorite teams deleted.";

            return RedirectToAction("Index", "Home", new { ActiveConferend = session.GetActiveConference(), ActiveDivision = session.GetActiveDivision() });
        }
    }
}
