namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editactivetest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ActiveTests", "FromTime");
            DropColumn("dbo.ActiveTests", "ToTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActiveTests", "ToTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.ActiveTests", "FromTime", c => c.DateTime(nullable: false));
        }
    }
}
