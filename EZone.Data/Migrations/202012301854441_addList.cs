namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropIndex("dbo.Product", new[] { "CategoryId" });
            AddColumn("dbo.Category", "Product_ProductId", c => c.Int());
            CreateIndex("dbo.Category", "Product_ProductId");
            AddForeignKey("dbo.Category", "Product_ProductId", "dbo.Product", "ProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "Product_ProductId", "dbo.Product");
            DropIndex("dbo.Category", new[] { "Product_ProductId" });
            DropColumn("dbo.Category", "Product_ProductId");
            CreateIndex("dbo.Product", "CategoryId");
            AddForeignKey("dbo.Product", "CategoryId", "dbo.Category", "CategoryId", cascadeDelete: true);
        }
    }
}
