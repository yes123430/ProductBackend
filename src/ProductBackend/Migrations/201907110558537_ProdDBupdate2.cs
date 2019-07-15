namespace ProductBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProdDBupdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "ProdName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "ProdName", c => c.String());
        }
    }
}
