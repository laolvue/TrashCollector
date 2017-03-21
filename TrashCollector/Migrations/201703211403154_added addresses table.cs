namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedaddressestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressName = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            AddColumn("dbo.People", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.People", "AddressId");
            AddForeignKey("dbo.People", "AddressId", "dbo.Addresses", "AddressId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "CityId", "dbo.Cities");
            DropIndex("dbo.People", new[] { "AddressId" });
            DropIndex("dbo.Addresses", new[] { "CityId" });
            DropColumn("dbo.People", "AddressId");
            DropTable("dbo.Addresses");
        }
    }
}
