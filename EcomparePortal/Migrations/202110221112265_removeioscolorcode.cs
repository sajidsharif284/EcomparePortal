namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeioscolorcode : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Companies", "IOSColorCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "IOSColorCode", c => c.String());
        }
    }
}
