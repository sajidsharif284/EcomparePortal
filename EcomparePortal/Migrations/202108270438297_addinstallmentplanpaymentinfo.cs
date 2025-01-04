namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinstallmentplanpaymentinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentInfoes", "InstallmentPlan", c => c.String());
            DropColumn("dbo.PaymentInfoes", "RemainingAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentInfoes", "RemainingAmount", c => c.Int(nullable: false));
            DropColumn("dbo.PaymentInfoes", "InstallmentPlan");
        }
    }
}
