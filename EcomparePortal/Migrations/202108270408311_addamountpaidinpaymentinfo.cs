namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addamountpaidinpaymentinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentInfoes", "AmountPaid", c => c.Int(nullable: false));
            AddColumn("dbo.PaymentInfoes", "RemainingAmount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentInfoes", "RemainingAmount");
            DropColumn("dbo.PaymentInfoes", "AmountPaid");
        }
    }
}
