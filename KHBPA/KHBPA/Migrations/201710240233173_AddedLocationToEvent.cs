namespace KHBPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLocationToEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events1", "lat", c => c.Single(nullable: false));
            AddColumn("dbo.Events1", "lng", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events1", "lng");
            DropColumn("dbo.Events1", "lat");
        }
    }
}
