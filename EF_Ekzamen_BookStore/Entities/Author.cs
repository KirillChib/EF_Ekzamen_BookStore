using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Ekzamen_BookStore.Entities
{
	internal class Author
	{
		public int Id { get; set; }
		public string Firstname { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }

		public ICollection<Book> Books { get; set; }
		public ICollection<Stoke> Stokes { get; set; }
	}
}
