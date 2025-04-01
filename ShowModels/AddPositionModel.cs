using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public  class AddPositionModel
    {
        [Required]
        public string PositionName { get; set; }

    }
}
