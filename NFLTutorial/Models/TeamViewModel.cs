using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFLTutorial.Models {
    public class TeamViewModel {
        public Team Team { get; set; }
        public string ActiveConference { get; set; } = "All";
        public string ActiveDivision { get; set; } = "All";
    }
}
