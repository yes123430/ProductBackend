namespace ProductBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Order2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderModels", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderModels", "Description");
        }
    }
}
