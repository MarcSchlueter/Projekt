using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp051.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp051.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp051.Services.MessageBus;
using De.HsFlensburg.ClientApp051.Services.SerializationService;
using System;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.ViewModels
{
    public class MainWindowViewModel
    {
        private string pathForSerialization;
        private ModelFileHandler modelFileHandler;

        public BookManager BookManager { get; set; }
        public BookCollectionViewModel Books { get; set; }
        public UserCollectionViewModel Users { get; set; }

        public ICommand LoadDemoDataCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand AddBookCommand { get; }
        public ICommand AddUserCommand { get; }
        public ICommand BuildRankingCommand { get; }
        public ICommand AddRatingCommand { get; }

        public MainWindowViewModel()
        {
            BookManager = new BookManager();
            Books = new BookCollectionViewModel();
            Users = new UserCollectionViewModel();

            LoadDemoDataCommand = new RelayCommand(LoadDemoData);
            SaveCommand = new RelayCommand(SaveModel);
            LoadCommand = new RelayCommand(LoadModel);
            AddBookCommand = new RelayCommand(AddTestBook);
            AddUserCommand = new RelayCommand(AddTestUser);
            BuildRankingCommand = new RelayCommand(BuildRanking);
            AddRatingCommand = new RelayCommand(OpenAddRatingWindow);

            modelFileHandler = new ModelFileHandler();
            pathForSerialization = Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments) +
                "\\BookManagerSerialization\\BookManager.bm";

            LoadDemoData();
        }

        private void LoadDemoData()
        {
            BookManager = BookManagerInitializer.CreateDemoData();
            SyncCollectionsFromManager();
        }

        private void SyncCollectionsFromManager()
        {
            Books.Clear();
            foreach (var book in BookManager.Books)
            {
                var bookViewModel = new BookViewModel();
                bookViewModel.Model = book;
                Books.Add(bookViewModel);
            }

            Users.Clear();
            foreach (var user in BookManager.Users)
            {
                var userViewModel = new UserViewModel();
                userViewModel.Model = user;
                Users.Add(userViewModel);
            }
        }

        private void AddTestBook()
        {
            var newBook = new Book(
                "b-" + Guid.NewGuid().ToString().Substring(0, 8),
                "Neues Testbuch",
                "Test Autor",
                300,
                "Test");
            BookManager.AddBook(newBook);

            var bookViewModel = new BookViewModel();
            bookViewModel.Model = newBook;
            Books.Add(bookViewModel);
        }

        private void AddTestUser()
        {
            var newUser = new User(
                "u-" + Guid.NewGuid().ToString().Substring(0, 8),
                "testuser",
                "Test User",
                "test@example.com",
                "password");
            BookManager.RegisterUser(newUser);

            var userViewModel = new UserViewModel();
            userViewModel.Model = newUser;
            Users.Add(userViewModel);
        }

        private void BuildRanking()
        {
            var ranking = BookManager.BuildDailyRanking();
        }

        private void OpenAddRatingWindow()
        {
            ServiceBus.Instance.Send(
                new OpenAddRatingWindowMessage(BookManager));
        }

        private void SaveModel()
        {
            modelFileHandler.WriteModelToFile(
                pathForSerialization,
                BookManager);
        }

        private void LoadModel()
        {
            BookManager = modelFileHandler.ReadModelFromFile(
                pathForSerialization);
            SyncCollectionsFromManager();
        }
    }
}