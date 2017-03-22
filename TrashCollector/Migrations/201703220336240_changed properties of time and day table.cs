namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedpropertiesoftimeanddaytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Days", "DayName", c => c.String());
            AddColumn("dbo.Times", "TimeName", c => c.String());
            DropColumn("dbo.Days", "Monday");
            DropColumn("dbo.Days", "Tuesday");
            DropColumn("dbo.Days", "Wednesday");
            DropColumn("dbo.Days", "Thursday");
            DropColumn("dbo.Days", "Friday");
            DropColumn("dbo.Times", "Morning");
            DropColumn("dbo.Times", "Afternoon");
            DropColumn("dbo.Times", "Evening");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Times", "Evening", c => c.String());
            AddColumn("dbo.Times", "Afternoon", c => c.String());
            AddColumn("dbo.Times", "Morning", c => c.String());
            AddColumn("dbo.Days", "Friday", c => c.String());
            AddColumn("dbo.Days", "Thursday", c => c.String());
            AddColumn("dbo.Days", "Wednesday", c => c.String());
            AddColumn("dbo.Days", "Tuesday", c => c.String());
            AddColumn("dbo.Days", "Monday", c => c.String());
            DropColumn("dbo.Times", "TimeName");
            DropColumn("dbo.Days", "DayName");
        }
    }
}
