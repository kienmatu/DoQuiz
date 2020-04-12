namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLesson : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quizs", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.QuizTests", "SubjectID", "dbo.Subjects");
            DropIndex("dbo.QuizTests", new[] { "SubjectID" });
            DropIndex("dbo.Quizs", new[] { "SubjectID" });
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Time = c.Int(nullable: false),
                        CreatorID = c.Int(nullable: false),
                        File = c.String(),
                        YoutubeLink = c.String(),
                        Description = c.String(nullable: false, maxLength: 256),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: true),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: true),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: true)
                .Index(t => t.CreatorID);
            
            AddColumn("dbo.Quizs", "LessonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Quizs", "LessonId");
            AddForeignKey("dbo.Quizs", "LessonId", "dbo.Lessons", "ID", cascadeDelete: false);
            DropColumn("dbo.QuizTests", "SubjectID");
            DropColumn("dbo.Quizs", "SubjectID");
            DropTable("dbo.Subjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Quizs", "SubjectID", c => c.Int(nullable: false));
            AddColumn("dbo.QuizTests", "SubjectID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Quizs", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons", "CreatorID", "dbo.Users");
            DropIndex("dbo.Quizs", new[] { "LessonId" });
            DropIndex("dbo.Lessons", new[] { "CreatorID" });
            DropColumn("dbo.Quizs", "LessonId");
            DropTable("dbo.Lessons");
            CreateIndex("dbo.Quizs", "SubjectID");
            CreateIndex("dbo.QuizTests", "SubjectID");
            AddForeignKey("dbo.QuizTests", "SubjectID", "dbo.Subjects", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Quizs", "SubjectID", "dbo.Subjects", "ID", cascadeDelete: true);
        }
    }
}
