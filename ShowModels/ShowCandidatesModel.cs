using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public class ShowCandidatesModel
    {
        public enumElectionStatus StatusId { get; set; }
        public List<ElectionsModel> Elections { get; set; }
    }

    public class ElectionsModel
    {
        public string ElectionName { get; set; }
        public List<ElectionPosition> ElecPositions { get; set; }
    }

    public class ElectionPosition
    {
        public string PositionName { get; set; }
        public List<ElectionCandidates> ElecCandidates { get; set; }
    }

    public class ElectionCandidates
    {
        public string CandidateName { get; set; }

    }
}
