using ShowModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Repository
{
    public class DashboardCountRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();
        public async Task<int> UsersCountAsync()
        {          
            return await _db.Users.CountAsync();
        }
        public async Task<int> CandidateCountAsync()
        {        
            return await _db.Candidates.CountAsync();
        }
        public async Task<int> ElectionCountAsync()
        {
            return await _db.Elections.CountAsync();
        }
    }
}
