namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedusertable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "PersonId", "dbo.People");
            DropIndex("dbo.Users", new[] { "PersonId" });
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateIndex("dbo.Users", "PersonId");
            AddForeignKey("dbo.Users", "PersonId", "dbo.People", "PersonId", cascadeDelete: true);
        }
    }
}
