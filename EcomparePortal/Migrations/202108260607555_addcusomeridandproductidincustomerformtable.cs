namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcusomeridandproductidincustomerformtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerFormDatas", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerFormDatas", "CustomerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CustomerFormDatas", "ProductId");
            CreateIndex("dbo.CustomerFormDatas", "CustomerId");
            AddForeignKey("dbo.CustomerFormDatas", "CustomerId", "dbo.CustomerProfiles", "ApplicationUserId");
            AddForeignKey("dbo.CustomerFormDatas", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerFormDatas", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CustomerFormDatas", "CustomerId", "dbo.CustomerProfiles");
            DropIndex("dbo.CustomerFormDatas", new[] { "CustomerId" });
            DropIndex("dbo.CustomerFormDatas", new[] { "ProductId" });
            DropColumn("dbo.CustomerFormDatas", "CustomerId");
            DropColumn("dbo.CustomerFormDatas", "ProductId");
        }
    }
}
