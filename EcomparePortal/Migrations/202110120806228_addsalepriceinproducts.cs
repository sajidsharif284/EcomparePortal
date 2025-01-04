namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsalepriceinproducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SalePrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "SalePrice");
        }
    }
}
