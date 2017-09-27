namespace KHBPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedMember : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Members", "DateOfBirth");
            DropColumn("dbo.Members", "Income");
            DropColumn("dbo.Members", "Signature");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Signature", c => c.String());
            AddColumn("dbo.Members", "Income", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Members", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
