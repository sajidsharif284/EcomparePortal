namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecompanynamefromproducttable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "CompanyName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CompanyName", c => c.String());
        }
    }
}
