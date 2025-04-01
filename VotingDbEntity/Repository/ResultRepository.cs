using ShowModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Repository
{
    public class ResultRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();

        public async Task<ResultModel> GetResultsAsync(int electionId)
        {
          
            var election = await _db.Elections.FindAsync(electionId);
           
                var resultElection = new ResultModel
                {
                    ElectionName = election.Title,
                    ResultPositions = await getPositionsAsync(election.ElectionId)
                };
            return resultElection;
        }

        private async Task<List<ResultPositionModel>> getPositionsAsync(int electionId)
        {
            var positions = await _db.Positions.ToListAsync();
            var resultPositions = new List<ResultPositionModel>();
            foreach (var position in positions)
            {
                var resultPosition = new ResultPositionModel
                {
                    PositionName = position.PositionName,
                    DataModels = await getDataModelsAsync(position.PositionId, electionId)
                };
                resultPositions.Add(resultPosition);
            }
            return resultPositions;
        }

        private async Task<List<object>> getDataModelsAsync(int positionId, int electionId)
        {
            var resultCandidates = new List<object>();
            var candidates = await _db.Candidates.Where(v => v.PositionId == positionId && v.ElectionId == electionId)
                .Include(v => v.User).ToListAsync();
            foreach (var candidate in candidates)
            {
                var dataModel = new object[]
                {
                     candidate.User.Fname + " " + candidate.User.Lname,
                     await _db.Votes.CountAsync(v => v.CandidateId == candidate.CandidateId && v.ElectionId == electionId && v.PositionId == positionId)
                };
                resultCandidates.Add(dataModel);
            }
            return resultCandidates;
        }
    }
}
