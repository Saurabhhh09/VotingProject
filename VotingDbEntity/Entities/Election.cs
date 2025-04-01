using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Entities
{
    [Table("Election")]
    public class Election
    {
        [Key]
        public int ElectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        //[Required]
        //public StatusEnum Status { get; set; }

        //public Election()
        //{            
        //    Status = StatusEnum.Upcoming;
        //}
    }
}
