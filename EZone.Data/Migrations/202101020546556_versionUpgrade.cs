namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versionUpgrade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        OrderQuantity = c.Int(nullable: false),
                        OrderTotal = c.Double(nullable: false),
                        IsShipped = c.Boolean(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DateOfOrder = c.DateTimeOffset(nullable: false, precision: 7),
                        DateOfShipping = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Customer", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropIndex("dbo.Order", new[] { "ProductId" });
            DropColumn("dbo.Customer", "PhoneNumber");
            DropTable("dbo.Order");
        }
    }
}
