namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclaimsdynamicinputs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClaimsDynamicInputs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubCategoryId = c.Int(nullable: false),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.SubCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClaimsDynamicInputs", "SubCategoryId", "dbo.SubCategories");
            DropIndex("dbo.ClaimsDynamicInputs", new[] { "SubCategoryId" });
            DropTable("dbo.ClaimsDynamicInputs");
        }
    }
}
