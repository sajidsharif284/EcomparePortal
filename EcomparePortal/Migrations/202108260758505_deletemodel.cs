namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerFormDatas", "CustomerId", "dbo.CustomerProfiles");
            DropForeignKey("dbo.CustomerFormDatas", "ProductId", "dbo.Products");
            DropIndex("dbo.CustomerFormDatas", new[] { "ProductId" });
            DropIndex("dbo.CustomerFormDatas", new[] { "CustomerId" });
            DropTable("dbo.CustomerFormDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomerFormDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fields = c.String(),
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.CustomerFormDatas", "CustomerId");
            CreateIndex("dbo.CustomerFormDatas", "ProductId");
            AddForeignKey("dbo.CustomerFormDatas", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerFormDatas", "CustomerId", "dbo.CustomerProfiles", "ApplicationUserId");
        }
    }
}
