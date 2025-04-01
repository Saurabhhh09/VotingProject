using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public class ViewVotesModel
    {
        public string ElectionName { get; set; }
        public List<ViewVotesPositionModel> ViewVotesPositions { get; set; }
    }

    public class ViewVotesPositionModel
    {
        public string PositionName { get; set; }
        public List<object> ViewVoteModel { get; set; }
    }
}
