using ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Repository
{
    public  class AddElectionRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();
        public async Task<bool> AddElectionAsync(AddElectionModel model)
        {
            try
            {
                var election = new Entities.Election
                {
                    Title = model.Title,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };
                _db.Elections.Add(election);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }         
        }
    }
}
