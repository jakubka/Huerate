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
    
    public partial class CascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnswerSets", "RestaurantAccountId", "dbo.RestaurantAccounts");
            DropForeignKey("dbo.FormSteps", "RestaurantAccountId", "dbo.RestaurantAccounts");
            DropForeignKey("dbo.Questions", "OwnerId", "dbo.RestaurantAccounts");
            DropForeignKey("dbo.Answers", "AnswerSetId", "dbo.AnswerSets");
            DropForeignKey("dbo.Activities", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Activities", "ActivityTypeId", "dbo.ActivityTypes");
            DropForeignKey("dbo.PercentAnswers", "PercentQuestionId", "dbo.PercentQuestions");
            DropForeignKey("dbo.YesNoAnswers", "YesNoQuestionId", "dbo.YesNoQuestions");
            DropForeignKey("dbo.TextAnswers", "TextQuestionFormStepId", "dbo.TextQuestionFormSteps");
            DropIndex("dbo.AnswerSets", new[] { "RestaurantAccountId" });
            DropIndex("dbo.FormSteps", new[] { "RestaurantAccountId" });
            DropIndex("dbo.Questions", new[] { "OwnerId" });
            DropIndex("dbo.Answers", new[] { "AnswerSetId" });
            DropIndex("dbo.Activities", new[] { "ContactId" });
            DropIndex("dbo.Activities", new[] { "ActivityTypeId" });
            DropIndex("dbo.PercentAnswers", new[] { "PercentQuestionId" });
            DropIndex("dbo.YesNoAnswers", new[] { "YesNoQuestionId" });
            DropIndex("dbo.TextAnswers", new[] { "TextQuestionFormStepId" });
            AddForeignKey("dbo.AnswerSets", "RestaurantAccountId", "dbo.RestaurantAccounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FormSteps", "RestaurantAccountId", "dbo.RestaurantAccounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "OwnerId", "dbo.RestaurantAccounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "AnswerSetId", "dbo.AnswerSets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Activities", "ContactId", "dbo.Contacts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Activities", "ActivityTypeId", "dbo.ActivityTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PercentAnswers", "PercentQuestionId", "dbo.PercentQuestions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.YesNoAnswers", "YesNoQuestionId", "dbo.YesNoQuestions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TextAnswers", "TextQuestionFormStepId", "dbo.TextQuestionFormSteps", "Id", cascadeDelete: true);
            CreateIndex("dbo.AnswerSets", "RestaurantAccountId");
            CreateIndex("dbo.FormSteps", "RestaurantAccountId");
            CreateIndex("dbo.Questions", "OwnerId");
            CreateIndex("dbo.Answers", "AnswerSetId");
            CreateIndex("dbo.Activities", "ContactId");
            CreateIndex("dbo.Activities", "ActivityTypeId");
            CreateIndex("dbo.PercentAnswers", "PercentQuestionId");
            CreateIndex("dbo.YesNoAnswers", "YesNoQuestionId");
            CreateIndex("dbo.TextAnswers", "TextQuestionFormStepId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TextAnswers", new[] { "TextQuestionFormStepId" });
            DropIndex("dbo.YesNoAnswers", new[] { "YesNoQuestionId" });
            DropIndex("dbo.PercentAnswers", new[] { "PercentQuestionId" });
            DropIndex("dbo.Activities", new[] { "ActivityTypeId" });
            DropIndex("dbo.Activities", new[] { "ContactId" });
            DropIndex("dbo.Answers", new[] { "AnswerSetId" });
            DropIndex("dbo.Questions", new[] { "OwnerId" });
            DropIndex("dbo.FormSteps", new[] { "RestaurantAccountId" });
            DropIndex("dbo.AnswerSets", new[] { "RestaurantAccountId" });
            DropForeignKey("dbo.TextAnswers", "TextQuestionFormStepId", "dbo.TextQuestionFormSteps");
            DropForeignKey("dbo.YesNoAnswers", "YesNoQuestionId", "dbo.YesNoQuestions");
            DropForeignKey("dbo.PercentAnswers", "PercentQuestionId", "dbo.PercentQuestions");
            DropForeignKey("dbo.Activities", "ActivityTypeId", "dbo.ActivityTypes");
            DropForeignKey("dbo.Activities", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Answers", "AnswerSetId", "dbo.AnswerSets");
            DropForeignKey("dbo.Questions", "OwnerId", "dbo.RestaurantAccounts");
            DropForeignKey("dbo.FormSteps", "RestaurantAccountId", "dbo.RestaurantAccounts");
            DropForeignKey("dbo.AnswerSets", "RestaurantAccountId", "dbo.RestaurantAccounts");
            CreateIndex("dbo.TextAnswers", "TextQuestionFormStepId");
            CreateIndex("dbo.YesNoAnswers", "YesNoQuestionId");
            CreateIndex("dbo.PercentAnswers", "PercentQuestionId");
            CreateIndex("dbo.Activities", "ActivityTypeId");
            CreateIndex("dbo.Activities", "ContactId");
            CreateIndex("dbo.Answers", "AnswerSetId");
            CreateIndex("dbo.Questions", "OwnerId");
            CreateIndex("dbo.FormSteps", "RestaurantAccountId");
            CreateIndex("dbo.AnswerSets", "RestaurantAccountId");
            AddForeignKey("dbo.TextAnswers", "TextQuestionFormStepId", "dbo.TextQuestionFormSteps", "Id");
            AddForeignKey("dbo.YesNoAnswers", "YesNoQuestionId", "dbo.YesNoQuestions", "Id");
            AddForeignKey("dbo.PercentAnswers", "PercentQuestionId", "dbo.PercentQuestions", "Id");
            AddForeignKey("dbo.Activities", "ActivityTypeId", "dbo.ActivityTypes", "Id");
            AddForeignKey("dbo.Activities", "ContactId", "dbo.Contacts", "Id");
            AddForeignKey("dbo.Answers", "AnswerSetId", "dbo.AnswerSets", "Id");
            AddForeignKey("dbo.Questions", "OwnerId", "dbo.RestaurantAccounts", "Id");
            AddForeignKey("dbo.FormSteps", "RestaurantAccountId", "dbo.RestaurantAccounts", "Id");
            AddForeignKey("dbo.AnswerSets", "RestaurantAccountId", "dbo.RestaurantAccounts", "Id");
        }
    }
}
