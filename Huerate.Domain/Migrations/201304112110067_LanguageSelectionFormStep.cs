/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

namespace Huerate.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LanguageSelectionFormStep : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LanguageSelectionFormSteps",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TitleText = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormSteps", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.LanguageSelectionFormSteps", new[] { "Id" });
            DropForeignKey("dbo.LanguageSelectionFormSteps", "Id", "dbo.FormSteps");
            DropTable("dbo.LanguageSelectionFormSteps");
        }
    }
}
