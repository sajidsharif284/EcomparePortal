namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteamountpaidinpaymentinfo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PaymentInfoes", "AmountPaid");
            DropColumn("dbo.PaymentInfoes", "RemainingAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentInfoes", "RemainingAmount", c => c.String());
            AddColumn("dbo.PaymentInfoes", "AmountPaid", c => c.String());
        }
    }
}
