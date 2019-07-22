namespace ProductBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProdName = c.String(nullable: false),
                        ImagePath = c.String(),
                        Price = c.Double(nullable: false),
                        Count = c.Double(nullable: false),
                        BuildDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);   
        }
        
        public override void Down()
        {
            DropTable("dbo.Product");
        }
    }
}
