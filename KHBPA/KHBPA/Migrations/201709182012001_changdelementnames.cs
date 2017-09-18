namespace KHBPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changdelementnames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "FirstName", c => c.String());
            AddColumn("dbo.Members", "MembershipEnrollmentDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Members", "FristName");
            DropColumn("dbo.Members", "MembershipEnrollment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "MembershipEnrollment", c => c.DateTime(nullable: false));
            AddColumn("dbo.Members", "FristName", c => c.String());
            DropColumn("dbo.Members", "MembershipEnrollmentDate");
            DropColumn("dbo.Members", "FirstName");
        }
    }
}
