namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddisplaynamesofaccountstable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "StreetAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "State", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "State", c => c.String());
            AlterColumn("dbo.Accounts", "City", c => c.String());
            AlterColumn("dbo.Accounts", "StreetAddress", c => c.String());
            AlterColumn("dbo.Accounts", "LastName", c => c.String());
        }
    }
}
