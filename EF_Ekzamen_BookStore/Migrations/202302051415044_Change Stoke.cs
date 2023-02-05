namespace EF_Ekzamen_BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStoke : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Stokes", name: "AuthorId", newName: "Author_Id");
            RenameIndex(table: "dbo.Stokes", name: "IX_AuthorId", newName: "IX_Author_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Stokes", name: "IX_Author_Id", newName: "IX_AuthorId");
            RenameColumn(table: "dbo.Stokes", name: "Author_Id", newName: "AuthorId");
        }
    }
}
