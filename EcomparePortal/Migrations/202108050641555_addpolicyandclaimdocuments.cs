namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpolicyandclaimdocuments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClaimDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimDocumentURL = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.PolicyDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PolicyDocumentURL = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolicyDocuments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ClaimDocuments", "ProductId", "dbo.Products");
            DropIndex("dbo.PolicyDocuments", new[] { "ProductId" });
            DropIndex("dbo.ClaimDocuments", new[] { "ProductId" });
            DropTable("dbo.PolicyDocuments");
            DropTable("dbo.ClaimDocuments");
        }
    }
}
