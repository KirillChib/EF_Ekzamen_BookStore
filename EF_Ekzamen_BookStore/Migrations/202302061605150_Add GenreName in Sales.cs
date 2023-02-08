namespace EF_Ekzamen_BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreNameinSales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "GenreName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "GenreName");
        }
    }
}
