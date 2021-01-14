namespace EZone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "City", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Order", "State", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "State", c => c.String());
            AlterColumn("dbo.Order", "City", c => c.String());
            AlterColumn("dbo.Order", "Address", c => c.String());
        }
    }
}
