namespace ProductBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Order3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetailsModels", "OrderModel_ID", "dbo.OrderModels");
            DropIndex("dbo.OrderDetailsModels", new[] { "OrderModel_ID" });
            DropColumn("dbo.OrderDetailsModels", "OrderID");
            RenameColumn(table: "dbo.OrderDetailsModels", name: "OrderModel_ID", newName: "OrderID");
            AlterColumn("dbo.OrderDetailsModels", "OrderID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetailsModels", "OrderID");
            AddForeignKey("dbo.OrderDetailsModels", "OrderID", "dbo.OrderModels", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetailsModels", "OrderID", "dbo.OrderModels");
            DropIndex("dbo.OrderDetailsModels", new[] { "OrderID" });
            AlterColumn("dbo.OrderDetailsModels", "OrderID", c => c.Int());
            RenameColumn(table: "dbo.OrderDetailsModels", name: "OrderID", newName: "OrderModel_ID");
            AddColumn("dbo.OrderDetailsModels", "OrderID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetailsModels", "OrderModel_ID");
            AddForeignKey("dbo.OrderDetailsModels", "OrderModel_ID", "dbo.OrderModels", "ID");
        }
    }
}
