namespace EF_Ekzamen_BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangetypePhoneonstring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PublishingHouses", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PublishingHouses", "Phone", c => c.Int(nullable: false));
        }
    }
}
