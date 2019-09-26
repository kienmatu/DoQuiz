namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixrelation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Quizs", new[] { "QuizTest_TestID" });
            AlterColumn("dbo.Quizs", "QuizTest_TestID", c => c.Int());
            CreateIndex("dbo.Quizs", "QuizTest_TestID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Quizs", new[] { "QuizTest_TestID" });
            AlterColumn("dbo.Quizs", "QuizTest_TestID", c => c.Int(nullable: false));
            CreateIndex("dbo.Quizs", "QuizTest_TestID");
        }
    }
}
