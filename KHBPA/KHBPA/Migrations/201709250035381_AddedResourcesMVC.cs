namespace KHBPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedResourcesMVC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceType = c.Int(nullable: false),
                        ResourceName = c.String(),
                        ResourceDesc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Resources");
        }
    }
}
