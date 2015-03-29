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
    
    public partial class RequiredFieldsInCustomTranslation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomTranslations", "CodeName", c => c.String(nullable: false));
            AlterColumn("dbo.CustomTranslations", "Language", c => c.String(nullable: false));
            AlterColumn("dbo.CustomTranslations", "Value", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomTranslations", "Value", c => c.String());
            AlterColumn("dbo.CustomTranslations", "Language", c => c.String());
            AlterColumn("dbo.CustomTranslations", "CodeName", c => c.String());
        }
    }
}
