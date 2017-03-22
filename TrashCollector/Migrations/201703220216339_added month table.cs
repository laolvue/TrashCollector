namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmonthtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Months",
                c => new
                    {
                        MonthId = c.Int(nullable: false, identity: true),
                        MonthName = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MonthId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Months");
        }
    }
}
