namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomersaledatatable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerSaleDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fields = c.String(),
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerProfiles", t => t.CustomerId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerSaleDatas", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CustomerSaleDatas", "CustomerId", "dbo.CustomerProfiles");
            DropIndex("dbo.CustomerSaleDatas", new[] { "CustomerId" });
            DropIndex("dbo.CustomerSaleDatas", new[] { "ProductId" });
            DropTable("dbo.CustomerSaleDatas");
        }
    }
}
