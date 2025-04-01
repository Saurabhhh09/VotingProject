using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShowModels
{
    public class CandidateRegistrationModel
    {
        [Required]
        public int ElectionId { get; set; }
        [Required]
        public int PositionId { get; set; }

        public List<SelectListItem> Elections { get; set; }
        public List<SelectListItem> Positions { get; set; }

    }
}
