namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProduct : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Product", "CategoryId");
            AddForeignKey("dbo.Product", "CategoryId", "dbo.Category", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropIndex("dbo.Product", new[] { "CategoryId" });
        }
    }
}
