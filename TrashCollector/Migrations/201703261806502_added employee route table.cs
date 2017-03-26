namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedemployeeroutetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeRoutes", "DayId", c => c.Int(nullable: false));
            CreateIndex("dbo.EmployeeRoutes", "DayId");
            AddForeignKey("dbo.EmployeeRoutes", "DayId", "dbo.Days", "DayId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeRoutes", "DayId", "dbo.Days");
            DropIndex("dbo.EmployeeRoutes", new[] { "DayId" });
            DropColumn("dbo.EmployeeRoutes", "DayId");
        }
    }
}
