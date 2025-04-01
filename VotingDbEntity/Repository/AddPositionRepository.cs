using ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Repository
{
    public class AddPositionRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();

        public async Task<bool> AddPositionAsync(AddPositionModel model)
        {
            try
            {
                var position = new Entities.Position
                {
                    PositionName = model.PositionName
                };
                _db.Positions.Add(position);
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
