using ShowModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Repository
{
    public class ElectionRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();

        public async Task<List<UpcomingElectionModel>> GetUpComingElectionsAsync()
        {
            var currentDate = DateTime.UtcNow;
            var upcomingElections = new List<UpcomingElectionModel>();

            var elections = await _db.Elections.Where(e => e.StartDate > currentDate).ToListAsync();

            foreach (var election in elections)
            {
                var upcomingElection = new UpcomingElectionModel
                {
                    Title = election.Title,
                    StartDate = election.StartDate.ConvertUtcToLocalTime(),
                    EndDate = election.EndDate.ConvertUtcToLocalTime(),
                    RegistrationDate = election.StartDate.AddDays(-20).ConvertUtcToLocalTime(),
                    Candidates = new List<UpcomingElectionCandidateModel>()
                };
                
                var candidates = await _db.Candidates
                    .Include(c => c.User) // Ensure User is included
                    .Include(c => c.Position) // Ensure Position is included
                    .Where(c => c.ElectionId == election.ElectionId)
                    .ToListAsync();
                foreach (var candidate in candidates)
                {
                    var candidateModel = new UpcomingElectionCandidateModel
                    {
                        Candidate = candidate.User.Fname + " " + candidate.User.Lname,
                        Position = candidate.Position.PositionName
                    };

                    upcomingElection.Candidates.Add(candidateModel);
                }
                upcomingElections.Add(upcomingElection);
            }
            return upcomingElections;
        }
    }
}
