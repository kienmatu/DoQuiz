namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixrelation1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuizTests", "Quiz_QuizID", "dbo.Quizs");
            DropForeignKey("dbo.QuizTests", "TestID", "dbo.Quizs");
            DropForeignKey("dbo.QuizTests", "CreatorID", "dbo.Users");
            DropIndex("dbo.QuizTests", new[] { "TestID" });
            DropIndex("dbo.QuizTests", new[] { "Quiz_QuizID" });
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
            
            AddColumn("dbo.QuizTests", "User_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.QuizTests", "User_ID");
            AddForeignKey("dbo.QuizTests", "User_ID", "dbo.Users", "ID");
            DropColumn("dbo.QuizTests", "Quiz_QuizID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuizTests", "Quiz_QuizID", c => c.Int());
            DropForeignKey("dbo.QuizTests", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Quiz_Of_Test", "QuizID", "dbo.Quizs");
            DropForeignKey("dbo.Quiz_Of_Test", "TestID", "dbo.QuizTests");
            DropIndex("dbo.Quiz_Of_Test", new[] { "QuizID" });
            DropIndex("dbo.Quiz_Of_Test", new[] { "TestID" });
            DropIndex("dbo.QuizTests", new[] { "User_ID" });
            DropColumn("dbo.QuizTests", "User_ID");
            DropTable("dbo.Quiz_Of_Test");
            CreateIndex("dbo.QuizTests", "Quiz_QuizID");
            CreateIndex("dbo.QuizTests", "TestID");
            AddForeignKey("dbo.QuizTests", "CreatorID", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.QuizTests", "TestID", "dbo.Quizs", "QuizID");
            AddForeignKey("dbo.QuizTests", "Quiz_QuizID", "dbo.Quizs", "QuizID");
        }
    }
}
