namespace VotingDbEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedEmailColumn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidate",
                c => new
                    {
                        CandidateId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                        ElectionId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateId)
                .ForeignKey("dbo.Election", t => t.ElectionId, cascadeDelete: true)
                .ForeignKey("dbo.Position", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PositionId)
                .Index(t => t.ElectionId);
            
            CreateTable(
                "dbo.Election",
                c => new
                    {
                        ElectionId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ElectionId);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        PositionName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PositionId)
                .Index(t => t.PositionName, unique: true);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Fname = c.String(nullable: false, maxLength: 50),
                        Lname = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                        Role = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Result",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        WinnerId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                        ElectionId = c.Int(nullable: false),
                        VoteCount = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.Candidate", t => t.WinnerId, cascadeDelete: false)
                .ForeignKey("dbo.Election", t => t.ElectionId, cascadeDelete: false)
                .ForeignKey("dbo.Position", t => t.PositionId, cascadeDelete: false)
                .Index(t => t.WinnerId)
                .Index(t => t.PositionId)
                .Index(t => t.ElectionId);
            
            CreateTable(
                "dbo.Vote",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        VoterId = c.Int(nullable: false),
                        CandidateId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                        ElectionId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.Candidate", t => t.CandidateId, cascadeDelete: true)
                .ForeignKey("dbo.Election", t => t.ElectionId, cascadeDelete: false)
                .ForeignKey("dbo.Position", t => t.PositionId, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.VoterId, cascadeDelete: false)
                .Index(t => new { t.VoterId, t.CandidateId, t.PositionId }, unique: true, name: "IX_Voter_Candidate")
                .Index(t => t.ElectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vote", "VoterId", "dbo.User");
            DropForeignKey("dbo.Vote", "PositionId", "dbo.Position");
            DropForeignKey("dbo.Vote", "ElectionId", "dbo.Election");
            DropForeignKey("dbo.Vote", "CandidateId", "dbo.Candidate");
            DropForeignKey("dbo.Result", "PositionId", "dbo.Position");
            DropForeignKey("dbo.Result", "ElectionId", "dbo.Election");
            DropForeignKey("dbo.Result", "WinnerId", "dbo.Candidate");
            DropForeignKey("dbo.Candidate", "UserId", "dbo.User");
            DropForeignKey("dbo.Candidate", "PositionId", "dbo.Position");
            DropForeignKey("dbo.Candidate", "ElectionId", "dbo.Election");
            DropIndex("dbo.Vote", new[] { "ElectionId" });
            DropIndex("dbo.Vote", "IX_Voter_Candidate");
            DropIndex("dbo.Result", new[] { "ElectionId" });
            DropIndex("dbo.Result", new[] { "PositionId" });
            DropIndex("dbo.Result", new[] { "WinnerId" });
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.Position", new[] { "PositionName" });
            DropIndex("dbo.Candidate", new[] { "ElectionId" });
            DropIndex("dbo.Candidate", new[] { "PositionId" });
            DropIndex("dbo.Candidate", new[] { "UserId" });
            DropTable("dbo.Vote");
            DropTable("dbo.Result");
            DropTable("dbo.User");
            DropTable("dbo.Position");
            DropTable("dbo.Election");
            DropTable("dbo.Candidate");
        }
    }
}
