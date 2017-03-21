namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcitiestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        ZipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Zips", t => t.ZipId, cascadeDelete: true)
                .Index(t => t.ZipId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "ZipId", "dbo.Zips");
            DropIndex("dbo.Cities", new[] { "ZipId" });
            DropTable("dbo.Cities");
        }
    }
}
