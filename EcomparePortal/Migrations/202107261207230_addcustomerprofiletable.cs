namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomerprofiletable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerProfiles",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CNIC = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.String(),
                        Address = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        MobileNumber = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ApplicationUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerProfiles", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CustomerProfiles", new[] { "ApplicationUserId" });
            DropTable("dbo.CustomerProfiles");
        }
    }
}
