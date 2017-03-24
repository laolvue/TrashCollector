namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedbillingtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Billings",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        BillAmount = c.Double(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Billings", "PersonId", "dbo.People");
            DropIndex("dbo.Billings", new[] { "PersonId" });
            DropTable("dbo.Billings");
        }
    }
}
