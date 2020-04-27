namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActiveTests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuizTestID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.QuizTests", t => t.QuizTestID, cascadeDelete: false)
                .Index(t => t.QuizTestID)
                .Index(t => t.CreatorID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        status = c.Int(nullable: false),
                        password = c.String(nullable: false, maxLength: 500),
                        gender = c.Int(nullable: false),
                        username = c.String(nullable: false, maxLength: 20, unicode: false),
                        fullname = c.String(maxLength: 100),
                        email = c.String(maxLength: 100),
                        type = c.Int(nullable: false),
                        role = c.String(nullable: false, maxLength: 15),
                        description = c.String(storeType: "ntext"),
                        register_date = c.DateTime(),
                        AvatarImage = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.username, unique: true)
                .Index(t => t.email, unique: true);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Time = c.Int(nullable: false),
                        CreatorID = c.Int(nullable: false),
                        File = c.String(),
                        YoutubeLink = c.String(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 256),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: true)
                .Index(t => t.CreatorID);
            
            CreateTable(
                "dbo.QuizTests",
                c => new
                    {
                        TestID = c.Int(nullable: false, identity: true),
                        TotalTime = c.Int(nullable: false),
                        TotalMark = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 100),
                        CreatorID = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LessonId = c.Int(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.TestID)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.CreatorID)
                .Index(t => t.LessonId)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        QuizID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        CreatorID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        content = c.String(nullable: false, maxLength: 500),
                        HardType = c.Int(nullable: false),
                        image = c.String(maxLength: 100),
                        answerA = c.String(nullable: false, maxLength: 200),
                        answerB = c.String(nullable: false, maxLength: 200),
                        answerC = c.String(nullable: false, maxLength: 200),
                        answerD = c.String(nullable: false, maxLength: 200),
                        trueAnswer = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuizID)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: false)
                .Index(t => t.CreatorID)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.QuizResults",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Score = c.Double(nullable: false),
                        DoneAt = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        ActiveTestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.ActiveTests", t => t.ActiveTestID)
                .Index(t => t.StudentID)
                .Index(t => t.ActiveTestID);
            
            CreateTable(
                "dbo.Quiz_Of_Test",
                c => new
                    {
                        TestID = c.Int(nullable: false),
                        QuizID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TestID, t.QuizID })
                .ForeignKey("dbo.QuizTests", t => t.TestID)
                .ForeignKey("dbo.Quizs", t => t.QuizID)
                .Index(t => t.TestID)
                .Index(t => t.QuizID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActiveTests", "QuizTestID", "dbo.QuizTests");
            DropForeignKey("dbo.QuizResults", "ActiveTestID", "dbo.ActiveTests");
            DropForeignKey("dbo.QuizResults", "StudentID", "dbo.Users");
            DropForeignKey("dbo.ActiveTests", "CreatorID", "dbo.Users");
            DropForeignKey("dbo.QuizTests", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Quiz_Of_Test", "QuizID", "dbo.Quizs");
            DropForeignKey("dbo.Quiz_Of_Test", "TestID", "dbo.QuizTests");
            DropForeignKey("dbo.Quizs", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Quizs", "CreatorID", "dbo.Users");
            DropForeignKey("dbo.QuizTests", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.QuizTests", "CreatorID", "dbo.Users");
            DropForeignKey("dbo.Lessons", "CreatorID", "dbo.Users");
            DropIndex("dbo.Quiz_Of_Test", new[] { "QuizID" });
            DropIndex("dbo.Quiz_Of_Test", new[] { "TestID" });
            DropIndex("dbo.QuizResults", new[] { "ActiveTestID" });
            DropIndex("dbo.QuizResults", new[] { "StudentID" });
            DropIndex("dbo.Quizs", new[] { "LessonId" });
            DropIndex("dbo.Quizs", new[] { "CreatorID" });
            DropIndex("dbo.QuizTests", new[] { "User_ID" });
            DropIndex("dbo.QuizTests", new[] { "LessonId" });
            DropIndex("dbo.QuizTests", new[] { "CreatorID" });
            DropIndex("dbo.Lessons", new[] { "CreatorID" });
            DropIndex("dbo.Users", new[] { "email" });
            DropIndex("dbo.Users", new[] { "username" });
            DropIndex("dbo.ActiveTests", new[] { "CreatorID" });
            DropIndex("dbo.ActiveTests", new[] { "QuizTestID" });
            DropTable("dbo.Quiz_Of_Test");
            DropTable("dbo.QuizResults");
            DropTable("dbo.Quizs");
            DropTable("dbo.QuizTests");
            DropTable("dbo.Lessons");
            DropTable("dbo.Users");
            DropTable("dbo.ActiveTests");
        }
    }
}
