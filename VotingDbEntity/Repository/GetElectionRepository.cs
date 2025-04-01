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
    public class GetElectionRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();

        public async Task<List<Election>> getUpcomingElectionForRegistrationAsync()
        {
            return await _db.Elections.Where(e => e.StartDate > DateTime.UtcNow).ToListAsync();
        }

        public async Task<List<ElectionModel>> getAllElectionAsync()
        {
            var ListElections = new List<ElectionModel>();
            var Elections = await _db.Elections.ToListAsync();
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

        public async Task<List<ElectionModel>> getCompletedElectionAsync()
        {
            var ListElections = new List<ElectionModel>();
            var Elections = await _db.Elections.Where(e => e.StartDate < DateTime.UtcNow).ToListAsync();
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

    }
}
