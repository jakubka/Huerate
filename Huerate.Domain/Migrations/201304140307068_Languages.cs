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
    
    public partial class Languages : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomTranslations", "RestaurantAccountId", c => c.Int());
            RenameColumn("dbo.RestaurantAccounts", "DefaultCulture", "DefaultLanguage");
            RenameColumn("dbo.RestaurantAccounts", "Cultures", "Languages");
            RenameColumn("dbo.EmailTemplates", "Culture", "Language");
            RenameColumn("dbo.CustomTranslations", "Culture", "Language");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomTranslations", "RestaurantAccountId", c => c.Int(nullable: false));
            RenameColumn("dbo.RestaurantAccounts", "DefaultLanguage", "DefaultCulture");
            RenameColumn("dbo.RestaurantAccounts", "Languages", "Cultures");
            RenameColumn("dbo.EmailTemplates", "Language", "Culture");
            RenameColumn("dbo.CustomTranslations", "Language", "Culture");
        }
    }
}
