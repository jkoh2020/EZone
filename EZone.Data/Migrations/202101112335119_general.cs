namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class general : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "UserName", c => c.String(maxLength: 50));
            AddColumn("dbo.Order", "Email", c => c.String());
            AlterColumn("dbo.Order", "FirstName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "FirstName", c => c.String(maxLength: 50));
            DropColumn("dbo.Order", "Email");
            DropColumn("dbo.Order", "UserName");
        }
    }
}
