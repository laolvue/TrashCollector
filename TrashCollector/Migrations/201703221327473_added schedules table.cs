namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedschedulestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        WeekId = c.Int(nullable: false),
                        TimeId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleId)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Times", t => t.TimeId, cascadeDelete: true)
                .ForeignKey("dbo.Weeks", t => t.WeekId, cascadeDelete: true)
                .Index(t => t.WeekId)
                .Index(t => t.TimeId)
                .Index(t => t.DayId)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "WeekId", "dbo.Weeks");
            DropForeignKey("dbo.Schedules", "TimeId", "dbo.Times");
            DropForeignKey("dbo.Schedules", "PersonId", "dbo.People");
            DropForeignKey("dbo.Schedules", "DayId", "dbo.Days");
            DropIndex("dbo.Schedules", new[] { "PersonId" });
            DropIndex("dbo.Schedules", new[] { "DayId" });
            DropIndex("dbo.Schedules", new[] { "TimeId" });
            DropIndex("dbo.Schedules", new[] { "WeekId" });
            DropTable("dbo.Schedules");
        }
    }
}
