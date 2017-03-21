namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstateforeignkeytocitytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "StateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cities", "StateId");
            AddForeignKey("dbo.Cities", "StateId", "dbo.States", "StateId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropColumn("dbo.Cities", "StateId");
        }
    }
}
