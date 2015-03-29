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
    
    public partial class EmailTemplates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduledOutgoingEmails", "OutgoingEmailDataId", "dbo.OutgoingEmailDatas");
            DropForeignKey("dbo.EmailBodySubstitutions", "ScheduledOutgoingEmail_Id", "dbo.ScheduledOutgoingEmails");
            DropIndex("dbo.ScheduledOutgoingEmails", new[] { "OutgoingEmailDataId" });
            DropIndex("dbo.EmailBodySubstitutions", new[] { "ScheduledOutgoingEmail_Id" });
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeName = c.String(),
                        Culture = c.String(),
                        SubjectTemplate = c.String(),
                        HtmlBodyTemplate = c.String(),
                        PlainTextBodyTemplate = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ScheduledOutgoingEmails", "EmailTemplateId", c => c.Int());
            AddForeignKey("dbo.ScheduledOutgoingEmails", "EmailTemplateId", "dbo.EmailTemplates", "Id");
            CreateIndex("dbo.ScheduledOutgoingEmails", "EmailTemplateId");
            DropColumn("dbo.ScheduledOutgoingEmails", "OutgoingEmailDataId");
            DropTable("dbo.OutgoingEmailDatas");
            DropTable("dbo.EmailBodySubstitutions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmailBodySubstitutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        Text = c.String(),
                        ScheduledOutgoingEmail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OutgoingEmailDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Body = c.String(),
                        IsBodyHtml = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ScheduledOutgoingEmails", "OutgoingEmailDataId", c => c.Int(nullable: false));
            DropIndex("dbo.ScheduledOutgoingEmails", new[] { "EmailTemplateId" });
            DropForeignKey("dbo.ScheduledOutgoingEmails", "EmailTemplateId", "dbo.EmailTemplates");
            DropColumn("dbo.ScheduledOutgoingEmails", "EmailTemplateId");
            DropTable("dbo.EmailTemplates");
            CreateIndex("dbo.EmailBodySubstitutions", "ScheduledOutgoingEmail_Id");
            CreateIndex("dbo.ScheduledOutgoingEmails", "OutgoingEmailDataId");
            AddForeignKey("dbo.EmailBodySubstitutions", "ScheduledOutgoingEmail_Id", "dbo.ScheduledOutgoingEmails", "Id");
            AddForeignKey("dbo.ScheduledOutgoingEmails", "OutgoingEmailDataId", "dbo.OutgoingEmailDatas", "Id");
        }
    }
}
