namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomerdynamicinputdetailstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerDynamicInputDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Status = c.String(),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerDynamicInputDetails");
        }
    }
}
