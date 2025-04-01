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
    public class ViewResultToUsersRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();

        public async Task<ViewResultToUserModel> GetResultToUsersAsync()
        {
            try
            {
                var latestElections = await _db.Elections
                    .Where(e => e.EndDate < DateTime.UtcNow)
                    .OrderByDescending(e => e.EndDate)
                    .FirstOrDefaultAsync();
                var resultToUsers = new ViewResultToUserModel
                {
                    electionName = latestElections.Title,
                    Positions = await GetPositionsToUsersAsync(latestElections.ElectionId)
                };

                return resultToUsers;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<ViewResultToUserPositions>> GetPositionsToUsersAsync(int electionId)
        {
            try
            {
                var positions = await _db.Positions.ToListAsync();
                var positionModel = new List<ViewResultToUserPositions>();
                foreach (var position in positions) 
                {
                    var positionToUser = new ViewResultToUserPositions
                    {
                        PositionName = position.PositionName,
                        Winners = await GetWinnersAsync(position.PositionId, electionId)
                        //CandidateName = await GetWinnersAsync(position.PositionId, electionId)
                    };
                    positionModel.Add(positionToUser);
                }
                return positionModel;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<ViewResultToUserCandidates> GetWinnersAsync(int positionId,int electionId)
        {
            try
            {
                var candidateVotes = await _db.Votes
                            .Where(v => v.PositionId == positionId && v.ElectionId == electionId)
                            .GroupBy(v => v.CandidateId)
                            .Select(g => new
                            {
                                CandidateId = g.Key,
                                TotalVotes = g.Count()
                            })
                            .OrderByDescending(g => g.TotalVotes)
                            .FirstOrDefaultAsync();
                if (candidateVotes != null)
                {
                    var candidate = await _db.Candidates
                        .Where(c => c.CandidateId == candidateVotes.CandidateId)
                        .Select(c => new ViewResultToUserCandidates
                        {
                            CandidateName = c.User.Fname + " " + c.User.Lname,
                            TotalVotes = candidateVotes.TotalVotes
                        })
                        .FirstOrDefaultAsync();

                    return candidate;
                }

                return null;
            }
            catch(Exception)
            {
                return null;    
            }
        }

    }
}
