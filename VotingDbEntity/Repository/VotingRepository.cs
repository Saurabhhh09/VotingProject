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
    public class VotingRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();
        public async Task<List<ElectionModel>> getOngoingElectionAsync()
        {
            var ListElections = new List<ElectionModel>();
            var Elections = await _db.Elections.Where(e => e.StartDate <= DateTime.UtcNow && e.EndDate >= DateTime.UtcNow).ToListAsync();
            foreach (var Election in Elections)
            {
                var ElectionModel = new ElectionModel
                {
                    Id = Election.ElectionId,
                    ElectionName = Election.Title,
                    startDate = Election.StartDate.ConvertUtcToLocalTime(),
                    endDate = Election.EndDate.ConvertUtcToLocalTime(),
                };
                ListElections.Add(ElectionModel);
            }
            return ListElections;
        }

        public async Task<VotingPageModel> GetCandidatesForOngoingElectionAsync(int electionId)
        {
            var votingModel = new VotingPageModel();
            var election = await _db.Elections.FirstOrDefaultAsync(e => e.ElectionId == electionId);
            votingModel.ElectionId = election.ElectionId;
            votingModel.ElectionName = election.Title;
            votingModel.Positions = new List<VotingPositionModel>();
            var Positions = await _db.Positions.ToListAsync();
            foreach (var Position in Positions)
            {
                var PositionModel = new VotingPositionModel
                {
                    PositionId = Position.PositionId,
                    PositionName = Position.PositionName,
                    Candidates = await GetCandidatesAsync(electionId, Position.PositionId)
                };
                votingModel.Positions.Add(PositionModel);
            }
           return votingModel;

        }

        public async Task<List<CandidateModel>> GetCandidatesAsync(int  electionId, int positionId)
        {
            var ListCandidates = new List<CandidateModel>();
            var Candidates = await _db.Candidates
                .Where(c => c.ElectionId == electionId && c.PositionId == positionId)
                .Include(c => c.User).ToListAsync();
            foreach (var Candidate in Candidates)
            {
                var CandidateModel = new CandidateModel
                {
                    CandidateId = Candidate.CandidateId,
                    CandidateName = Candidate.User.Fname + " " + Candidate.User.Lname,
                };
                ListCandidates.Add(CandidateModel);
            }
            return ListCandidates;
        }
        public async Task<string> RegisterVoteAsync(VotingPageModel model, int userId)
        {
            try
            {
                var userAlready = await _db.Votes.FirstOrDefaultAsync(v => v.VoterId == userId && v.ElectionId == model.ElectionId);
                if (userAlready != null)
                {
                    return "You already Voted for this Election !! ";
                }
                var listVote = new List<Vote>();
                foreach(var data in model.Positions)
                {
                    var votes = new Vote
                    {
                        VoterId = userId,
                        CandidateId = data.SelectedCandidateId,
                        PositionId = data.PositionId,
                        ElectionId = model.ElectionId,
                        Time = DateTime.UtcNow
                    };
                    listVote.Add(votes);
                }
                _db.Votes.AddRange(listVote);
                await _db.SaveChangesAsync();
                return null;
            }
            catch (Exception)
            {
                return "Voting Failed!!";
            }
        }
    }
}
