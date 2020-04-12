namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class convertDatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActiveTests", "FromTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ActiveTests", "ToTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Lessons", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Lessons", "ModifiedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Quizs", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.QuizTests", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuizTests", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Quizs", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Lessons", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Lessons", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActiveTests", "ToTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActiveTests", "FromTime", c => c.DateTime(nullable: false));
        }
    }
}
