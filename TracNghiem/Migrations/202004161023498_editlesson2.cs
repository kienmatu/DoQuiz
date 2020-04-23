namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editlesson2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "OrderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lessons", "OrderId");
        }
    }
}
