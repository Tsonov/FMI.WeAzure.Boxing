namespace FMI.WeAzure.Boxing.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableLogoutDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logins", "LogoutAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logins", "LogoutAt", c => c.DateTime(nullable: false));
        }
    }
}
