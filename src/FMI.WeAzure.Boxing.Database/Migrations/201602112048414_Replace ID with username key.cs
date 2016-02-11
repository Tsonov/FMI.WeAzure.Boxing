namespace FMI.WeAzure.Boxing.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplaceIDwithusernamekey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Predictions", "MadeBy_Id", "dbo.Users");
            DropForeignKey("dbo.Logins", "ForUser_Id", "dbo.Users");
            DropIndex("dbo.Predictions", new[] { "MadeBy_Id" });
            DropIndex("dbo.Logins", new[] { "ForUser_Id" });
            RenameColumn(table: "dbo.Predictions", name: "MadeBy_Id", newName: "MadeBy_Username");
            RenameColumn(table: "dbo.Logins", name: "ForUser_Id", newName: "ForUser_Username");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Predictions", "MadeBy_Username", c => c.String(maxLength: 128));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Logins", "ForUser_Username", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Users", "Username");
            CreateIndex("dbo.Predictions", "MadeBy_Username");
            CreateIndex("dbo.Logins", "ForUser_Username");
            AddForeignKey("dbo.Predictions", "MadeBy_Username", "dbo.Users", "Username");
            AddForeignKey("dbo.Logins", "ForUser_Username", "dbo.Users", "Username");
            DropColumn("dbo.Users", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Logins", "ForUser_Username", "dbo.Users");
            DropForeignKey("dbo.Predictions", "MadeBy_Username", "dbo.Users");
            DropIndex("dbo.Logins", new[] { "ForUser_Username" });
            DropIndex("dbo.Predictions", new[] { "MadeBy_Username" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Logins", "ForUser_Username", c => c.Int());
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.Predictions", "MadeBy_Username", c => c.Int());
            AddPrimaryKey("dbo.Users", "Id");
            RenameColumn(table: "dbo.Logins", name: "ForUser_Username", newName: "ForUser_Id");
            RenameColumn(table: "dbo.Predictions", name: "MadeBy_Username", newName: "MadeBy_Id");
            CreateIndex("dbo.Logins", "ForUser_Id");
            CreateIndex("dbo.Predictions", "MadeBy_Id");
            AddForeignKey("dbo.Logins", "ForUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Predictions", "MadeBy_Id", "dbo.Users", "Id");
        }
    }
}
