namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedweektable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Weeks",
                c => new
                    {
                        WeekId = c.Int(nullable: false, identity: true),
                        StartingWeek = c.String(),
                    })
                .PrimaryKey(t => t.WeekId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Weeks");
        }
    }
}
