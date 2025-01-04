namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproductbenefitsandfeatures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductBenefits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fields = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Feature = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductFeatures", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductBenefits", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductFeatures", new[] { "ProductId" });
            DropIndex("dbo.ProductBenefits", new[] { "ProductId" });
            DropTable("dbo.ProductFeatures");
            DropTable("dbo.ProductBenefits");
        }
    }
}
