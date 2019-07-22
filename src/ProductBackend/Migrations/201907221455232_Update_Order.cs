namespace ProductBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetailsModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        Item = c.Int(nullable: false),
                        OrderModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OrderModels", t => t.OrderModel_ID)
                .Index(t => t.OrderModel_ID);
            
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        OrderMail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetailsModels", "OrderModel_ID", "dbo.OrderModels");
            DropIndex("dbo.OrderDetailsModels", new[] { "OrderModel_ID" });
            DropTable("dbo.OrderModels");
            DropTable("dbo.OrderDetailsModels");
        }
    }
}
