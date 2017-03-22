namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmodeltosetpickuptimes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SetPickUps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Days", "SetPickUp_Id", c => c.Int());
            AddColumn("dbo.Times", "SetPickUp_Id", c => c.Int());
            CreateIndex("dbo.Days", "SetPickUp_Id");
            CreateIndex("dbo.Times", "SetPickUp_Id");
            AddForeignKey("dbo.Days", "SetPickUp_Id", "dbo.SetPickUps", "Id");
            AddForeignKey("dbo.Times", "SetPickUp_Id", "dbo.SetPickUps", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Times", "SetPickUp_Id", "dbo.SetPickUps");
            DropForeignKey("dbo.Days", "SetPickUp_Id", "dbo.SetPickUps");
            DropIndex("dbo.Times", new[] { "SetPickUp_Id" });
            DropIndex("dbo.Days", new[] { "SetPickUp_Id" });
            DropColumn("dbo.Times", "SetPickUp_Id");
            DropColumn("dbo.Days", "SetPickUp_Id");
            DropTable("dbo.SetPickUps");
        }
    }
}
