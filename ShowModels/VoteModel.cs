using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ShowModels
{
    public class VoteModel
    {
        public int ElectionId { get; set; }
        public List<VoteCandidateModel> Candidate { get; set; }
    }
    public class VoteCandidateModel
    {
        public int PositionId { get; set; }
        public int CandidateId { get; set; }
    }
}
