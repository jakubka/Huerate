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
    
    public partial class FormSteps : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.RestaurantAccounts", "OverallQuestionId", "dbo.Questions");
            DropForeignKey("dbo.RestaurantAccounts", "FirstSpecialQuestionId", "dbo.Questions");
            DropForeignKey("dbo.RestaurantAccounts", "SecondSpecialQuestionId", "dbo.Questions");
            DropForeignKey("dbo.RestaurantAccounts", "ThirdSpecialQuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "DependsOnQuestionId", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.RestaurantAccounts", new[] { "OverallQuestionId" });
            DropIndex("dbo.RestaurantAccounts", new[] { "FirstSpecialQuestionId" });
            DropIndex("dbo.RestaurantAccounts", new[] { "SecondSpecialQuestionId" });
            DropIndex("dbo.RestaurantAccounts", new[] { "ThirdSpecialQuestionId" });
            DropIndex("dbo.Questions", new[] { "DependsOnQuestionId" });
            CreateTable(
                "dbo.FormSteps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        RestaurantAccountId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RestaurantAccounts", t => t.RestaurantAccountId)
                .Index(t => t.RestaurantAccountId);
            
            CreateTable(
                "dbo.PercentQuestionsFormSteps",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DisplayQuestionsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormSteps", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PercentQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PercentQuestionsFormStepId = c.Int(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Id)
                .ForeignKey("dbo.PercentQuestionsFormSteps", t => t.PercentQuestionsFormStepId)
                .Index(t => t.Id)
                .Index(t => t.PercentQuestionsFormStepId);
            
            CreateTable(
                "dbo.PercentAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PercentQuestionId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.Id)
                .ForeignKey("dbo.PercentQuestions", t => t.PercentQuestionId)
                .Index(t => t.Id)
                .Index(t => t.PercentQuestionId);
            
            CreateTable(
                "dbo.YesNoQuestionsFormSteps",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DisplayQuestionsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormSteps", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.YesNoQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        YesNoQuestionsFormStepId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Id)
                .ForeignKey("dbo.YesNoQuestionsFormSteps", t => t.YesNoQuestionsFormStepId)
                .Index(t => t.Id)
                .Index(t => t.YesNoQuestionsFormStepId);
            
            CreateTable(
                "dbo.YesNoAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        YesNoQuestionId = c.Int(nullable: false),
                        Value = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.Id)
                .ForeignKey("dbo.YesNoQuestions", t => t.YesNoQuestionId)
                .Index(t => t.Id)
                .Index(t => t.YesNoQuestionId);
            
            CreateTable(
                "dbo.TextInfoFormSteps",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormSteps", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TextQuestionFormSteps",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TitleText = c.String(),
                        InfoText = c.String(),
                        SubmitButtonText = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormSteps", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TextAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TextQuestionFormStepId = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.Id)
                .ForeignKey("dbo.TextQuestionFormSteps", t => t.TextQuestionFormStepId)
                .Index(t => t.Id)
                .Index(t => t.TextQuestionFormStepId);
            
            CreateTable(
                "dbo.ConfirmationFormSteps",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TitleText = c.String(),
                        InfoText = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormSteps", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.RestaurantAccounts", "DefaultCulture", c => c.String());
            DropColumn("dbo.Answers", "Value");
            DropColumn("dbo.Answers", "QuestionId");
            DropColumn("dbo.AnswerSets", "TextAnswer");
            DropColumn("dbo.RestaurantAccounts", "OverallQuestionId");
            DropColumn("dbo.RestaurantAccounts", "FirstSpecialQuestionId");
            DropColumn("dbo.RestaurantAccounts", "SecondSpecialQuestionId");
            DropColumn("dbo.RestaurantAccounts", "ThirdSpecialQuestionId");
            DropColumn("dbo.Questions", "IsPredefined");
            DropColumn("dbo.Questions", "Weight");
            DropColumn("dbo.Questions", "ImagePath");
            DropColumn("dbo.Questions", "DependsOnQuestionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "DependsOnQuestionId", c => c.Int());
            AddColumn("dbo.Questions", "ImagePath", c => c.String());
            AddColumn("dbo.Questions", "Weight", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "IsPredefined", c => c.Boolean(nullable: false));
            AddColumn("dbo.RestaurantAccounts", "ThirdSpecialQuestionId", c => c.Int());
            AddColumn("dbo.RestaurantAccounts", "SecondSpecialQuestionId", c => c.Int());
            AddColumn("dbo.RestaurantAccounts", "FirstSpecialQuestionId", c => c.Int());
            AddColumn("dbo.RestaurantAccounts", "OverallQuestionId", c => c.Int());
            AddColumn("dbo.AnswerSets", "TextAnswer", c => c.String());
            AddColumn("dbo.Answers", "QuestionId", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "Value", c => c.Int(nullable: false));
            DropIndex("dbo.ConfirmationFormSteps", new[] { "Id" });
            DropIndex("dbo.TextAnswers", new[] { "TextQuestionFormStepId" });
            DropIndex("dbo.TextAnswers", new[] { "Id" });
            DropIndex("dbo.TextQuestionFormSteps", new[] { "Id" });
            DropIndex("dbo.TextInfoFormSteps", new[] { "Id" });
            DropIndex("dbo.YesNoAnswers", new[] { "YesNoQuestionId" });
            DropIndex("dbo.YesNoAnswers", new[] { "Id" });
            DropIndex("dbo.YesNoQuestions", new[] { "YesNoQuestionsFormStepId" });
            DropIndex("dbo.YesNoQuestions", new[] { "Id" });
            DropIndex("dbo.YesNoQuestionsFormSteps", new[] { "Id" });
            DropIndex("dbo.PercentAnswers", new[] { "PercentQuestionId" });
            DropIndex("dbo.PercentAnswers", new[] { "Id" });
            DropIndex("dbo.PercentQuestions", new[] { "PercentQuestionsFormStepId" });
            DropIndex("dbo.PercentQuestions", new[] { "Id" });
            DropIndex("dbo.PercentQuestionsFormSteps", new[] { "Id" });
            DropIndex("dbo.FormSteps", new[] { "RestaurantAccountId" });
            DropForeignKey("dbo.ConfirmationFormSteps", "Id", "dbo.FormSteps");
            DropForeignKey("dbo.TextAnswers", "TextQuestionFormStepId", "dbo.TextQuestionFormSteps");
            DropForeignKey("dbo.TextAnswers", "Id", "dbo.Answers");
            DropForeignKey("dbo.TextQuestionFormSteps", "Id", "dbo.FormSteps");
            DropForeignKey("dbo.TextInfoFormSteps", "Id", "dbo.FormSteps");
            DropForeignKey("dbo.YesNoAnswers", "YesNoQuestionId", "dbo.YesNoQuestions");
            DropForeignKey("dbo.YesNoAnswers", "Id", "dbo.Answers");
            DropForeignKey("dbo.YesNoQuestions", "YesNoQuestionsFormStepId", "dbo.YesNoQuestionsFormSteps");
            DropForeignKey("dbo.YesNoQuestions", "Id", "dbo.Questions");
            DropForeignKey("dbo.YesNoQuestionsFormSteps", "Id", "dbo.FormSteps");
            DropForeignKey("dbo.PercentAnswers", "PercentQuestionId", "dbo.PercentQuestions");
            DropForeignKey("dbo.PercentAnswers", "Id", "dbo.Answers");
            DropForeignKey("dbo.PercentQuestions", "PercentQuestionsFormStepId", "dbo.PercentQuestionsFormSteps");
            DropForeignKey("dbo.PercentQuestions", "Id", "dbo.Questions");
            DropForeignKey("dbo.PercentQuestionsFormSteps", "Id", "dbo.FormSteps");
            DropForeignKey("dbo.FormSteps", "RestaurantAccountId", "dbo.RestaurantAccounts");
            DropColumn("dbo.RestaurantAccounts", "DefaultCulture");
            DropTable("dbo.ConfirmationFormSteps");
            DropTable("dbo.TextAnswers");
            DropTable("dbo.TextQuestionFormSteps");
            DropTable("dbo.TextInfoFormSteps");
            DropTable("dbo.YesNoAnswers");
            DropTable("dbo.YesNoQuestions");
            DropTable("dbo.YesNoQuestionsFormSteps");
            DropTable("dbo.PercentAnswers");
            DropTable("dbo.PercentQuestions");
            DropTable("dbo.PercentQuestionsFormSteps");
            DropTable("dbo.FormSteps");
            CreateIndex("dbo.Questions", "DependsOnQuestionId");
            CreateIndex("dbo.RestaurantAccounts", "ThirdSpecialQuestionId");
            CreateIndex("dbo.RestaurantAccounts", "SecondSpecialQuestionId");
            CreateIndex("dbo.RestaurantAccounts", "FirstSpecialQuestionId");
            CreateIndex("dbo.RestaurantAccounts", "OverallQuestionId");
            CreateIndex("dbo.Answers", "QuestionId");
            AddForeignKey("dbo.Questions", "DependsOnQuestionId", "dbo.Questions", "Id");
            AddForeignKey("dbo.RestaurantAccounts", "ThirdSpecialQuestionId", "dbo.Questions", "Id");
            AddForeignKey("dbo.RestaurantAccounts", "SecondSpecialQuestionId", "dbo.Questions", "Id");
            AddForeignKey("dbo.RestaurantAccounts", "FirstSpecialQuestionId", "dbo.Questions", "Id");
            AddForeignKey("dbo.RestaurantAccounts", "OverallQuestionId", "dbo.Questions", "Id");
            AddForeignKey("dbo.Answers", "QuestionId", "dbo.Questions", "Id");
        }
    }
}
