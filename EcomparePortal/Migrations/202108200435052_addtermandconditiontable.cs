namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtermandconditiontable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductTermAndConditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TermAndCondition = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTermAndConditions", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductTermAndConditions", new[] { "ProductId" });
            DropTable("dbo.ProductTermAndConditions");
        }
    }
}
