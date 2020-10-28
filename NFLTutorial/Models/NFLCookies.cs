using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFLTutorial.Models
{
    public class NFLCookies
    {
        private const string TEAMS_KEY = "MyTeams";
        private const string DELIMITER = "-";

        private IRequestCookieCollection RequestCookies { get; set; }
        private IResponseCookies ResponseCookies { get; set; }

        public NFLCookies(IRequestCookieCollection requestCookies)
        {
            this.RequestCookies = requestCookies;
        }

        public NFLCookies(IResponseCookies responseCookies)
        {
            this.ResponseCookies = responseCookies;
        }

        public void SetMyTeamIds(List<Team> myTeams)
        {
            List<string> ids = myTeams.Select(t => t.TeamID).ToList();
            string idString = string.Join(DELIMITER, ids);

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            };

            RemoveMyTeamIds();                                      //remove any existing cookies
            ResponseCookies.Append(TEAMS_KEY, idString, options);   //add new cookies
        }

        public string[] GetMyTeamIds()
        {
            string cookies = RequestCookies[TEAMS_KEY];
            return string.IsNullOrEmpty(cookies) ? new string[] { } : cookies.Split(DELIMITER);
        }

        public void RemoveMyTeamIds()
        {
            ResponseCookies.Delete(TEAMS_KEY);
        }
    }
}
