using System.Collections.Generic;

namespace EF_Ekzamen_BookStore.Entities
{
	public class PublishingHouse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string CIty { get; set; }
		public string Phone { get; set; }

		public ICollection<Book> Books { get; set; }
	}
}