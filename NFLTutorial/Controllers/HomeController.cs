﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NFLTutorial.Models;

namespace NFLTutorial.Controllers {
    public class HomeController : Controller {
        private TeamContext context;

        public HomeController(TeamContext context) {
            this.context = context;
        }

        public IActionResult Index(string activeConference = "all", string activeDivision = "all") {
            var data = new TeamListViewModel {
                ActiveConference = activeConference,
                ActiveDivision = activeDivision,
                Conferences = context.Conferences.ToList(),
                Divsisions = context.Divisions.ToList()
            };

            IQueryable<Team> query = context.Teams;

            if (activeConference != "all") {
                query = query.Where(t => t.Conference.ConferenceID.ToLower() == activeConference.ToLower());
            }

            if (activeDivision != "all") {
                query = query.Where(d => d.Division.DivisionID.ToLower() == activeDivision.ToLower());
            }

            data.Teams = query.ToList();

            return View(data);
        }

        [HttpPost]
        public IActionResult Details(TeamViewModel model) {
            TempData["ActiveConference"] = model.ActiveConference;
            TempData["ActiveDivision"] = model.ActiveDivision;

            return RedirectToAction("Details", new { ID = model.Team.TeamID });
        }

        [HttpGet]
        public IActionResult Details(string id) {
            var model = new TeamViewModel {
                Team = context.Teams.Include(c => c.Conference).Include(d => d.Division).FirstOrDefault(t => t.TeamID == id),
                ActiveConference = TempData?["ActiveConference"]?.ToString() ?? "all",
                ActiveDivision = TempData?["ActiveDivision"]?.ToString() ?? "all"
            };

            return View(model);
        }
    }
}
