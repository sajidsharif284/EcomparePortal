namespace EcomparePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomerformandpaymentinfotable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerFormDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fields = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AmountPaid = c.String(),
                        Bank = c.String(),
                        Accountnumber = c.String(),
                        Mobilenumber = c.String(),
                        TransactionType = c.String(),
                        TransactionReferenceNumber = c.String(),
                        TransactionDateTime = c.String(),
                        Status = c.String(),
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerProfiles", t => t.CustomerId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentInfoes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PaymentInfoes", "CustomerId", "dbo.CustomerProfiles");
            DropIndex("dbo.PaymentInfoes", new[] { "CustomerId" });
            DropIndex("dbo.PaymentInfoes", new[] { "ProductId" });
            DropTable("dbo.PaymentInfoes");
            DropTable("dbo.CustomerFormDatas");
        }
    }
}
