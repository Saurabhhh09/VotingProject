using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public  class ReportsModel
    {
        public string ElectionName { get; set; }
        public List<ReportPositionModel> ReportPositions { get; set; }
    }
    public class ReportPositionModel
    {
        public string PositionName { get; set; }
        public List<ReportsCandidateModel> Candidates { get; set; }
    }

    public class ReportsCandidateModel
    {
        public string CandidateName { get; set; }
        public List<ReportsVoterModel> Voters { get; set; }
        public int TotalVotes { get; set; }
    }

    public class ReportsVoterModel
    {
        public string VoterName { get; set; }
        public string Date { get; set; }
    }
}
