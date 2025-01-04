namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcompanyamountinpaymentinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentInfoes", "CompanyAmount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentInfoes", "CompanyAmount");
        }
    }
}
