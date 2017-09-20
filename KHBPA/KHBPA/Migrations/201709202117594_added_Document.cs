namespace KHBPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_Document : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DocumentName = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        UploadedBy = c.String(),
                        FileBytes = c.Binary(),
                        ContentType = c.String(),
                        Discriminator = c.String(),
                        ContentLength = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Documents");
        }
    }
}
