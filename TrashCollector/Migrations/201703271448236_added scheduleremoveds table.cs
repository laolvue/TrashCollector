namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedscheduleremovedstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleRemoveds",
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
            DropForeignKey("dbo.ScheduleRemoveds", "WeekId", "dbo.Weeks");
            DropForeignKey("dbo.ScheduleRemoveds", "TimeId", "dbo.Times");
            DropForeignKey("dbo.ScheduleRemoveds", "PersonId", "dbo.People");
            DropForeignKey("dbo.ScheduleRemoveds", "DayId", "dbo.Days");
            DropIndex("dbo.ScheduleRemoveds", new[] { "PersonId" });
            DropIndex("dbo.ScheduleRemoveds", new[] { "DayId" });
            DropIndex("dbo.ScheduleRemoveds", new[] { "TimeId" });
            DropIndex("dbo.ScheduleRemoveds", new[] { "WeekId" });
            DropTable("dbo.ScheduleRemoveds");
        }
    }
}
