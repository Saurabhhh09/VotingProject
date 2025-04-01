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
    public class PositionRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();

        public async Task<List<Position>> getAllPositionsAsync()
        {
            return await _db.Positions.ToListAsync();
        }

        //public async Task<List<PositionDropDownModel>> getAllPositionsForDropDownAsync()
        //{
        //    var ListPosition = new List<PositionDropDownModel>();
        //    var Positions = await _db.Positions.ToListAsync();
        //    foreach (var position in Positions)
        //    {
        //        var positionModel = new PositionDropDownModel
        //        {
        //            PositionId = position.PositionId,
        //            PositionName = position.PositionName,
        //        };
        //        ListPosition.Add(positionModel);
        //    }
        //    return ListPosition;
        //}
    }
}
