namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtitleincustomerinputtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerDynamicInputDetails", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerDynamicInputDetails", "Title");
        }
    }
}
