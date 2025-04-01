using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShowModels
{
    public class ShowPositionsModel
    {
        public List<PositionModel> Positions { get; set; }
    }
    public class PositionModel
    {
        public int PositionId { get; set; }

        [Required]
        public string PositionName { get; set; }
    }
}
