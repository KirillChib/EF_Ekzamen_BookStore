using EF_Ekzamen_BookStore.Entities;
using System.Data.Entity;

namespace EF_Ekzamen_BookStore.Context
{
	internal class BookStoreContext : DbContext
	{
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<PublishingHouse> PublishingHouses { get; set; }
		public DbSet<SubjectMatter> SubjectMatters { get; set; }
		public DbSet<Stoke> Stokes { get; set; }
		public DbSet<Sale> Sales { get; set; }

		public BookStoreContext() : base("DbConnection")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//Author
			#region
			modelBuilder.Entity<Author>()
										.Property(a => a.Firstname)
										.HasMaxLength(20)
										.IsRequired();

			modelBuilder.Entity<Author>()
										.Property(a => a.Surname)
										.HasMaxLength(20)
										.IsRequired();

			modelBuilder.Entity<Author>()
										.Property(a => a.Patronymic)
										.HasMaxLength(20)
										.IsRequired();
			#endregion

			//Book
			#region
			modelBuilder.Entity<Book>()
										.Property(b => b.Name)
										.HasMaxLength(50)
										.IsRequired();

			modelBuilder.Entity<Book>()
										.HasIndex(b => b.Name)
										.IsUnique();

			modelBuilder.Entity<Book>()
										.Property(b => b.Year)
										.HasColumnType("date");

			modelBuilder.Entity<Book>()
										.Property(b => b.CostPrice)
										.HasColumnType("money");

			modelBuilder.Entity<Book>()
										.Property(b => b.Price)
										.HasColumnType("money");

			modelBuilder.Entity<Book>()
										.Property(b => b.SeriesOfWorks)
										.HasMaxLength(50);

			modelBuilder.Entity<Book>()
										.HasRequired(b => b.Genre)
										.WithMany(g => g.Books)
										.HasForeignKey(b => b.GenreId);

			modelBuilder.Entity<Book>()
										.HasRequired(b => b.SubjectMatter)
										.WithMany(s => s.Books)
										.HasForeignKey(b => b.SubjectMatterId);

			modelBuilder.Entity<Book>()
										.HasRequired(b => b.Author)
										.WithMany(a => a.Books)
										.HasForeignKey(b => b.AutorId);

			modelBuilder.Entity<Book>()
										.HasRequired(b => b.PublishingHouse)
										.WithMany(p => p.Books)
										.HasForeignKey(b => b.PublishingHouseId);

			modelBuilder.Entity<Book>()
										.HasOptional(b => b.Stoke)
										.WithMany(s => s.Books)
										.HasForeignKey(b => b.StockId);

			#endregion

			//Genre
			#region
			modelBuilder.Entity<Genre>()
										.Property(g => g.Name)
										.HasMaxLength(30)
										.IsRequired();

			modelBuilder.Entity<Genre>()
										.HasIndex(g => g.Name)
										.IsUnique();
			#endregion

			//PublishingHouse
			#region
			modelBuilder.Entity<PublishingHouse>()
										.Property(p => p.Name)
										.IsRequired();

			modelBuilder.Entity<PublishingHouse>()
										.Property(p => p.CIty)
										.IsRequired();
			#endregion

			//SubjectMatter
			#region
			modelBuilder.Entity<SubjectMatter>()
										.Property(s => s.Name)
										.HasMaxLength(100)
										.IsRequired();
			#endregion

			//Stoke
			#region
			modelBuilder.Entity<Stoke>()
										.Property(s => s.Name)
										.HasMaxLength(100)
										.IsRequired();

			modelBuilder.Entity<Stoke>()
										.HasOptional(s => s.Genre)
										.WithMany(g => g.Stokes)
										.HasForeignKey(s => s.GenreId);

			modelBuilder.Entity<Stoke>()
										.HasOptional(s => s.SubjectMatter)
										.WithMany(sub => sub.Stokes)
										.HasForeignKey(s => s.SubjectMatterId);

			modelBuilder.Entity<Stoke>()
										.HasOptional(s => s.Author)
										.WithMany(a => a.Stokes)
										.HasForeignKey(s => s.AuthorId);
			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}