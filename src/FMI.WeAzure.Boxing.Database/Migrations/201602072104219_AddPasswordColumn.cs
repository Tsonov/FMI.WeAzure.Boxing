namespace FMI.WeAzure.Boxing.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Password");
        }
    }
}
