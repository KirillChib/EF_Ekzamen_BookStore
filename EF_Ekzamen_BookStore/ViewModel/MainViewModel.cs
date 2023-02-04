using EF_Ekzamen_BookStore.Command;
using EF_Ekzamen_BookStore.Context;
using EF_Ekzamen_BookStore.Entities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EF_Ekzamen_BookStore.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private BookStoreContext context;

		//Для отображения таблиц в DataGrid
		private ObservableCollection<Author> dbAuthors = new ObservableCollection<Author>();
		private ObservableCollection<Book> dbBooksForUpdate = new ObservableCollection<Book>();
		private ObservableCollection<Book> dbBooks = new ObservableCollection<Book>();
		private ObservableCollection<Book> dbBooksFOrSale = new ObservableCollection<Book>();
		private ObservableCollection<Booking> dbBookings = new ObservableCollection<Booking>();
		private ObservableCollection<Genre> dbGenres = new ObservableCollection<Genre>();
		private ObservableCollection<PublishingHouse> dbPublishingHouses = new ObservableCollection<PublishingHouse>();
		private ObservableCollection<Sale> dbSales = new ObservableCollection<Sale>();
		private ObservableCollection<Stoke> dbStokes = new ObservableCollection<Stoke>();
		private ObservableCollection<SubjectMatter> dbSubjectMatters = new ObservableCollection<SubjectMatter>();
		//---------------------------------------------------------------------

		//Поля для вкладки "Поиск и продажа"
		private string selectedParametrForSearchSale;

		private string stringForSearchSale;
		private string stringCountForSale;
		private Book selectedBookForSale;
		private string saleSum;
		private string fullNameForBooking;
		private string stringCountForBooking;
		private string bookingDays;
		//-------------------------------------------------

		//Поля для добавления книг
		private string bookNameForInsert;

		private string bookCountPagesForInsert;
		private DateTime dateReleaseForInsert;
		private string bookCostPriceForInsert;
		private string bookPrice;
		private string bookSeriesOfWorksForInsert;
		private string bookCountForInsert;
		private string bookGenreIdForInsert;
		private string bookSubjectMatterIdForInsert;
		private string bookAuthorIdForInsert;
		private string bookPublishingHouseIdForInsert;

		private ICommand bookAdd;
		//------------------------------------

		//Поля для вкладки "Редактирование"
		private string bookNameSearch;
		private string bookNameForSearchUpdate;
		private string bookCountPagesForISearchUpdate;
		private DateTime dateReleaseForSearchUpdate;
		private string bookCostPriceForSearchUpdate;
		private string bookPriceForSearchUpdate;
		private string bookSeriesOfWorksForSearchUpdate;
		private string bookCountForSearchUpdate;
		private string bookGenreIdForSearchUpdate;
		private string bookSubjectMatterIdForSearchUpdate;
		private string bookAuthorIdForSearchUpdate;
		private string bookPublishingHouseIdForSearchUpdate;
		private Book selectedBook = new Book();

		private ICommand searchForUpdateCommand;
		private ICommand updateCommand;
		//---------------------------------------------

		//Команды для вкладки "Поиск и продажа"
		private ICommand searchForSaleCommand;

		private ICommand calculateSumCommand;
		private ICommand saleBookCommand;
		private ICommand bookingCommand;
		//-----------------------------------------------------

		//Для отображения таблиц в DataGrid
		internal ObservableCollection<Author> DbAuthors { get => dbAuthors; set => Set(ref dbAuthors, value); }
		internal ObservableCollection<Book> DbBooksForUpdate { get => dbBooksForUpdate; set =>Set(ref dbBooksForUpdate,value); }
		internal ObservableCollection<Book> DbBooksFOrSale { get => dbBooksFOrSale; set => Set(ref dbBooksFOrSale, value); }
		internal ObservableCollection<Booking> DbBookings { get => dbBookings; set => Set(ref dbBookings, value); }
		internal ObservableCollection<Genre> DbGenres { get => dbGenres; set => Set(ref dbGenres, value); }
		internal ObservableCollection<PublishingHouse> DbPublishingHouses { get => dbPublishingHouses; set => Set(ref dbPublishingHouses, value); }
		internal ObservableCollection<Sale> DbSales { get => dbSales; set => Set(ref dbSales, value); }
		internal ObservableCollection<Stoke> DbStokes { get => dbStokes; set => Set(ref dbStokes, value); }
		internal ObservableCollection<SubjectMatter> DbSubjectMatters { get => dbSubjectMatters; set => Set(ref dbSubjectMatters, value); }
		//----------------------------------------------

		//Cвойства и команды вкладка "Поиск и продажа"
		public string[] ParametrFoSearchSale { get; set; } =
		{
			"Название книги","Фамилия автора","Жанр"
		};

		public string StringCountForSale { get => stringCountForSale; set => Set(ref stringCountForSale, value); }
		public string StringForSearchSale { get => stringForSearchSale; set => Set(ref stringForSearchSale, value); }
		public string SelectedParametrForSearchSale { get => selectedParametrForSearchSale; set => Set(ref selectedParametrForSearchSale, value); }
		internal Book SelectedBookForSale { get => selectedBookForSale; set => Set(ref selectedBookForSale, value); }
		public string SaleSum { get => saleSum; set => Set(ref saleSum, value); }
		public string FullNameForBooking { get => fullNameForBooking; set => Set(ref fullNameForBooking, value); }
		public string StringCountForBooking { get => stringCountForBooking; set => Set(ref stringCountForBooking, value); }
		public string BookingDays { get => bookingDays; set => Set(ref bookingDays, value); }

		public ICommand SearchForSaleCommand => searchForSaleCommand ??= new AsyncRelayCommand(SearchForSaleOrBookingAsync);
		public ICommand CalculateSumCommand => calculateSumCommand ??= new RelayCommand(CalculateSum);
		public ICommand SaleBookCommand => saleBookCommand ??= new AsyncRelayCommand(BookSaleAsync);
		public ICommand BookingCommand => bookingCommand ??= new AsyncRelayCommand(BookingAsync);
		//--------------------------------------------------------------------

		//Свойства и команды для вкладки "Добавление книг"
		public string BookNameForInsert { get => bookNameForInsert; set => Set(ref bookNameForInsert, value); }

		internal ObservableCollection<Book> DbBooks { get => dbBooks; set => Set(ref dbBooks, value); }
		public string BookCountPagesForInsert { get => bookCountPagesForInsert; set => Set(ref bookCountPagesForInsert, value); }
		public DateTime DateReleaseForInsert { get => dateReleaseForInsert; set => Set(ref dateReleaseForInsert, value); }
		public string BookCostPriceForInsert { get => bookCostPriceForInsert; set => Set(ref bookCostPriceForInsert, value); }
		public string BookPrice { get => bookPrice; set => Set(ref bookPrice, value); }
		public string BookSeriesOfWorksForInsert { get => bookSeriesOfWorksForInsert; set => Set(ref bookSeriesOfWorksForInsert, value); }
		public string BookCountForInsert { get => bookCountForInsert; set => Set(ref bookCountForInsert, value); }
		public string BookGenreIdForInsert { get => bookGenreIdForInsert; set => Set(ref bookGenreIdForInsert, value); }
		public string BookSubjectMatterIdForInsert { get => bookSubjectMatterIdForInsert; set => Set(ref bookSubjectMatterIdForInsert, value); }
		public string BookAuthorIdForInsert { get => bookAuthorIdForInsert; set => Set(ref bookAuthorIdForInsert, value); }
		public string BookPublishingHouseIdForInsert { get => bookPublishingHouseIdForInsert; set => Set(ref bookPublishingHouseIdForInsert, value); }

		internal ICollection<Genre> GenreNames { get; set; }
		internal ICollection<Author> AuthorNames { get; set; }
		internal ICollection<PublishingHouse> PublishingHouseNames { get; set; }
		internal ICollection<SubjectMatter> SubjectMatterNames { get; set; }

		public ICommand BookAdd => bookAdd ??= new AsyncRelayCommand(BookInsertAsync);
		//-----------------------------------------------------------------------------------

		//Свойства и команды для вкладки "Редактирование"
		public string BookNameForSearchUpdate { get => bookNameForSearchUpdate; set =>Set(ref bookNameForSearchUpdate,value); }
		public string BookCountPagesForISearchUpdate { get => bookCountPagesForISearchUpdate; set => Set(ref bookCountPagesForISearchUpdate,value); }
		public DateTime DateReleaseForSearchUpdate { get => dateReleaseForSearchUpdate; set => Set(ref dateReleaseForSearchUpdate,value); }
		public string BookCostPriceForSearchUpdate { get => bookCostPriceForSearchUpdate; set => Set(ref bookCountForSearchUpdate,value); }
		public string BookPriceForSearchUpdate { get => bookPriceForSearchUpdate; set => Set(ref bookPriceForSearchUpdate,value); }
		public string BookSeriesOfWorksForSearchUpdate { get => bookSeriesOfWorksForSearchUpdate; set => Set(ref bookSeriesOfWorksForSearchUpdate,value); }
		public string BookCountForSearchUpdate { get => bookCountForSearchUpdate; set =>Set(ref bookCountForSearchUpdate,value); }
		public string BookGenreIdForSearchUpdate { get => bookGenreIdForSearchUpdate; set => Set(ref bookGenreIdForSearchUpdate,value); }
		public string BookSubjectMatterIdForSearchUpdate { get => bookSubjectMatterIdForSearchUpdate; set =>Set(ref bookSubjectMatterIdForSearchUpdate); }
		public string BookAuthorIdForSearchUpdate { get => bookAuthorIdForSearchUpdate; set => Set(ref bookAuthorIdForSearchUpdate,value); }
		public string BookPublishingHouseIdForSearchUpdate { get => bookPublishingHouseIdForSearchUpdate; set =>Set(ref bookPublishingHouseIdForSearchUpdate,value); }
		internal Book SelectedBook { get => selectedBook; set =>Set(ref selectedBook,value); }
		public string BookNameSearch { get => bookNameSearch; set => Set(ref bookNameSearch,value); }

		public ICommand SearchForUpdateCommand => searchForUpdateCommand ??= new AsyncRelayCommand(BookSearchForUpdate);
		public ICommand UpdateCommand => updateCommand ??= new AsyncRelayCommand(BookUpdateAsync);

		//---------------------------------------------------------------------------------------------

		public MainViewModel()
		{
			context = new BookStoreContext();

			GenreNames = GetGenres();
			AuthorNames = GetAuthors();
			PublishingHouseNames = GetPublishingHouses();
			SubjectMatterNames = GetSubjectMatters();
		}

		//Получаем Авторов
		private ObservableCollection<Author> GetAuthors()
		{
			var list = context.Authors;
			return new ObservableCollection<Author>(list);
		}

		//Получаем Книги
		private ObservableCollection<Book> GetBooks()
		{
			var list = context.Books;
			return new ObservableCollection<Book>(list);
		}

		//Получаем Брони
		private ObservableCollection<Booking> GetBookings()
		{
			var list = context.Bookings;
			return new ObservableCollection<Booking>(list);
		}

		//Получаем Жанры
		private ObservableCollection<Genre> GetGenres()
		{
			var list = context.Genres;
			return new ObservableCollection<Genre>(list);
		}

		//Получаем Издательства
		private ObservableCollection<PublishingHouse> GetPublishingHouses()
		{
			var list = context.PublishingHouses;
			return new ObservableCollection<PublishingHouse>(list);
		}

		//Получаем Продажи
		private ObservableCollection<Sale> GetSales()
		{
			var list = context.Sales;
			return new ObservableCollection<Sale>(list);
		}

		//Получаем Акции
		private ObservableCollection<Stoke> GetStokes()
		{
			var list = context.Stokes;
			return new ObservableCollection<Stoke>(list);
		}

		//Получаем Тематики
		private ObservableCollection<SubjectMatter> GetSubjectMatters()
		{
			var list = context.SubjectMatters;
			return new ObservableCollection<SubjectMatter>(list);
		}

		//Поиск для продажи или брони
		private async Task SearchForSaleOrBookingAsync()
		{
			if (string.IsNullOrEmpty(SelectedParametrForSearchSale) || string.IsNullOrEmpty(StringForSearchSale))
				return;

			switch (SelectedParametrForSearchSale)
			{
				case "Название книги":
					var list = await context.Books.Where(b => b.Name == StringForSearchSale).ToListAsync();
					DbBooksFOrSale = new ObservableCollection<Book>(list);

					break;

				case "Фамилия автора":
					var list1 = await context.Books.Where(b => b.Author.Surname == StringForSearchSale).ToListAsync();
					DbBooksFOrSale = new ObservableCollection<Book>(list1);

					break;

				case "Жанр":
					var list2 = await context.Books.Where(b => b.Genre.Name == StringForSearchSale).ToListAsync();
					DbBooksFOrSale = new ObservableCollection<Book>(list2);

					break;
			}
		}

		//Рассчет суммы покупки
		private void CalculateSum()
		{
			int count;
			decimal saleSum;

			if (string.IsNullOrEmpty(StringCountForSale))
				return;
			else if (!int.TryParse(StringCountForSale, out count))
				return;

			count = int.Parse(StringCountForSale);

			if (count > SelectedBookForSale.CountOnStoke)
				return;

			if (SelectedBookForSale.StockId != null)
			{
				saleSum = (decimal)(SelectedBookForSale.Stoke.Size / 100) * (count * SelectedBookForSale.Price);

				SaleSum = saleSum.ToString();

				return;
			}
			else
			{
				saleSum = count * SelectedBookForSale.Price;

				SaleSum = saleSum.ToString();

				return;
			}
		}

		//Продажа книги
		private async Task BookSaleAsync()
		{
			int count;
			decimal saleSum;

			if (string.IsNullOrEmpty(StringCountForSale))
				return;
			else if (!int.TryParse(StringCountForSale, out count))
				return;

			count = int.Parse(StringCountForSale);

			if (count > SelectedBookForSale.CountOnStoke)
				return;

			if (SelectedBookForSale.StockId != null)
			{
				saleSum = (decimal)(SelectedBookForSale.Stoke.Size / 100) * (count * SelectedBookForSale.Price);
			}
			else
			{
				saleSum = count * SelectedBookForSale.Price;
			}

			var sale = new Sale()
			{
				BookName = SelectedBookForSale.Name,
				AuthorName = $"{SelectedBookForSale.Author.Firstname} {SelectedBookForSale.Author.Surname} {SelectedBookForSale.Author.Patronymic}",
				SaleDate = DateTime.Now,
				Count = count
			};

			context.Sales.Add(sale);
			SelectedBookForSale.CountOnStoke -= count;

			await context.SaveChangesAsync();

			SaleSum = "";
			StringCountForSale = "";
		}

		//Бронирование книги
		private async Task BookingAsync()
		{
			int count;
			int days;

			if (string.IsNullOrEmpty(FullNameForBooking))
				return;
			else if (!int.TryParse(StringCountForBooking, out count))
				return;
			else if (!int.TryParse(BookingDays, out days))
				return;

			count = int.Parse(StringCountForSale);
			days = int.Parse(BookingDays);

			if (count > SelectedBookForSale.CountOnStoke)
				return;

			var booking = new Booking()
			{
				FullName = FullNameForBooking,
				Date = DateTime.Now,
				Days = days,
				Count = count,
				BookId = SelectedBookForSale.Id
			};

			SelectedBookForSale.Booking = true;
			context.Bookings.Add(booking);

			await context.SaveChangesAsync();

			FullNameForBooking = "";
			StringCountForBooking = "";
			BookingDays = "";
		}

		//Добавленеи книги
		private async Task BookInsertAsync()
		{
			int pages;
			int count;
			decimal cost;
			decimal price;

			if (!int.TryParse(BookCountPagesForInsert, out pages))
				return;
			else if (!int.TryParse(BookCountForInsert, out count))
				return;
			else if (!decimal.TryParse(BookCostPriceForInsert, out cost))
				return;
			else if (!decimal.TryParse(BookPrice, out price))
				return;
			else if (string.IsNullOrEmpty(BookNameForInsert) || string.IsNullOrEmpty(BookSeriesOfWorksForInsert))
				return;

			var genreId = await context.Genres.Where(g => g.Name == BookGenreIdForInsert).Select(g => g.Id).ToArrayAsync();
			var authorId = await context.Authors.Where(a => a.Surname == BookAuthorIdForInsert).Select(a => a.Id).ToArrayAsync();
			var publish = await context.PublishingHouses.Where(p => p.Name == BookPublishingHouseIdForInsert).Select(p => p.Id).ToArrayAsync();
			var subject = await context.SubjectMatters.Where(s => s.Name == BookSubjectMatterIdForInsert).Select(s => s.Id).ToArrayAsync();

			var book = new Book()
			{
				Name = BookNameForInsert,
				NumberOfPages = pages,
				Year = DateReleaseForInsert,
				CostPrice = cost,
				Price = price,
				SeriesOfWorks = BookSeriesOfWorksForInsert,
				CountOnStoke = count,
				Booking = false,
				GenreId = genreId[0],
				AutorId = authorId[0],
				PublishingHouseId = publish[0],
				SubjectMatterId = subject[0]
			};

			context.Books.Add(book);
			await context.SaveChangesAsync();

			BookCountPagesForInsert = "";
			BookCostPriceForInsert = "";
			BookPrice = "";
			BookSeriesOfWorksForInsert = "";
			BookCountForInsert = "";																		
			
		}

		//Поиск для редактирования
		private async Task BookSearchForUpdate()
		{
			if (string.IsNullOrEmpty(BookNameSearch))
				return;

			var books = await context.Books.Where(b => b.Name == BookNameSearch).ToListAsync();

			DbBooksForUpdate = new ObservableCollection<Book>(books);
		}

		//Редактирование книги
		private async Task BookUpdateAsync()
		{
			int pages;
			int count;
			decimal cost;
			decimal price;

			if (!int.TryParse(BookCountPagesForISearchUpdate, out pages))
				return;
			else if (!int.TryParse(BookCountForSearchUpdate, out count))
				return;
			else if (!decimal.TryParse(BookCostPriceForSearchUpdate, out cost))
				return;
			else if (!decimal.TryParse(BookPriceForSearchUpdate, out price))
				return;
			else if (string.IsNullOrEmpty(BookNameForSearchUpdate) || string.IsNullOrEmpty(BookSeriesOfWorksForSearchUpdate))
				return;

			var genreId = await context.Genres.Where(g => g.Name == BookGenreIdForSearchUpdate).Select(g => g.Id).ToArrayAsync();
			var authorId = await context.Authors.Where(a => a.Surname == BookAuthorIdForSearchUpdate).Select(a => a.Id).ToArrayAsync();
			var publish = await context.PublishingHouses.Where(p => p.Name == BookPublishingHouseIdForSearchUpdate).Select(p => p.Id).ToArrayAsync();
			var subject = await context.SubjectMatters.Where(s => s.Name == BookSubjectMatterIdForSearchUpdate).Select(s => s.Id).ToArrayAsync();


			SelectedBook.Name = BookNameForSearchUpdate;
			SelectedBook.NumberOfPages = pages;
			SelectedBook.Year = DateReleaseForSearchUpdate;
			SelectedBook.CostPrice = cost;
			SelectedBook.Price = price;
			SelectedBook.SeriesOfWorks = BookSeriesOfWorksForSearchUpdate;
			SelectedBook.CountOnStoke = count;
			SelectedBook.GenreId = genreId[0];
			SelectedBook.AutorId = authorId[0];
			SelectedBook.PublishingHouseId = publish[0];
			SelectedBook.SubjectMatterId = subject[0];

			await context.SaveChangesAsync();
			
		}
	}
}