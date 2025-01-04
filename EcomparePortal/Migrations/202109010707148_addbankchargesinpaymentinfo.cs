namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbankchargesinpaymentinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentInfoes", "BankCharges", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentInfoes", "BankCharges");
        }
    }
}
