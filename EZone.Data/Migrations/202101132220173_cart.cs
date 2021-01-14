namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        OrderDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(),
                        OrderQuantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        OrderTotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Order", "Total", c => c.Double(nullable: false));
            AddColumn("dbo.Order", "OrderDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Order", "UserName", c => c.String());
            AlterColumn("dbo.Order", "FirstName", c => c.String(maxLength: 50));
            DropColumn("dbo.Order", "DateOfOrder");
            DropColumn("dbo.Order", "OrderTotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "OrderTotal", c => c.Double(nullable: false));
            AddColumn("dbo.Order", "DateOfOrder", c => c.DateTimeOffset(nullable: false, precision: 7));
            DropForeignKey("dbo.OrderDetail", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "OrderId", "dbo.Order");
            DropIndex("dbo.OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.OrderDetail", new[] { "OrderId" });
            AlterColumn("dbo.Order", "FirstName", c => c.String());
            AlterColumn("dbo.Order", "UserName", c => c.String(maxLength: 50));
            DropColumn("dbo.Order", "OrderDate");
            DropColumn("dbo.Order", "Total");
            DropTable("dbo.OrderDetail");
        }
    }
}
