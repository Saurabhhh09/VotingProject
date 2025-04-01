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
    public class CandidateRegisterRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();
        public async Task<string> RegisterCandidateAsync(CandidateRegistrationModel model, int userId)
        {
            try
            {

                var userAlready = await _db.Candidates.FirstOrDefaultAsync(e => e.UserId == userId && e.ElectionId == model.ElectionId);
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (userAlready != null)
                {
                    return "User already applied for this election !! ";
                }
                else if (user.Role.ToString() == "Admin")
                {
                    return "Admin can't Registered for election,Only Students can!! ";
                }
                var candidate = new Candidate
                {
                    ElectionId = model.ElectionId,
                    PositionId = model.PositionId,
                    UserId = userId,
                    Date = DateTime.UtcNow
                };
                _db.Candidates.Add(candidate);
                await _db.SaveChangesAsync();
                return null;
            }
            catch (Exception)
            {
                return "Registration Failed!!";
            }
        }
    }
}
