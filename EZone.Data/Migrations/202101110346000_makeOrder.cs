namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "Order_OrderId", "dbo.Order");
            DropIndex("dbo.Product", new[] { "Order_OrderId" });
            DropColumn("dbo.Order", "ProductId");
            DropColumn("dbo.Order", "OrderQuantity");
            DropColumn("dbo.Product", "Order_OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Order_OrderId", c => c.Int());
            AddColumn("dbo.Order", "OrderQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "Order_OrderId");
            AddForeignKey("dbo.Product", "Order_OrderId", "dbo.Order", "OrderId");
        }
    }
}
