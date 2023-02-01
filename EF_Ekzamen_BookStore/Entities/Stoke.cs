using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Ekzamen_BookStore.Entities
{
	internal class Stoke
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Size{ get; set; }
		public DateTime StratDate { get; set; }
		public DateTime EndDate { get; set; }
		public int? GenreId { get; set; }
		public int? SubjectMatterId { get; set; }
		public int? AuthorId { get; set; }

		public Genre Genre { get; set; }
		public SubjectMatter SubjectMatter { get; set; }
		public Author Author { get; set; }
		
		public ICollection<Book> Books { get; set; }
	}
}
