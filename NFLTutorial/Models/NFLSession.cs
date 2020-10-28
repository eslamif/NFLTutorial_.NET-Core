using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NFLTutorial.Models
{
    public class NFLSession
    {
        private const string TEAM_KEY = "TeamKey";
        private const string TEAM_COUNT = "TeamCount";
        private const string CONFERENCE_KEY = "ConferenceKey";
        private const string DIVISION_KEY = "DivisionKey";

        private ISession Session { get; set; }

        public NFLSession(ISession session)
        {
            this.Session = session;
        }

        #region Team
        public void SetMyTeams(List<Team> teams)
        {
            Session.SetObject(TEAM_KEY, teams);
            Session.SetInt32(TEAM_COUNT, teams.Count);
        }

        public List<Team> GetMyTeams() => Session.GetObject<List<Team>>(TEAM_KEY) ?? new List<Team>();

        public int? GetMyTeamCount() => Session.GetInt32(TEAM_COUNT);

        public void RemoveMyTeams()
        {
            Session.Remove(TEAM_KEY);
            Session.Remove(TEAM_COUNT);
        }
        #endregion

        #region Conference
        public void SetActiveConference(string conference) => Session.SetString(CONFERENCE_KEY, conference);

        public string GetActiveConference() => Session.GetString(CONFERENCE_KEY);
        #endregion

        #region Divison
        public void SetActiveDivision(string division) => Session.SetString(DIVISION_KEY, division);

        public string GetActiveDivision() => Session.GetString(DIVISION_KEY);
        #endregion
    }
}
