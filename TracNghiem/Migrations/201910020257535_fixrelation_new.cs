namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixrelation_new : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.QuizTests", new[] { "User_ID" });
            AlterColumn("dbo.QuizTests", "User_ID", c => c.Int());
            CreateIndex("dbo.QuizTests", "User_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.QuizTests", new[] { "User_ID" });
            AlterColumn("dbo.QuizTests", "User_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.QuizTests", "User_ID");
        }
    }
}
