namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsatrtandexpirydatepaymentinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentInfoes", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaymentInfoes", "ExpiryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentInfoes", "ExpiryDate");
            DropColumn("dbo.PaymentInfoes", "StartDate");
        }
    }
}
