namespace KHBPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedSpelling : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "FirstName", c => c.String());
            DropColumn("dbo.Members", "FristName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "FristName", c => c.String());
            DropColumn("dbo.Members", "FirstName");
        }
    }
}
