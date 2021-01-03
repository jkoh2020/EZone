namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "CategoryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "CategoryName");
        }
    }
}
