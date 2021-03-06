namespace Huerate.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValueNotRequiredInCustomTranslation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomTranslations", "Value", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomTranslations", "Value", c => c.String(nullable: false));
        }
    }
}
