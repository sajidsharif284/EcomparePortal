namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletecompanyamountinpaymentinfo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PaymentInfoes", "CompanyAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentInfoes", "CompanyAmount", c => c.Int(nullable: false));
        }
    }
}
