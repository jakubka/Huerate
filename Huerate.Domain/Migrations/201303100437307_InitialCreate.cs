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
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        AnswerSetId = c.Guid(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnswerSets", t => t.AnswerSetId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.AnswerSetId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.AnswerSets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        TextAnswer = c.String(),
                        RestaurantAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RestaurantAccounts", t => t.RestaurantAccountId)
                .Index(t => t.RestaurantAccountId);
            
            CreateTable(
                "dbo.RestaurantAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeName = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 200),
                        CreationDate = c.DateTime(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        PasswordSalt = c.String(nullable: false, maxLength: 50),
                        AccountGuid = c.Guid(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        EmailNotificationsEnabled = c.Boolean(nullable: false),
                        DisplayName = c.String(nullable: false),
                        OverallQuestionId = c.Int(),
                        FirstSpecialQuestionId = c.Int(),
                        SecondSpecialQuestionId = c.Int(),
                        ThirdSpecialQuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.OverallQuestionId)
                .ForeignKey("dbo.Questions", t => t.FirstSpecialQuestionId)
                .ForeignKey("dbo.Questions", t => t.SecondSpecialQuestionId)
                .ForeignKey("dbo.Questions", t => t.ThirdSpecialQuestionId)
                .Index(t => t.OverallQuestionId)
                .Index(t => t.FirstSpecialQuestionId)
                .Index(t => t.SecondSpecialQuestionId)
                .Index(t => t.ThirdSpecialQuestionId)
                .Index(t => t.Email, true)
                .Index(t => t.CodeName, true);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Order = c.Int(nullable: false),
                        IsPredefined = c.Boolean(nullable: false),
                        Weight = c.Int(nullable: false),
                        ImagePath = c.String(),
                        OwnerId = c.Int(nullable: false),
                        DependsOnQuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RestaurantAccounts", t => t.OwnerId)
                .ForeignKey("dbo.Questions", t => t.DependsOnQuestionId)
                .Index(t => t.OwnerId)
                .Index(t => t.DependsOnQuestionId);
            
            CreateTable(
                "dbo.NewsletterSubscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        IPAddress = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Recorded = c.DateTime(nullable: false),
                        CustomData = c.String(),
                        ContactId = c.Int(nullable: false),
                        LoggedInRestaurantId = c.Int(),
                        ActivityTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId)
                .ForeignKey("dbo.RestaurantAccounts", t => t.LoggedInRestaurantId)
                .ForeignKey("dbo.ActivityTypes", t => t.ActivityTypeId)
                .Index(t => t.ContactId)
                .Index(t => t.LoggedInRestaurantId)
                .Index(t => t.ActivityTypeId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActivityTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Points = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.ScheduledOutgoingEmails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutgoingEmailDataId = c.Int(nullable: false),
                        ScheduledSendDateTime = c.DateTime(),
                        ReceiverAddress = c.String(nullable: false),
                        SendDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OutgoingEmailDatas", t => t.OutgoingEmailDataId)
                .Index(t => t.OutgoingEmailDataId);
            
            CreateTable(
                "dbo.EmailBodySubstitutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        Text = c.String(),
                        ScheduledOutgoingEmail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScheduledOutgoingEmails", t => t.ScheduledOutgoingEmail_Id)
                .Index(t => t.ScheduledOutgoingEmail_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.EmailBodySubstitutions", new[] { "ScheduledOutgoingEmail_Id" });
            DropIndex("dbo.ScheduledOutgoingEmails", new[] { "OutgoingEmailDataId" });
            DropIndex("dbo.Activities", new[] { "ActivityTypeId" });
            DropIndex("dbo.Activities", new[] { "LoggedInRestaurantId" });
            DropIndex("dbo.Activities", new[] { "ContactId" });
            DropIndex("dbo.Questions", new[] { "DependsOnQuestionId" });
            DropIndex("dbo.Questions", new[] { "OwnerId" });
            DropIndex("dbo.RestaurantAccounts", new[] { "ThirdSpecialQuestionId" });
            DropIndex("dbo.RestaurantAccounts", new[] { "SecondSpecialQuestionId" });
            DropIndex("dbo.RestaurantAccounts", new[] { "FirstSpecialQuestionId" });
            DropIndex("dbo.RestaurantAccounts", new[] { "OverallQuestionId" });
            DropIndex("dbo.AnswerSets", new[] { "RestaurantAccountId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Answers", new[] { "AnswerSetId" });
            DropForeignKey("dbo.EmailBodySubstitutions", "ScheduledOutgoingEmail_Id", "dbo.ScheduledOutgoingEmails");
            DropForeignKey("dbo.ScheduledOutgoingEmails", "OutgoingEmailDataId", "dbo.OutgoingEmailDatas");
            DropForeignKey("dbo.Activities", "ActivityTypeId", "dbo.ActivityTypes");
            DropForeignKey("dbo.Activities", "LoggedInRestaurantId", "dbo.RestaurantAccounts");
            DropForeignKey("dbo.Activities", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Questions", "DependsOnQuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "OwnerId", "dbo.RestaurantAccounts");
            DropForeignKey("dbo.RestaurantAccounts", "ThirdSpecialQuestionId", "dbo.Questions");
            DropForeignKey("dbo.RestaurantAccounts", "SecondSpecialQuestionId", "dbo.Questions");
            DropForeignKey("dbo.RestaurantAccounts", "FirstSpecialQuestionId", "dbo.Questions");
            DropForeignKey("dbo.RestaurantAccounts", "OverallQuestionId", "dbo.Questions");
            DropForeignKey("dbo.AnswerSets", "RestaurantAccountId", "dbo.RestaurantAccounts");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "AnswerSetId", "dbo.AnswerSets");
            DropTable("dbo.EmailBodySubstitutions");
            DropTable("dbo.ScheduledOutgoingEmails");
            DropTable("dbo.OutgoingEmailDatas");
            DropTable("dbo.ActivityTypes");
            DropTable("dbo.Contacts");
            DropTable("dbo.Activities");
            DropTable("dbo.NewsletterSubscriptions");
            DropTable("dbo.Questions");
            DropTable("dbo.RestaurantAccounts");
            DropTable("dbo.AnswerSets");
            DropTable("dbo.Answers");
        }
    }
}
