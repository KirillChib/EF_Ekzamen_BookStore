namespace EF_Ekzamen_BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 20),
                        Surname = c.String(nullable: false, maxLength: 20),
                        Patronymic = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NumberOfPages = c.Int(nullable: false),
                        Year = c.DateTime(nullable: false, storeType: "date"),
                        CostPrice = c.Decimal(nullable: false, storeType: "money"),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        SeriesOfWorks = c.String(maxLength: 50),
                        CountOnStoke = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        SubjectMatterId = c.Int(nullable: false),
                        AutorId = c.Int(nullable: false),
                        PublishingHouseId = c.Int(nullable: false),
                        StockId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AutorId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.PublishingHouses", t => t.PublishingHouseId, cascadeDelete: true)
                .ForeignKey("dbo.Stokes", t => t.StockId)
                .ForeignKey("dbo.SubjectMatters", t => t.SubjectMatterId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.GenreId)
                .Index(t => t.SubjectMatterId)
                .Index(t => t.AutorId)
                .Index(t => t.PublishingHouseId)
                .Index(t => t.StockId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Stokes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Size = c.Double(nullable: false),
                        StratDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        GenreId = c.Int(),
                        SubjectMatterId = c.Int(),
                        AuthorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.SubjectMatters", t => t.SubjectMatterId)
                .Index(t => t.GenreId)
                .Index(t => t.SubjectMatterId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.SubjectMatters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublishingHouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CIty = c.String(nullable: false),
                        Phone = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookName = c.String(),
                        AuthorName = c.String(),
                        SaleDate = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "SubjectMatterId", "dbo.SubjectMatters");
            DropForeignKey("dbo.Books", "StockId", "dbo.Stokes");
            DropForeignKey("dbo.Books", "PublishingHouseId", "dbo.PublishingHouses");
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Stokes", "SubjectMatterId", "dbo.SubjectMatters");
            DropForeignKey("dbo.Stokes", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Stokes", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Books", "AutorId", "dbo.Authors");
            DropIndex("dbo.Stokes", new[] { "AuthorId" });
            DropIndex("dbo.Stokes", new[] { "SubjectMatterId" });
            DropIndex("dbo.Stokes", new[] { "GenreId" });
            DropIndex("dbo.Genres", new[] { "Name" });
            DropIndex("dbo.Books", new[] { "StockId" });
            DropIndex("dbo.Books", new[] { "PublishingHouseId" });
            DropIndex("dbo.Books", new[] { "AutorId" });
            DropIndex("dbo.Books", new[] { "SubjectMatterId" });
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropIndex("dbo.Books", new[] { "Name" });
            DropTable("dbo.Sales");
            DropTable("dbo.PublishingHouses");
            DropTable("dbo.SubjectMatters");
            DropTable("dbo.Stokes");
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
