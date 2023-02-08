using System;

namespace EF_Ekzamen_BookStore.Entities
{
	public class Sale
	{
		public int Id { get; set; }
		public string BookName { get; set; }
		public string AuthorName { get; set; }
		public string GenreName { get; set; }
		public DateTime SaleDate { get; set; }
		public int Count { get; set; }
	}
}