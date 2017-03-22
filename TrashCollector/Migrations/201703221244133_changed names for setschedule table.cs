namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changednamesforsetscheduletable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SetSchedules", name: "PersonId", newName: "TimeId");
            RenameIndex(table: "dbo.SetSchedules", name: "IX_PersonId", newName: "IX_TimeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SetSchedules", name: "IX_TimeId", newName: "IX_PersonId");
            RenameColumn(table: "dbo.SetSchedules", name: "TimeId", newName: "PersonId");
        }
    }
}
