namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsetschedulestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SetSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .ForeignKey("dbo.Times", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.DayId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SetSchedules", "PersonId", "dbo.Times");
            DropForeignKey("dbo.SetSchedules", "DayId", "dbo.Days");
            DropIndex("dbo.SetSchedules", new[] { "DayId" });
            DropIndex("dbo.SetSchedules", new[] { "PersonId" });
            DropTable("dbo.SetSchedules");
        }
    }
}
