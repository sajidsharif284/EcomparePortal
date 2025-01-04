namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "CardImageURL", c => c.String());
            AddColumn("dbo.Companies", "TagImageURL", c => c.String());
            AddColumn("dbo.Companies", "ColorCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "ColorCode");
            DropColumn("dbo.Companies", "TagImageURL");
            DropColumn("dbo.Companies", "CardImageURL");
        }
    }
}
