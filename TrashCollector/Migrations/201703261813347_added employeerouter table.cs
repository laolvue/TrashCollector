namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedemployeeroutertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeRouters",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        ZipId = c.Int(nullable: false),
                        WeekId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RouteId)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .ForeignKey("dbo.Weeks", t => t.WeekId, cascadeDelete: true)
                .ForeignKey("dbo.Zips", t => t.ZipId, cascadeDelete: true)
                .Index(t => t.ZipId)
                .Index(t => t.WeekId)
                .Index(t => t.DayId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeRouters", "ZipId", "dbo.Zips");
            DropForeignKey("dbo.EmployeeRouters", "WeekId", "dbo.Weeks");
            DropForeignKey("dbo.EmployeeRouters", "DayId", "dbo.Days");
            DropIndex("dbo.EmployeeRouters", new[] { "DayId" });
            DropIndex("dbo.EmployeeRouters", new[] { "WeekId" });
            DropIndex("dbo.EmployeeRouters", new[] { "ZipId" });
            DropTable("dbo.EmployeeRouters");
        }
    }
}
