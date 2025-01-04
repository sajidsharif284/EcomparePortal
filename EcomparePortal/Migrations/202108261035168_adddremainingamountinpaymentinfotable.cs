namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddremainingamountinpaymentinfotable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentInfoes", "RemainingAmount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentInfoes", "RemainingAmount");
        }
    }
}
