using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Ekzamen_BookStore.Entities
{
	internal class Booking
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public DateTime Date { get; set; }
		public int Days { get; set; }
		public int BookId { get; set; }
		public int Count { get; set; }

		public Book Book { get; set; }
	}
}
