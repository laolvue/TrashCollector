namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedemployeeroutes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeRoutes",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        ZipId = c.Int(nullable: false),
                        WeekId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RouteId)
                .ForeignKey("dbo.Weeks", t => t.WeekId, cascadeDelete: true)
                .ForeignKey("dbo.Zips", t => t.ZipId, cascadeDelete: true)
                .Index(t => t.ZipId)
                .Index(t => t.WeekId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeRoutes", "ZipId", "dbo.Zips");
            DropForeignKey("dbo.EmployeeRoutes", "WeekId", "dbo.Weeks");
            DropIndex("dbo.EmployeeRoutes", new[] { "WeekId" });
            DropIndex("dbo.EmployeeRoutes", new[] { "ZipId" });
            DropTable("dbo.EmployeeRoutes");
        }
    }
}
