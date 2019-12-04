namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixtablequizresult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuizResults", "ActiveTestID", c => c.Int(nullable: false));
            AlterColumn("dbo.QuizResults", "Score", c => c.Double(nullable: false));
            CreateIndex("dbo.QuizResults", "ActiveTestID");
            AddForeignKey("dbo.QuizResults", "ActiveTestID", "dbo.ActiveTests", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizResults", "ActiveTestID", "dbo.ActiveTests");
            DropIndex("dbo.QuizResults", new[] { "ActiveTestID" });
            AlterColumn("dbo.QuizResults", "Score", c => c.Int(nullable: false));
            DropColumn("dbo.QuizResults", "ActiveTestID");
        }
    }
}
