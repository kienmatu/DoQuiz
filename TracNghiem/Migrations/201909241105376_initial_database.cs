namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuizTests",
                c => new
                    {
                        TestID = c.Int(nullable: false, identity: true),
                        TotalTime = c.Int(nullable: false),
                        TotalMark = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 100),
                        CreatorID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        Quiz_QuizID = c.Int(),
                    })
                .PrimaryKey(t => t.TestID)
                .ForeignKey("dbo.Quizs", t => t.Quiz_QuizID)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.CreatorID)
                .Index(t => t.SubjectID)
                .Index(t => t.Quiz_QuizID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        status = c.Int(nullable: false),
                        password = c.String(nullable: false, maxLength: 500),
                        gender = c.Int(nullable: false),
                        username = c.String(nullable: false, maxLength: 20),
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
                "dbo.Quizs",
                c => new
                    {
                        QuizID = c.Int(nullable: false, identity: true),
                        CreatorID = c.Int(nullable: false),
                        content = c.String(nullable: false, maxLength: 500),
                        HardType = c.Int(nullable: false),
                        image = c.String(maxLength: 100),
                        SubjectID = c.Int(nullable: false),
                        answerA = c.String(nullable: false, maxLength: 200),
                        answerB = c.String(nullable: false, maxLength: 200),
                        answerC = c.String(nullable: false, maxLength: 200),
                        answerD = c.String(nullable: false, maxLength: 200),
                        trueAnswer = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        QuizTest_TestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuizID)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .ForeignKey("dbo.QuizTests", t => t.QuizTest_TestID)
                .Index(t => t.CreatorID)
                .Index(t => t.SubjectID)
                .Index(t => t.QuizTest_TestID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizTests", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Quizs", "QuizTest_TestID", "dbo.QuizTests");
            DropForeignKey("dbo.QuizTests", "CreatorID", "dbo.Users");
            DropForeignKey("dbo.Quizs", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.QuizTests", "Quiz_QuizID", "dbo.Quizs");
            DropForeignKey("dbo.Quizs", "CreatorID", "dbo.Users");
            DropIndex("dbo.Quizs", new[] { "QuizTest_TestID" });
            DropIndex("dbo.Quizs", new[] { "SubjectID" });
            DropIndex("dbo.Quizs", new[] { "CreatorID" });
            DropIndex("dbo.Users", new[] { "email" });
            DropIndex("dbo.Users", new[] { "username" });
            DropIndex("dbo.QuizTests", new[] { "Quiz_QuizID" });
            DropIndex("dbo.QuizTests", new[] { "SubjectID" });
            DropIndex("dbo.QuizTests", new[] { "CreatorID" });
            DropTable("dbo.Subjects");
            DropTable("dbo.Quizs");
            DropTable("dbo.Users");
            DropTable("dbo.QuizTests");
        }
    }
}
