using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public class HomePageModel
    {
        public List<UpcomingElectionModel> UpcomingElections { get; set; }
    }

    public class UpcomingElectionModel
    {
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string RegistrationDate { get; set; } 
        public List<UpcomingElectionCandidateModel> Candidates { get; set; }
    }

    public class UpcomingElectionCandidateModel
    {
        public string Position { get; set; }
        public string Candidate { get; set; }
    }
}
