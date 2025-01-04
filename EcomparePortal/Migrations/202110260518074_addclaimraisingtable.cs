namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclaimraisingtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClaimRaisingDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Fields = c.String(),
                        CustomerProfileId = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        ClaimDateTime = c.DateTime(nullable: false),
                        IncompleteDateTime = c.DateTime(nullable: false),
                        CompleteDateTime = c.DateTime(nullable: false),
                        ProcessingDateTime = c.DateTime(nullable: false),
                        ApprovedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CustomerProfiles", t => t.CustomerProfileId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CustomerProfileId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClaimRaisingDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ClaimRaisingDetails", "CustomerProfileId", "dbo.CustomerProfiles");
            DropIndex("dbo.ClaimRaisingDetails", new[] { "ProductId" });
            DropIndex("dbo.ClaimRaisingDetails", new[] { "CustomerProfileId" });
            DropTable("dbo.ClaimRaisingDetails");
        }
    }
}
