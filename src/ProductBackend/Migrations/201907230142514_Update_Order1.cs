namespace ProductBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Order1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetailsModels", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetailsModels", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.OrderModels", "Amount", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderDetailsModels", "Item", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetailsModels", "Item", c => c.Int(nullable: false));
            DropColumn("dbo.OrderModels", "Amount");
            DropColumn("dbo.OrderDetailsModels", "Price");
            DropColumn("dbo.OrderDetailsModels", "Count");
        }
    }
}
