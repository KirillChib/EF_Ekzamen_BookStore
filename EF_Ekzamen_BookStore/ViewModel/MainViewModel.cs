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
		private ObservableCollection<Sale> dbSalesStatistic = new ObservableCollection<Sale>();
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
		private Genre bookGenreIdForInsert;
		private SubjectMatter bookSubjectMatterIdForInsert;
		private Author bookAuthorIdForInsert;
		private PublishingHouse bookPublishingHouseIdForInsert;

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
		private Genre bookGenreIdForSearchUpdate;
		private SubjectMatter bookSubjectMatterIdForSearchUpdate;
		private Author bookAuthorIdForSearchUpdate;
		private PublishingHouse bookPublishingHouseIdForSearchUpdate;
		private Book selectedBook = new Book();

		private ICommand searchForUpdateCommand;
		private ICommand updateCommand;
		private ICommand removeCommand;
		//---------------------------------------------

		// Поля для вкладки "Акции"
		private string stokeName;

		private string stokeSize;
		private DateTime stokeStratDate;
		private DateTime stokeEndDate;
		private Genre stokeGenreId;
		private SubjectMatter stokeSubjectMatterId;
		private Author stokeAuthorId;
		private Stoke selectedStoke;

		private ICommand addStokeCommand;
		private ICommand removeStokeCommand;
		//-----------------------------------

		//Поля для вкладки "Брони"
		private string bookingFullName;

		private Booking selectedBooking;

		private ICommand searchBookingCommand;
		private ICommand activeBookingCommand;
		private ICommand revoveBookingCommand;
		//-----------------------------------

		//Поля для вкладки Продажи
		private ICommand showSalesByDayCommand;

		private ICommand showSalesByWeekCommand;
		private ICommand showSalesByMonthCommand;
		private ICommand showSalesByYearCommand;
		private ICommand showSalesByAllTimeCommand;

		//--------------------------------------

		//Поля для вкладки "Авторы"
		private string authorFirstName;

		private string autthorSurname;
		private string authirPatronymic;
		private Author selectedAuthor;

		private ICommand addAuthorCommand;
		private ICommand removeAuthorCommand;

		//----------------------------------------

		//Поля для вкладки "Жанры"
		private string genreName;

		private Genre selectedGenre;

		private ICommand addGenreCommand;
		private ICommand removeGenreCommand;
		//-------------------------------------

		//Поля для вкладки "Тематики"
		private string subjectMatterName;

		private SubjectMatter selectedSubjectMatter;

		private ICommand addSubjectMatterCommand;
		private ICommand removeSubjectMatterCommand;
		//-------------------------------------

		//Поля для вкладки "Издательства"
		private string publishingHouseName;

		private string publishingHouseCity;
		private string publishingHousePhone;
		private PublishingHouse selectedPublishingHouse;

		private ICommand addPublishingHouseCommand;
		private ICommand removePublishingHouseCommand;

		//----------------------------------------

		//Поля для вкладки "Статистика"
		//private ICommand showNewBooksCommand;
		//private ICommand showTopBooks;
		//private ICommand showTopAuthor
		//private ICommand
		//----------------------------------------------

		//Команды для вкладки "Поиск и продажа"
		private ICommand searchForSaleCommand;

		private ICommand calculateSumCommand;
		private ICommand saleBookCommand;
		private ICommand bookingCommand;
		//-----------------------------------------------------

		//Для отображения таблиц в DataGrid
		public ObservableCollection<Author> DbAuthors { get => dbAuthors; set => Set(ref dbAuthors, value); }

		public ObservableCollection<Book> DbBooksForUpdate { get => dbBooksForUpdate; set => Set(ref dbBooksForUpdate, value); }
		public ObservableCollection<Book> DbBooksFOrSale { get => dbBooksFOrSale; set => Set(ref dbBooksFOrSale, value); }
		public ObservableCollection<Booking> DbBookings { get => dbBookings; set => Set(ref dbBookings, value); }
		public ObservableCollection<Genre> DbGenres { get => dbGenres; set => Set(ref dbGenres, value); }
		public ObservableCollection<PublishingHouse> DbPublishingHouses { get => dbPublishingHouses; set => Set(ref dbPublishingHouses, value); }
		public ObservableCollection<Sale> DbSales { get => dbSales; set => Set(ref dbSales, value); }
		public ObservableCollection<Sale> DbSalesStatistic { get => dbSalesStatistic; set => Set(ref dbSalesStatistic, value); }
		public ObservableCollection<Stoke> DbStokes { get => dbStokes; set => Set(ref dbStokes, value); }
		public ObservableCollection<SubjectMatter> DbSubjectMatters { get => dbSubjectMatters; set => Set(ref dbSubjectMatters, value); }
		//----------------------------------------------

		//Cвойства и команды вкладка "Поиск и продажа"
		public string[] ParametrFoSearchSale { get; set; } =
		{
			"Название книги","Фамилия автора","Жанр"
		};

		public string StringCountForSale { get => stringCountForSale; set => Set(ref stringCountForSale, value); }
		public string StringForSearchSale { get => stringForSearchSale; set => Set(ref stringForSearchSale, value); }
		public string SelectedParametrForSearchSale { get => selectedParametrForSearchSale; set => Set(ref selectedParametrForSearchSale, value); }
		public Book SelectedBookForSale { get => selectedBookForSale; set => Set(ref selectedBookForSale, value); }
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

		public ObservableCollection<Book> DbBooks { get => dbBooks; set => Set(ref dbBooks, value); }
		public string BookCountPagesForInsert { get => bookCountPagesForInsert; set => Set(ref bookCountPagesForInsert, value); }
		public DateTime DateReleaseForInsert { get => dateReleaseForInsert; set => Set(ref dateReleaseForInsert, value); }
		public string BookCostPriceForInsert { get => bookCostPriceForInsert; set => Set(ref bookCostPriceForInsert, value); }
		public string BookPrice { get => bookPrice; set => Set(ref bookPrice, value); }
		public string BookSeriesOfWorksForInsert { get => bookSeriesOfWorksForInsert; set => Set(ref bookSeriesOfWorksForInsert, value); }
		public string BookCountForInsert { get => bookCountForInsert; set => Set(ref bookCountForInsert, value); }
		public Genre BookGenreIdForInsert { get => bookGenreIdForInsert; set => Set(ref bookGenreIdForInsert, value); }
		public SubjectMatter BookSubjectMatterIdForInsert { get => bookSubjectMatterIdForInsert; set => Set(ref bookSubjectMatterIdForInsert, value); }
		public Author BookAuthorIdForInsert { get => bookAuthorIdForInsert; set => Set(ref bookAuthorIdForInsert, value); }
		public PublishingHouse BookPublishingHouseIdForInsert { get => bookPublishingHouseIdForInsert; set => Set(ref bookPublishingHouseIdForInsert, value); }

		public ICollection<Genre> GenreNames { get; set; }
		public ICollection<Author> AuthorNames { get; set; }
		public ICollection<PublishingHouse> PublishingHouseNames { get; set; }
		public ICollection<SubjectMatter> SubjectMatterNames { get; set; }

		public ICommand BookAdd => bookAdd ??= new AsyncRelayCommand(BookInsertAsync);
		//-----------------------------------------------------------------------------------

		//Свойства и команды для вкладки "Редактирование"
		public string BookNameForSearchUpdate { get => bookNameForSearchUpdate; set => Set(ref bookNameForSearchUpdate, value); }

		public string BookCountPagesForISearchUpdate { get => bookCountPagesForISearchUpdate; set => Set(ref bookCountPagesForISearchUpdate, value); }
		public DateTime DateReleaseForSearchUpdate { get => dateReleaseForSearchUpdate; set => Set(ref dateReleaseForSearchUpdate, value); }
		public string BookCostPriceForSearchUpdate { get => bookCostPriceForSearchUpdate; set => Set(ref bookCostPriceForSearchUpdate, value); }
		public string BookPriceForSearchUpdate { get => bookPriceForSearchUpdate; set => Set(ref bookPriceForSearchUpdate, value); }
		public string BookSeriesOfWorksForSearchUpdate { get => bookSeriesOfWorksForSearchUpdate; set => Set(ref bookSeriesOfWorksForSearchUpdate, value); }
		public string BookCountForSearchUpdate { get => bookCountForSearchUpdate; set => Set(ref bookCountForSearchUpdate, value); }
		public Genre BookGenreIdForSearchUpdate { get => bookGenreIdForSearchUpdate; set => Set(ref bookGenreIdForSearchUpdate, value); }
		public SubjectMatter BookSubjectMatterIdForSearchUpdate { get => bookSubjectMatterIdForSearchUpdate; set => Set(ref bookSubjectMatterIdForSearchUpdate); }
		public Author BookAuthorIdForSearchUpdate { get => bookAuthorIdForSearchUpdate; set => Set(ref bookAuthorIdForSearchUpdate, value); }
		public PublishingHouse BookPublishingHouseIdForSearchUpdate { get => bookPublishingHouseIdForSearchUpdate; set => Set(ref bookPublishingHouseIdForSearchUpdate, value); }
		public Book SelectedBook { get => selectedBook; set => Set(ref selectedBook, value); }
		public string BookNameSearch { get => bookNameSearch; set => Set(ref bookNameSearch, value); }

		public ICommand SearchForUpdateCommand => searchForUpdateCommand ??= new AsyncRelayCommand(BookSearchForUpdate);
		public ICommand UpdateCommand => updateCommand ??= new AsyncRelayCommand(BookUpdateAsync);
		public ICommand RemoveCommand => removeCommand ??= new AsyncRelayCommand(BookRemove);
		//--------------------------------------------------------------------------

		//Свойства и команды для вкладки "Акции"
		public string StokeName { get => stokeName; set => Set(ref stokeName, value); }

		public string StokeSize { get => stokeSize; set => Set(ref stokeSize, value); }
		public DateTime StokeStratDate { get => stokeStratDate; set => Set(ref stokeStratDate); }
		public DateTime StokeEndDate { get => stokeEndDate; set => Set(ref stokeEndDate); }
		public Genre StokeGenreId { get => stokeGenreId; set => Set(ref stokeGenreId, value); }
		public SubjectMatter StokeSubjectMatterId { get => stokeSubjectMatterId; set => Set(ref stokeSubjectMatterId, value); }
		public Author StokeAuthorId { get => stokeAuthorId; set => Set(ref stokeAuthorId, value); }
		public Stoke SelectedStoke { get => selectedStoke; set => Set(ref selectedStoke, value); }

		public ICommand AddStokeCommand => addStokeCommand ??= new AsyncRelayCommand(StokeAdd);
		public ICommand RemoveStokeCommand => removeStokeCommand ??= new AsyncRelayCommand(StokeRemove);

		//---------------------------------------------------------------

		//Свойство для вкладки "Брони"
		public string BookingFullName { get => bookingFullName; set => Set(ref bookingFullName, value); }

		public Booking SelectedBooking { get => selectedBooking; set => Set(ref selectedBooking, value); }

		public ICommand SearchBookingCommand => searchBookingCommand ??= new AsyncRelayCommand(SearchBooking);
		public ICommand ActiveBookingCommand => activeBookingCommand ??= new AsyncRelayCommand(BookingActive);
		public ICommand RevoveBookingCommand => revoveBookingCommand ??= new AsyncRelayCommand(BookingRemove);
		//----------------------------------------

		//Команды для вкладки "Продажи"
		public ICommand ShowSalesByDayCommand => showSalesByDayCommand ??= new AsyncRelayCommand(ShowSalesByDay);

		public ICommand ShowSalesByWeekCommand => showSalesByWeekCommand ??= new AsyncRelayCommand(ShowSalesByWeek);
		public ICommand ShowSalesByMonthCommand => showSalesByMonthCommand ??= new AsyncRelayCommand(ShowSalesByMonth);
		public ICommand ShowSalesByYearCommand => showSalesByYearCommand ??= new AsyncRelayCommand(ShowSalesByYear);
		public ICommand ShowSalesByAllTimeCommand => showSalesByAllTimeCommand ??= new RelayCommand(ShowSalesBAllTime);
		//----------------------------------------------------

		//Свойства и команды для вкладки "Авторы"
		public string AuthorFirstName { get => authorFirstName; set => Set(ref authorFirstName, value); }

		public string AutthorSurname { get => autthorSurname; set => Set(ref autthorSurname, value); }
		public string AuthirPatronymic { get => authirPatronymic; set => Set(ref authirPatronymic, value); }
		public Author SelectedAuthor { get => selectedAuthor; set => Set(ref selectedAuthor, value); }

		public ICommand AddAuthorCommand => addAuthorCommand ??= new AsyncRelayCommand(AddAuthor);
		public ICommand RemoveAuthorCommand => removeAuthorCommand ??= new AsyncRelayCommand(RemoveAuthor);
		//-----------------------------------------------------

		//Свойства и команды для вкладки "Жанры"
		public string GenreName { get => genreName; set => Set(ref genreName, value); }

		public Genre SelectedGenre { get => selectedGenre; set => Set(ref selectedGenre, value); }

		public ICommand AddGenreCommand => addGenreCommand ??= new AsyncRelayCommand(AddGenre);
		public ICommand RemoveGenreCommand => removeGenreCommand ??= new AsyncRelayCommand(RemoveGenre);
		//----------------------------------------------------------

		//Свойства и команды вкладки "Тематики"
		public string SubjectMatterName { get => subjectMatterName; set => Set(ref subjectMatterName, value); }

		public SubjectMatter SelectedSubjectMatter { get => selectedSubjectMatter; set => Set(ref selectedSubjectMatter, value); }

		public ICommand AddSubjectMatterCommand => addSubjectMatterCommand ??= new AsyncRelayCommand(AddSubjectMatter);
		public ICommand RemoveSubjectMatterCommand => removeStokeCommand ??= new AsyncRelayCommand(RemoveSubjectMatter);
		//-----------------------------------------------------------

		//Свойства и команды для вкладки "Издательста"
		public string PublishingHouseName { get => publishingHouseName; set => Set(ref publishingHouseName, value); }

		public string PublishingHouseCity { get => publishingHouseCity; set => Set(ref publishingHouseCity, value); }
		public string PublishingHousePhone { get => publishingHousePhone; set => Set(ref publishingHousePhone, value); }
		public PublishingHouse SelectedPublishingHouse { get => selectedPublishingHouse; set => Set(ref selectedPublishingHouse, value); }

		public ICommand AddPublishingHouseCommand => addPublishingHouseCommand ??= new AsyncRelayCommand(AddPublishingHouse);
		public ICommand RemovePublishingHouseCommand => removePublishingHouseCommand ??= new AsyncRelayCommand(RemovePublishingHouse);

		//---------------------------------------------------------------------------------------------

		public MainViewModel()
		{
			context = new BookStoreContext();

			DbAuthors = GetAuthors();
			DbBooksForUpdate = GetBooks();
			DbBooksFOrSale = GetBooks();
			DbBookings = GetBookings();
			DbGenres = GetGenres();
			DbPublishingHouses = GetPublishingHouses();
			DbSales = GetSales();
			DbStokes = GetStokes();
			DbSubjectMatters = GetSubjectMatters();

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

			SelectedBookForSale.CountOnStoke -= count;
			context.Sales.Add(sale);

			await context.SaveChangesAsync();

			DbBooks = GetBooks();
			DbSales = GetSales();

			SaleSum = "";
			StringCountForSale = "";

			DbBooksFOrSale = GetBooks();
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

			DbBooks = GetBooks();
			DbBookings = GetBookings();

			FullNameForBooking = "";
			StringCountForBooking = "";
			BookingDays = "";

			DbBooksFOrSale = GetBooks();
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

			//var genreId = await context.Genres.Where(g => g.Name == BookGenreIdForInsert).Select(g => g.Id).ToArrayAsync();
			//var authorId = await context.Authors.Where(a => a.Surname == BookAuthorIdForInsert).Select(a => a.Id).ToArrayAsync();
			//var publish = await context.PublishingHouses.Where(p => p.Name == BookPublishingHouseIdForInsert).Select(p => p.Id).ToArrayAsync();
			//var subject = await context.SubjectMatters.Where(s => s.Name == BookSubjectMatterIdForInsert).Select(s => s.Id).ToArrayAsync();

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
				GenreId = BookGenreIdForInsert.Id,
				AutorId = BookAuthorIdForInsert.Id,
				PublishingHouseId = BookPublishingHouseIdForInsert.Id,
				SubjectMatterId = BookSubjectMatterIdForInsert.Id
			};

			context.Books.Add(book);
			await context.SaveChangesAsync();

			BookCountPagesForInsert = "";
			BookCostPriceForInsert = "";
			BookPrice = "";
			BookSeriesOfWorksForInsert = "";
			BookCountForInsert = "";

			DbBooks = GetBooks();
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

			//var genreId = await context.Genres.Where(g => g.Name == BookGenreIdForSearchUpdate).Select(g => g.Id).ToArrayAsync();
			//var authorId = await context.Authors.Where(a => a.Surname == BookAuthorIdForSearchUpdate).Select(a => a.Id).ToArrayAsync();
			//var publish = await context.PublishingHouses.Where(p => p.Name == BookPublishingHouseIdForSearchUpdate).Select(p => p.Id).ToArrayAsync();
			//var subject = await context.SubjectMatters.Where(s => s.Name == BookSubjectMatterIdForSearchUpdate).Select(s => s.Id).ToArrayAsync();

			SelectedBook.Name = BookNameForSearchUpdate;
			SelectedBook.NumberOfPages = pages;
			SelectedBook.Year = DateReleaseForSearchUpdate;
			SelectedBook.CostPrice = cost;
			SelectedBook.Price = price;
			SelectedBook.SeriesOfWorks = BookSeriesOfWorksForSearchUpdate;
			SelectedBook.CountOnStoke = count;
			SelectedBook.GenreId = BookGenreIdForSearchUpdate.Id;
			SelectedBook.AutorId = BookAuthorIdForSearchUpdate.Id;
			SelectedBook.PublishingHouseId = BookPublishingHouseIdForSearchUpdate.Id;
			SelectedBook.SubjectMatterId = BookSubjectMatterIdForSearchUpdate.Id;

			await context.SaveChangesAsync();

			DbBooks = GetBooks();
		}

		//Удаление книги
		private async Task BookRemove()
		{
			if (SelectedBook != null)
			{
				context.Books.Remove(SelectedBook);

				await context.SaveChangesAsync();
			}

			DbBooks = GetBooks();
		}

		//Создание акции
		private async Task StokeAdd()
		{
			double size;
			if (StokeGenreId == null && StokeSubjectMatterId == null)
				return;
			else if (!double.TryParse(StokeSize, out size))
				return;
			else if (string.IsNullOrEmpty(StokeName))
				return;

			var stoke = new Stoke()
			{
				Name = StokeName,
				Size = size,
				StratDate = StokeStratDate,
				EndDate = StokeEndDate,
				GenreId = StokeGenreId.Id,
				SubjectMatterId = StokeSubjectMatterId.Id,
			};

			if (StokeSubjectMatterId == null)
			{
				foreach (var book in context.Books)
				{
					if (book.SubjectMatterId == StokeSubjectMatterId.Id)
					{
						var id = await context.Stokes.Where(s => s.Name == StokeName).Select(s => s.Id).ToListAsync();
						book.StockId = id[0];
					}
				}
			}
			else if (StokeGenreId == null)
			{
				foreach (var book in context.Books)
				{
					if (book.GenreId == StokeGenreId.Id)
					{
						var id = await context.Stokes.Where(s => s.Name == StokeName).Select(s => s.Id).ToListAsync();
						book.StockId = id[0];
					}
				}
			}

			context.Stokes.Add(stoke);
			await context.SaveChangesAsync();

			DbStokes = GetStokes();
		}

		//Удаление акции
		private async Task StokeRemove()
		{
			if (SelectedStoke == null)
				return;

			foreach (var book in context.Books)
			{
				if (book.StockId == SelectedStoke.Id)
				{
					book.StockId = null;
				}
			}

			context.Stokes.Remove(SelectedStoke);
			await context.SaveChangesAsync();

			DbStokes = GetStokes();
		}

		//Поиск брони
		private async Task SearchBooking()
		{
			if (string.IsNullOrEmpty(BookingFullName))
				return;

			var bookings = await context.Bookings.Where(b => b.FullName == BookingFullName).ToListAsync();

			DbBookings = new ObservableCollection<Booking>(bookings);
		}

		//Активация брони
		private async Task BookingActive()
		{
			if (SelectedBooking is null)
				return;

			var sale = new Sale()
			{
				AuthorName = SelectedBooking.Book.Author.Surname,
				BookName = SelectedBooking.Book.Name,
				SaleDate = DateTime.Now,
				Count = SelectedBooking.Count
			};

			SelectedBooking.Book.CountOnStoke -= SelectedBooking.Count;
			context.Sales.Add(sale);
			context.Bookings.Remove(SelectedBooking);

			await context.SaveChangesAsync();

			DbBookings = GetBookings();
			DbSales = GetSales();
		}

		//Удаление брони
		private async Task BookingRemove()
		{
			if (SelectedBooking is null)
				return;

			context.Bookings.Remove(SelectedBooking);

			await context.SaveChangesAsync();

			DbBookings = GetBookings();
		}

		//Показ Продаж за день
		private async Task ShowSalesByDay()
		{
			var date = DateTime.Now;

			var sales = await context.Sales.Where(s => s.SaleDate >= date.AddDays(-1)).ToListAsync();

			DbSales = new ObservableCollection<Sale>(sales);
		}

		//Показ Продаж за день
		private async Task ShowSalesByWeek()
		{
			var date = DateTime.Now;

			var sales = await context.Sales.Where(s => s.SaleDate >= date.AddDays(-7)).ToListAsync();

			DbSales = new ObservableCollection<Sale>(sales);
		}

		//Показ Продаж за день
		private async Task ShowSalesByMonth()
		{
			var date = DateTime.Now;

			var sales = await context.Sales.Where(s => s.SaleDate >= date.AddMonths(-1)).ToListAsync();

			DbSales = new ObservableCollection<Sale>(sales);
		}

		//Показ Продаж за день
		private async Task ShowSalesByYear()
		{
			var date = DateTime.Now;

			var sales = await context.Sales.Where(s => s.SaleDate >= date.AddYears(-1)).ToListAsync();

			DbSales = new ObservableCollection<Sale>(sales);
		}

		//Показ Продаж за день
		private void ShowSalesBAllTime()
		{
			DbSales = GetSales();
		}

		//Добавляем Автора
		private async Task AddAuthor()
		{
			if (AutthorSurname is null)
				return;

			var author = new Author()
			{
				Firstname = AuthorFirstName,
				Surname = AutthorSurname,
				Patronymic = AuthirPatronymic
			};

			context.Authors.Add(author);
			await context.SaveChangesAsync();

			DbAuthors = GetAuthors();
			AuthorNames = GetAuthors();

			AuthorFirstName = "";
			AutthorSurname = "";
			AuthirPatronymic = "";
		}

		//Удаляем автора
		private async Task RemoveAuthor()
		{
			if (SelectedAuthor is null)
				return;

			context.Authors.Remove(SelectedAuthor);

			await context.SaveChangesAsync();

			DbAuthors = GetAuthors();
			AuthorNames = GetAuthors();
		}

		//Добавляем Жанр
		private async Task AddGenre()
		{
			if (string.IsNullOrEmpty(GenreName))
				return;

			var genre = new Genre()
			{
				Name = GenreName
			};

			context.Genres.Add(genre);

			await context.SaveChangesAsync();

			DbGenres = GetGenres();
			GenreNames = GetGenres();

			GenreName = "";
		}

		//Удаляем жанр
		private async Task RemoveGenre()
		{
			if (SelectedGenre is null)
				return;

			context.Genres.Remove(SelectedGenre);

			await context.SaveChangesAsync();

			DbGenres = GetGenres();
			GenreNames = GetGenres();
		}

		//Добавляем Тематику
		private async Task AddSubjectMatter()
		{
			if (string.IsNullOrEmpty(SubjectMatterName))
				return;

			var sub = new SubjectMatter()
			{
				Name = SubjectMatterName
			};

			context.SubjectMatters.Add(sub);

			await context.SaveChangesAsync();

			DbSubjectMatters = GetSubjectMatters();
			SubjectMatterNames = GetSubjectMatters();

			SubjectMatterName = "";
		}

		//Удаляем тематику
		private async Task RemoveSubjectMatter()
		{
			if (SelectedSubjectMatter is null)
				return;

			context.SubjectMatters.Remove(SelectedSubjectMatter);

			await context.SaveChangesAsync();

			DbSubjectMatters = GetSubjectMatters();
			SubjectMatterNames = GetSubjectMatters();
		}

		//Добавляем Издательство
		private async Task AddPublishingHouse()
		{
			if (PublishingHouseName is null)
				return;

			var pub = new PublishingHouse()
			{
				Name = PublishingHouseName,
				CIty = PublishingHouseCity,
				Phone = PublishingHousePhone
			};

			context.PublishingHouses.Add(pub);
			await context.SaveChangesAsync();

			DbPublishingHouses = GetPublishingHouses();
			PublishingHouseNames = GetPublishingHouses();

			PublishingHouseName = "";
			PublishingHouseCity = "";
			PublishingHousePhone = "";
		}

		//Удаляем Издательство
		private async Task RemovePublishingHouse()
		{
			if (SelectedPublishingHouse is null)
				return;

			context.PublishingHouses.Remove(SelectedPublishingHouse);

			await context.SaveChangesAsync();

			DbPublishingHouses = GetPublishingHouses();
			PublishingHouseNames = GetPublishingHouses();
		}
	}
}