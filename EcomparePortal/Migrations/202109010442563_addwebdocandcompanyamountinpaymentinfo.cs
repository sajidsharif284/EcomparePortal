namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addwebdocandcompanyamountinpaymentinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentInfoes", "CompanyAmount", c => c.Int(nullable: false));
            AddColumn("dbo.PaymentInfoes", "WebdocAmount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentInfoes", "WebdocAmount");
            DropColumn("dbo.PaymentInfoes", "CompanyAmount");
        }
    }
}
