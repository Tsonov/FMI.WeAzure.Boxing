namespace FMI.WeAzure.Boxing.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPredictionWinnerColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Predictions", "PredictedWinner_Id", c => c.Int());
            CreateIndex("dbo.Predictions", "PredictedWinner_Id");
            AddForeignKey("dbo.Predictions", "PredictedWinner_Id", "dbo.Boxers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Predictions", "PredictedWinner_Id", "dbo.Boxers");
            DropIndex("dbo.Predictions", new[] { "PredictedWinner_Id" });
            DropColumn("dbo.Predictions", "PredictedWinner_Id");
        }
    }
}
