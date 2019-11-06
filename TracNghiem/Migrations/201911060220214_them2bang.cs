namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class them2bang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActiveTests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuizTestID = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                        CreatorID = c.Int(nullable: false),
                        FromTime = c.DateTime(nullable: false),
                        ToTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: true)
                .ForeignKey("dbo.QuizTests", t => t.QuizTestID, cascadeDelete: true)
                .Index(t => t.QuizTestID)
                .Index(t => t.CreatorID);
            
            CreateTable(
                "dbo.QuizResults",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        DoneAt = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizResults", "StudentID", "dbo.Users");
            DropForeignKey("dbo.ActiveTests", "QuizTestID", "dbo.QuizTests");
            DropForeignKey("dbo.ActiveTests", "CreatorID", "dbo.Users");
            DropIndex("dbo.QuizResults", new[] { "StudentID" });
            DropIndex("dbo.ActiveTests", new[] { "CreatorID" });
            DropIndex("dbo.ActiveTests", new[] { "QuizTestID" });
            DropTable("dbo.QuizResults");
            DropTable("dbo.ActiveTests");
        }
    }
}
