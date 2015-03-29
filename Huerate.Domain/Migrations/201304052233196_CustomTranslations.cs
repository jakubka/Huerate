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
    
    public partial class CustomTranslations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeName = c.String(),
                        Culture = c.String(),
                        Value = c.String(),
                        RestaurantAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RestaurantAccounts", t => t.RestaurantAccountId)
                .Index(t => t.RestaurantAccountId);
            
            AddColumn("dbo.RestaurantAccounts", "Cultures", c => c.String());
        }
        
        public override void Down()
        {
            DropIndex("dbo.CustomTranslations", new[] { "RestaurantAccountId" });
            DropForeignKey("dbo.CustomTranslations", "RestaurantAccountId", "dbo.RestaurantAccounts");
            DropColumn("dbo.RestaurantAccounts", "Cultures");
            DropTable("dbo.CustomTranslations");
        }
    }
}
