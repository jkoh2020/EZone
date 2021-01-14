namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUnit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderDetail", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderDetail", "OrderTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Order", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "Total", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderDetail", "OrderTotal", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderDetail", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Product", "Price", c => c.Double(nullable: false));
        }
    }
}
