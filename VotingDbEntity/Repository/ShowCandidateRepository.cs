using ShowModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingDbEntity.Entities;

namespace VotingDbEntity.Repository
{
    public class ShowCandidateRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();

        public async Task<List<ShowModels.ElectionsModel>> showCandidatesToAdminAsync(enumElectionStatus electionStatusId)
        {
            var Elections = new List<Election>();
            switch (electionStatusId)
            {
                case enumElectionStatus.Completed:
                    {
                        Elections = await _db.Elections.Where(e => e.StartDate < DateTime.UtcNow && e.EndDate < DateTime.UtcNow).ToListAsync();
                        break;
                    }
                case enumElectionStatus.Ongoing:
                    {
                        Elections = await _db.Elections.Where(e => e.StartDate < DateTime.UtcNow && e.EndDate > DateTime.UtcNow).ToListAsync();
                        break;
                    }
                case enumElectionStatus.Upcoming:
                    {
                        Elections = await _db.Elections.Where(e => e.StartDate > DateTime.UtcNow && e.EndDate > DateTime.UtcNow).ToListAsync();
                        break;
                    }
            }
            var listCandidates = new List<ShowModels.ElectionsModel>();
            foreach (var election in Elections)
            {
                var CompletedElection = new ShowModels.ElectionsModel
                {
                    ElectionName = election.Title,
                    ElecPositions = await GetPositionsAsync(election.ElectionId)
                };
                listCandidates.Add(CompletedElection);
            }
            return listCandidates;
        }

        public async Task<List<ShowModels.ElectionPosition>> GetPositionsAsync(int electionId)
        {
            var listPositions = new List<ShowModels.ElectionPosition>();
            var Positions = await _db.Positions.ToListAsync();
            foreach (var position in Positions)
            {
                var Position = new ShowModels.ElectionPosition
                {
                    PositionName = position.PositionName,
                    ElecCandidates = await GetCandidatesAsync(electionId, position.PositionId)
                };
                listPositions.Add(Position);
            }
            return listPositions;
        }

        private async Task<List<ElectionCandidates>> GetCandidatesAsync(int electionId, int positionId)
        {
            var listCandidates = new List<ShowModels.ElectionCandidates>();
            var Candidates = await _db.Candidates.Where(c => c.ElectionId == electionId && c.PositionId == positionId)
                .Include(c => c.User).ToListAsync();
            foreach (var candidate in Candidates)
            {
                var Candidate = new ShowModels.ElectionCandidates
                {
                    CandidateName = candidate.User.Fname + " " + candidate.User.Lname
                };
                listCandidates.Add(Candidate);
            }
            return listCandidates;
        }
    }
}
