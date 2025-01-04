namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedatetimeinpaymentinfo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PaymentInfoes", "TransactionDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentInfoes", "TransactionDateTime", c => c.String());
        }
    }
}
