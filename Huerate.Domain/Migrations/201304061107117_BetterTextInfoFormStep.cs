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
    
    public partial class BetterTextInfoFormStep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TextInfoFormSteps", "TitleText", c => c.String());
            AddColumn("dbo.TextInfoFormSteps", "InfoText", c => c.String());
            AddColumn("dbo.TextInfoFormSteps", "SubmitButtonText", c => c.String());
            DropColumn("dbo.TextInfoFormSteps", "Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TextInfoFormSteps", "Text", c => c.String());
            DropColumn("dbo.TextInfoFormSteps", "SubmitButtonText");
            DropColumn("dbo.TextInfoFormSteps", "InfoText");
            DropColumn("dbo.TextInfoFormSteps", "TitleText");
        }
    }
}
