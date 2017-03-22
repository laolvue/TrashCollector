namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddayandtimetables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        Monday = c.String(),
                        Tuesday = c.String(),
                        Wednesday = c.String(),
                        Thursday = c.String(),
                        Friday = c.String(),
                    })
                .PrimaryKey(t => t.DayId);
            
            CreateTable(
                "dbo.Times",
                c => new
                    {
                        TimeId = c.Int(nullable: false, identity: true),
                        Morning = c.String(),
                        Afternoon = c.String(),
                        Evening = c.String(),
                    })
                .PrimaryKey(t => t.TimeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Times");
            DropTable("dbo.Days");
        }
    }
}
