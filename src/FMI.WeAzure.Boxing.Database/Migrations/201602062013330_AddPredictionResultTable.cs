namespace FMI.WeAzure.Boxing.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPredictionResultTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PredictionResultEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PredictionResultEntities");
        }
    }
}
