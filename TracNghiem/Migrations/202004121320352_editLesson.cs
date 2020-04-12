namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editLesson : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lessons", "YoutubeLink", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lessons", "YoutubeLink", c => c.String());
        }
    }
}
