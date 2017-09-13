namespace KHBPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPropertyName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "MembershipEnrollmentDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Members", "MembershipEnrollment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "MembershipEnrollment", c => c.DateTime(nullable: false));
            DropColumn("dbo.Members", "MembershipEnrollmentDate");
        }
    }
}
