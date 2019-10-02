namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixrela : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quizs", "QuizTest_TestID", "dbo.QuizTests");
            DropIndex("dbo.Users", new[] { "username" });
            DropIndex("dbo.Quizs", new[] { "QuizTest_TestID" });
            AlterColumn("dbo.Users", "username", c => c.String(nullable: false, maxLength: 20, unicode: false));
            CreateIndex("dbo.QuizTests", "TestID");
            CreateIndex("dbo.Users", "username", unique: true);
            AddForeignKey("dbo.QuizTests", "TestID", "dbo.Quizs", "QuizID");
            DropColumn("dbo.Quizs", "QuizTest_TestID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quizs", "QuizTest_TestID", c => c.Int());
            DropForeignKey("dbo.QuizTests", "TestID", "dbo.Quizs");
            DropIndex("dbo.Users", new[] { "username" });
            DropIndex("dbo.QuizTests", new[] { "TestID" });
            AlterColumn("dbo.Users", "username", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Quizs", "QuizTest_TestID");
            CreateIndex("dbo.Users", "username", unique: true);
            AddForeignKey("dbo.Quizs", "QuizTest_TestID", "dbo.QuizTests", "TestID");
        }
    }
}
