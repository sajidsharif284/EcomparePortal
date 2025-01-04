namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproductmaindetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductMainDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fields = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductMainDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductMainDetails", new[] { "ProductId" });
            DropTable("dbo.ProductMainDetails");
        }
    }
}
