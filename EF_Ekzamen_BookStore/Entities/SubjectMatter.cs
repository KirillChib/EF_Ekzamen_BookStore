using System.Collections.Generic;

namespace EF_Ekzamen_BookStore.Entities
{
	internal class SubjectMatter
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Book> Books { get; set; }
		public ICollection<Stoke> Stokes { get; set; }
	}
}