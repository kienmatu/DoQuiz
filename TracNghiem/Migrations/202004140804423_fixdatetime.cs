namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixdatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActiveTests", "FromTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActiveTests", "ToTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Lessons", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.QuizTests", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Quizs", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Lessons", "ModifiedBy");
            DropColumn("dbo.Lessons", "ModifiedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lessons", "ModifiedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Lessons", "ModifiedBy", c => c.String());
            AlterColumn("dbo.Quizs", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.QuizTests", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Lessons", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ActiveTests", "ToTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ActiveTests", "FromTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
