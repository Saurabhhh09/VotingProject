using ShowModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Repository
{
    public class ReportsRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();

        public async Task<ReportsModel> Reports(int electionId)
        {
            var election = await _db.Elections.FindAsync(electionId);
            if (election == null)
            {
                return null;
            }
            var reportElection = new ReportsModel
            {
                ElectionName = election.Title,
                ReportPositions = await getPositionsAsync(electionId)
            };
            return reportElection;
        }

        private async Task<List<ReportPositionModel>> getPositionsAsync(int electionId)
        {
            var positions = await _db.Positions.ToListAsync();
            var reportPositions = new List<ReportPositionModel>();
            foreach (var position in positions)
            {
                var rp = new ReportPositionModel
                {
                    PositionName = position.PositionName,
                    Candidates = await getCandidatesAsync(position.PositionId, electionId)
                };
                reportPositions.Add(rp);
            }
            return reportPositions;
        }

        private async Task<List<ReportsCandidateModel>> getCandidatesAsync(int positionId, int electionId)
        {
            var candidates = await _db.Candidates.Where(c => c.PositionId == positionId && c.ElectionId == electionId)
                  .Include(c => c.User).ToListAsync();
            var reportCandidates = new List<ReportsCandidateModel>();
            foreach (var candidate in candidates)
            {
                var rc = new ReportsCandidateModel
                {
                    CandidateName = candidate.User.Fname + " " + candidate.User.Lname,
                    Voters = await getVotersAsync(positionId,electionId,candidate.CandidateId),
                    TotalVotes = await _db.Votes.CountAsync(v => v.CandidateId == candidate.CandidateId && v.ElectionId == electionId && v.PositionId == positionId)
                };
                reportCandidates.Add(rc);
            }
            return reportCandidates;
        }

        private async Task<List<ReportsVoterModel>> getVotersAsync(int positionId, int electionId,int candidateId)
        {
           var voters = await _db.Votes.Where(v => v.CandidateId == candidateId && v.ElectionId == electionId && v.PositionId == positionId)
                .Include(v => v.User).ToListAsync();
            var reportVoters = new List<ReportsVoterModel>(); 
            foreach (var voter in voters)
            {
                var rv = new ReportsVoterModel
                {
                    VoterName = voter.User.Fname + " " + voter.User.Lname,
                    Date = voter.Time.ConvertUtcToLocalTime()
                };
                reportVoters.Add(rv);
            }
            return reportVoters;
        }
    }
}
