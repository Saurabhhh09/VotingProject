using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Entities
{
    [Table("Vote")]
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }

        [Required]
        [ForeignKey("User")]
        [Index("IX_Voter_Candidate", 1, IsUnique = true)]
        public int VoterId { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Candidate")]
        [Index("IX_Voter_Candidate", 2, IsUnique = true)]
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        [Required]
        [ForeignKey("Position")]
        [Index("IX_Voter_Candidate", 3, IsUnique = true)]
        public int PositionId { get; set; }
        public Position Position { get; set; }

        [Required]
        [ForeignKey("Election")]
        public int ElectionId { get; set; }
        public Election Election { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }
}
