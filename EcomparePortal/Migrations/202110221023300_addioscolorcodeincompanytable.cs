namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addioscolorcodeincompanytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "IOSColorCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "IOSColorCode");
        }
    }
}
