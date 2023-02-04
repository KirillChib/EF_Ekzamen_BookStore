namespace EF_Ekzamen_BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangepropertyFullNametableBookingonrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "FullName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "FullName", c => c.String());
        }
    }
}
