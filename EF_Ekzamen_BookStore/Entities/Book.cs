using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Ekzamen_BookStore.Entities
{
	internal class Book
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int NumberOfPages { get; set; }
		public DateTime Year { get; set; }
		public decimal CostPrice { get; set; }
		public decimal Price { get; set; }
		public string SeriesOfWorks { get; set; }
		public int CountOnStoke { get; set; }
		public int GenreId { get; set; }
		public int SubjectMatterId { get; set; }
		public int AutorId { get; set; }
		public int PublishingHouseId { get; set; }
		public int? StockId { get; set; }

		public Genre Genre { get; set; }
		public SubjectMatter SubjectMatter { get; set; }
		public Author Author { get; set; }
		public PublishingHouse PublishingHouse { get; set; }
		public Stoke Stoke { get; set; }

	}
}
