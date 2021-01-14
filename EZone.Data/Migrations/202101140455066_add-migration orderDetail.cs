namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationorderDetail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderDetail", "OrderDate");
            DropColumn("dbo.OrderDetail", "ProductName");
            DropColumn("dbo.OrderDetail", "OrderTotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetail", "OrderTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetail", "ProductName", c => c.String());
            AddColumn("dbo.OrderDetail", "OrderDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
