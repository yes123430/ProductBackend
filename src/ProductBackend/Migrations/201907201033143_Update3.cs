namespace ProductBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProdDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ProdDescription");
        }
    }
}
