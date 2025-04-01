using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Entities
{
    [Table("Candidate")]
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId  { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public Position Position { get; set; }

        [Required]
        [ForeignKey("Election")]
        public int ElectionId { get; set; }
        public Election Election { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
