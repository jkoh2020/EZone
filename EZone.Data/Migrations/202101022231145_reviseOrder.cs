namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviseOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropColumn("dbo.Order", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "CustomerId");
            AddForeignKey("dbo.Order", "CustomerId", "dbo.Customer", "CustomerId", cascadeDelete: true);
        }
    }
}
