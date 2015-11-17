namespace FJS.USER.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Pwd = c.String(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Sex = c.Int(nullable: false),
                        NationID = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nation", t => t.NationID, cascadeDelete: true)
                .Index(t => t.NationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "NationID", "dbo.Nation");
            DropIndex("dbo.Users", new[] { "NationID" });
            DropTable("dbo.Users");
            DropTable("dbo.Nation");
        }
    }
}
