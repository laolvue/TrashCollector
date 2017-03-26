namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedemployeeroutetable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeRoutes", "DayId", "dbo.Days");
            DropForeignKey("dbo.EmployeeRoutes", "WeekId", "dbo.Weeks");
            DropForeignKey("dbo.EmployeeRoutes", "ZipId", "dbo.Zips");
            DropIndex("dbo.EmployeeRoutes", new[] { "ZipId" });
            DropIndex("dbo.EmployeeRoutes", new[] { "WeekId" });
            DropIndex("dbo.EmployeeRoutes", new[] { "DayId" });
            DropTable("dbo.EmployeeRoutes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmployeeRoutes",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        ZipId = c.Int(nullable: false),
                        WeekId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RouteId);
            
            CreateIndex("dbo.EmployeeRoutes", "DayId");
            CreateIndex("dbo.EmployeeRoutes", "WeekId");
            CreateIndex("dbo.EmployeeRoutes", "ZipId");
            AddForeignKey("dbo.EmployeeRoutes", "ZipId", "dbo.Zips", "ZipId", cascadeDelete: true);
            AddForeignKey("dbo.EmployeeRoutes", "WeekId", "dbo.Weeks", "WeekId", cascadeDelete: true);
            AddForeignKey("dbo.EmployeeRoutes", "DayId", "dbo.Days", "DayId", cascadeDelete: true);
        }
    }
}
