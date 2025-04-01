using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public class VotingPageModel
    {
        public int ElectionId { get; set; }
        public string ElectionName { get; set; }
        public List<VotingPositionModel> Positions { get; set; }
       
    }

    public class VotingPositionModel
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }

        public int SelectedCandidateId { get; set; }
        public List<CandidateModel> Candidates { get; set; }

    }
    public class CandidateModel
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
    }
}
