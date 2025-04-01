using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public class ViewResultToUserModel
    {
        public string electionName { get; set; }
        public List<ViewResultToUserPositions> Positions { get; set; }

    }
    public class ViewResultToUserPositions
    {
        public string PositionName { get; set; }
        public ViewResultToUserCandidates Winners { get; set; }

    }
    public class ViewResultToUserCandidates
    {
        public string CandidateName { get; set; }
        public int TotalVotes { get; set; }
    }
}
