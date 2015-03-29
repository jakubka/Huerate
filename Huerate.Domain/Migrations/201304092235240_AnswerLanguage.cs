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
    
    public partial class AnswerLanguage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Language", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "Language");
        }
    }
}
