namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeyindynamicinputdeatilsintable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerDynamicInputDetails", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.CustomerDynamicInputDetails", "ProductId");
            AddForeignKey("dbo.CustomerDynamicInputDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerDynamicInputDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.CustomerDynamicInputDetails", new[] { "ProductId" });
            DropColumn("dbo.CustomerDynamicInputDetails", "ProductId");
        }
    }
}
