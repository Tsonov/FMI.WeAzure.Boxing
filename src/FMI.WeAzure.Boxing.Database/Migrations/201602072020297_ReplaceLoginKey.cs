namespace FMI.WeAzure.Boxing.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplaceLoginKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Logins");
            AlterColumn("dbo.Logins", "Token", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Logins", "Token");
            DropColumn("dbo.Logins", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logins", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Logins");
            AlterColumn("dbo.Logins", "Token", c => c.String());
            AddPrimaryKey("dbo.Logins", "Id");
        }
    }
}
