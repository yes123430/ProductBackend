namespace ProductBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProdDBupdate : DbMigration
    {
        public override void Up()
        {
            
            AddColumn("dbo.Product", "Count", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Count");
        }
    }
}
