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
                        FirstBoxer_Id = c.Int(),
                        SecondBoxer_Id = c.Int(),
                        Winner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boxers", t => t.FirstBoxer_Id)
                .ForeignKey("dbo.Boxers", t => t.SecondBoxer_Id)
                .ForeignKey("dbo.Boxers", t => t.Winner_Id)
                .Index(t => t.FirstBoxer_Id)
                .Index(t => t.SecondBoxer_Id)
                .Index(t => t.Winner_Id);
            
            CreateTable(
                "dbo.Predictions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MadeBy_Id = c.Int(),
                        MadeFor_Id = c.Int(),
                        PredictionResult_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.MadeBy_Id)
                .ForeignKey("dbo.BoxingMatches", t => t.MadeFor_Id)
                .ForeignKey("dbo.PredictionResults", t => t.PredictionResult_Id)
                .Index(t => t.MadeBy_Id)
                .Index(t => t.MadeFor_Id)
                .Index(t => t.PredictionResult_Id);
            
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
                "dbo.PredictionResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssuedAt = c.DateTime(nullable: false),
                        ExpiresAt = c.DateTime(nullable: false),
                        Token = c.String(),
                        LogoutAt = c.DateTime(nullable: false),
                        ForUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ForUser_Id)
                .Index(t => t.ForUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logins", "ForUser_Id", "dbo.Users");
            DropForeignKey("dbo.BoxingMatches", "Winner_Id", "dbo.Boxers");
            DropForeignKey("dbo.BoxingMatches", "SecondBoxer_Id", "dbo.Boxers");
            DropForeignKey("dbo.Predictions", "PredictionResult_Id", "dbo.PredictionResults");
            DropForeignKey("dbo.Predictions", "MadeFor_Id", "dbo.BoxingMatches");
            DropForeignKey("dbo.Predictions", "MadeBy_Id", "dbo.Users");
            DropForeignKey("dbo.BoxingMatches", "FirstBoxer_Id", "dbo.Boxers");
            DropIndex("dbo.Logins", new[] { "ForUser_Id" });
            DropIndex("dbo.Predictions", new[] { "PredictionResult_Id" });
            DropIndex("dbo.Predictions", new[] { "MadeFor_Id" });
            DropIndex("dbo.Predictions", new[] { "MadeBy_Id" });
            DropIndex("dbo.BoxingMatches", new[] { "Winner_Id" });
            DropIndex("dbo.BoxingMatches", new[] { "SecondBoxer_Id" });
            DropIndex("dbo.BoxingMatches", new[] { "FirstBoxer_Id" });
            DropTable("dbo.Logins");
            DropTable("dbo.PredictionResults");
            DropTable("dbo.Users");
            DropTable("dbo.Predictions");
            DropTable("dbo.BoxingMatches");
            DropTable("dbo.Boxers");
            DropTable("dbo.Administrators");
        }
    }
}
