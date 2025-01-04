namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcompanycartables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarCompanies",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarCompanyProducts",
                c => new
                    {
                        CarModelId = c.Int(nullable: false, identity: true),
                        CarCompanyId = c.String(maxLength: 128),
                        ModelName = c.String(),
                    })
                .PrimaryKey(t => t.CarModelId)
                .ForeignKey("dbo.CarCompanies", t => t.CarCompanyId)
                .Index(t => t.CarCompanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarCompanyProducts", "CarCompanyId", "dbo.CarCompanies");
            DropIndex("dbo.CarCompanyProducts", new[] { "CarCompanyId" });
            DropTable("dbo.CarCompanyProducts");
            DropTable("dbo.CarCompanies");
        }
    }
}
