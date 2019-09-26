namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuizTests", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quizs", "name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Quizs", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "CreateDate");
            DropColumn("dbo.Quizs", "name");
            DropColumn("dbo.QuizTests", "CreateDate");
        }
    }
}
