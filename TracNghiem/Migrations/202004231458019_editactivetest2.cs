namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editactivetest2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ActiveTests", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActiveTests", "Code", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
