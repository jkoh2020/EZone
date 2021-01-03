namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviseOrderagain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropIndex("dbo.Order", new[] { "ProductId" });
            AddColumn("dbo.Order", "FirstName", c => c.String(maxLength: 50));
            AddColumn("dbo.Order", "LastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Order", "PhoneNumber", c => c.String());
            AddColumn("dbo.Order", "Address", c => c.String());
            AddColumn("dbo.Order", "City", c => c.String());
            AddColumn("dbo.Order", "State", c => c.String());
            AddColumn("dbo.Order", "ZipCode", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.Product", "Order_OrderId");
            AddForeignKey("dbo.Product", "Order_OrderId", "dbo.Order", "OrderId");
            DropColumn("dbo.Order", "IsShipped");
            DropColumn("dbo.Order", "DateOfShipping");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "DateOfShipping", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.Order", "IsShipped", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Product", "Order_OrderId", "dbo.Order");
            DropIndex("dbo.Product", new[] { "Order_OrderId" });
            DropColumn("dbo.Product", "Order_OrderId");
            DropColumn("dbo.Order", "ZipCode");
            DropColumn("dbo.Order", "State");
            DropColumn("dbo.Order", "City");
            DropColumn("dbo.Order", "Address");
            DropColumn("dbo.Order", "PhoneNumber");
            DropColumn("dbo.Order", "LastName");
            DropColumn("dbo.Order", "FirstName");
            CreateIndex("dbo.Order", "ProductId");
            AddForeignKey("dbo.Order", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
        }
    }
}
