namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuizTests", "LessonId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuizTests", "LessonId");
            AddForeignKey("dbo.QuizTests", "LessonId", "dbo.Lessons", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizTests", "LessonId", "dbo.Lessons");
            DropIndex("dbo.QuizTests", new[] { "LessonId" });
            DropColumn("dbo.QuizTests", "LessonId");
        }
    }
}
