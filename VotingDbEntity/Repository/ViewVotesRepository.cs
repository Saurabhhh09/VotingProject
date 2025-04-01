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
    public class ViewVotesRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();

        public async Task<ViewVotesModel> getVotesCurrentElection()
        {
            var election = await _db.Elections.Where(e => e.StartDate <= DateTime.UtcNow && e.EndDate >= DateTime.UtcNow).FirstOrDefaultAsync();
            if (election == null)
            {
                return null;
            }
            var viewVotesModel = new ViewVotesModel
            {
                ElectionName = election.Title,
                ViewVotesPositions = await GetPositionsCurrentElectionAsync(election.ElectionId)
            };
            return viewVotesModel;
        }

        public async Task<List<ViewVotesPositionModel>> GetPositionsCurrentElectionAsync(int electionId)
        {
            var positions = await _db.Positions.ToListAsync();
            var viewVotesPositionModels = new List<ViewVotesPositionModel>();
            foreach (var position in positions)
            {
                var viewVotesPositionModel = new ViewVotesPositionModel
                {
                    PositionName = position.PositionName,
                    ViewVoteModel = await GetVotesCurrentElectionAsync(electionId, position.PositionId)
                };
                viewVotesPositionModels.Add(viewVotesPositionModel);
            }
            return viewVotesPositionModels;
        }

        public async Task<List<object>> GetVotesCurrentElectionAsync(int electionId, int positionId)
        {
            double givenVotes = await _db.Votes.Where(v => v.ElectionId == electionId && v.PositionId == positionId).CountAsync();
            double totalVotes = await _db.Users.CountAsync();
            if (totalVotes == 0)
            {
                return null;
            }

            // Calculate percentages
            var votingPercent = (givenVotes / totalVotes) * 100;
            var remainingPercent = 100 - votingPercent;
            var remaining = totalVotes - givenVotes;
            

            var listVoteModel = new List<object>();
            var ViewVoteModel1 = new object[]
            {
                     "Caste Votes",
                     givenVotes,
            };
            listVoteModel.Add(ViewVoteModel1);
            var ViewVoteModel2 = new object[]
            {
                     "Remaining Votes",
                     remaining,
            };
            listVoteModel.Add(ViewVoteModel2);


            return listVoteModel;
        }
    }
}
