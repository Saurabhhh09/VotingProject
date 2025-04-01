using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Entities
{
    [Table("Result")]
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        [Required]
        [ForeignKey("Candidate")]
        public int WinnerId { get; set; }
        public Candidate Candidate { get; set; }

        [Required]
        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public Position Position { get; set; }

        [Required]
        [ForeignKey("Election")]
        public int ElectionId { get; set; }
        public Election Election { get; set; }

        [Required]
        public int VoteCount { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }
}
