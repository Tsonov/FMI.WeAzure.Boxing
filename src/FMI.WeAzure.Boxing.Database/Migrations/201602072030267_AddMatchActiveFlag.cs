namespace FMI.WeAzure.Boxing.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatchActiveFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoxingMatches", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BoxingMatches", "Active");
        }
    }
}
