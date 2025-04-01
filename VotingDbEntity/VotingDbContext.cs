using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using VotingDbEntity.Entities;

namespace VotingDbEntity
{
    public class VotingDbContext : DbContext
    {
        public VotingDbContext() : base("VotingDb")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Result> Results { get; set; }

    }
}
