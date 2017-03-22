namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsetweekschedule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SetWeekSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WeekId = c.Int(nullable: false),
                        TimeId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .ForeignKey("dbo.Times", t => t.TimeId, cascadeDelete: true)
                .ForeignKey("dbo.Weeks", t => t.WeekId, cascadeDelete: true)
                .Index(t => t.WeekId)
                .Index(t => t.TimeId)
                .Index(t => t.DayId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SetWeekSchedules", "WeekId", "dbo.Weeks");
            DropForeignKey("dbo.SetWeekSchedules", "TimeId", "dbo.Times");
            DropForeignKey("dbo.SetWeekSchedules", "DayId", "dbo.Days");
            DropIndex("dbo.SetWeekSchedules", new[] { "DayId" });
            DropIndex("dbo.SetWeekSchedules", new[] { "TimeId" });
            DropIndex("dbo.SetWeekSchedules", new[] { "WeekId" });
            DropTable("dbo.SetWeekSchedules");
        }
    }
}
