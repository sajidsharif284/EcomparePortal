namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addregisterfromincustomertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerProfiles", "RegisterFrom", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerProfiles", "RegisterFrom");
        }
    }
}
