namespace FMI.WeAzure.Boxing.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.Boxers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Biography = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoxingMatches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Time = c.DateTime(nullable: false),
                        Description = c.String(),
                        FirstBoxerId = c.Int(),
                        SecondBoxerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boxers", t => t.FirstBoxerId)
                .ForeignKey("dbo.Boxers", t => t.SecondBoxerId)
                .Index(t => t.FirstBoxerId)
                .Index(t => t.SecondBoxerId);
            
            CreateTable(
                "dbo.Predictions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PredictionResultId = c.Int(nullable: false),
                        MadeById = c.Int(),
                        MadeForId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.MadeById)
                .ForeignKey("dbo.BoxingMatches", t => t.MadeForId)
                .Index(t => t.MadeById)
                .Index(t => t.MadeForId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceIP = c.String(),
                        ExpiresAt = c.DateTime(nullable: false),
                        Token = c.String(),
                        LogoutAt = c.DateTime(nullable: false),
                        ForUserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ForUserId)
                .Index(t => t.ForUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logins", "ForUserId", "dbo.Users");
            DropForeignKey("dbo.BoxingMatches", "SecondBoxerId", "dbo.Boxers");
            DropForeignKey("dbo.Predictions", "MadeForId", "dbo.BoxingMatches");
            DropForeignKey("dbo.Predictions", "MadeById", "dbo.Users");
            DropForeignKey("dbo.BoxingMatches", "FirstBoxerId", "dbo.Boxers");
            DropIndex("dbo.Logins", new[] { "ForUserId" });
            DropIndex("dbo.Predictions", new[] { "MadeForId" });
            DropIndex("dbo.Predictions", new[] { "MadeById" });
            DropIndex("dbo.BoxingMatches", new[] { "SecondBoxerId" });
            DropIndex("dbo.BoxingMatches", new[] { "FirstBoxerId" });
            DropTable("dbo.Logins");
            DropTable("dbo.Users");
            DropTable("dbo.Predictions");
            DropTable("dbo.BoxingMatches");
            DropTable("dbo.Boxers");
            DropTable("dbo.Administrators");
        }
    }
}
