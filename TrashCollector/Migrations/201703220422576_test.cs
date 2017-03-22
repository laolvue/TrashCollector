namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Days", "SetPickUp_Id", "dbo.SetPickUps");
            DropForeignKey("dbo.Times", "SetPickUp_Id", "dbo.SetPickUps");
            DropIndex("dbo.Days", new[] { "SetPickUp_Id" });
            DropIndex("dbo.Times", new[] { "SetPickUp_Id" });
            DropColumn("dbo.Days", "SetPickUp_Id");
            DropColumn("dbo.Times", "SetPickUp_Id");
            DropTable("dbo.SetPickUps");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SetPickUps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Times", "SetPickUp_Id", c => c.Int());
            AddColumn("dbo.Days", "SetPickUp_Id", c => c.Int());
            CreateIndex("dbo.Times", "SetPickUp_Id");
            CreateIndex("dbo.Days", "SetPickUp_Id");
            AddForeignKey("dbo.Times", "SetPickUp_Id", "dbo.SetPickUps", "Id");
            AddForeignKey("dbo.Days", "SetPickUp_Id", "dbo.SetPickUps", "Id");
        }
    }
}
