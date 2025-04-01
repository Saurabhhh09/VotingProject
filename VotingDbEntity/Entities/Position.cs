using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingDbEntity.Entities
{
    [Table("Position")]
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Index(IsUnique = true)]
        [StringLength(100)]
        [Required]
        public string PositionName { get; set; }
    }
}
