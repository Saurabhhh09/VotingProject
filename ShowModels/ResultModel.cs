using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ShowModels
{
    public class ResultModel
    {
        public string ElectionName { get; set; }
        public List<ResultPositionModel> ResultPositions { get; set; }
    }
    public class ResultPositionModel
    {
        public string PositionName { get; set; }
        public List<object> DataModels { get; set; }
    }
}
