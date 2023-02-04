namespace EF_Ekzamen_BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddpropertyinBookingDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "Date");
        }
    }
}
