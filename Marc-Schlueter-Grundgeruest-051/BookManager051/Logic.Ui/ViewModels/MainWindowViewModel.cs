using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp051.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp051.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp051.Services.MessageBus;
using De.HsFlensburg.ClientApp051.Services.SerializationService;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
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
        public ICommand UploadBookCommand { get; }
        public ICommand DownloadBookCommand { get; }

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
            UploadBookCommand = new RelayCommand(OpenUploadBookWindow);
            DownloadBookCommand = new RelayCommand<BookViewModel>(
                DownloadBook);

            modelFileHandler = new ModelFileHandler();
            pathForSerialization = Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments) +
                "\\BookManagerSerialization\\BookManager.bm";

            ServiceBus.Instance.Register<RatingAddedMessage>(
                this, OnRatingAdded);

            ServiceBus.Instance.Register<BookUploadedMessage>(
                this, OnBookUploaded);

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

        private void OpenUploadBookWindow()
        {
            ServiceBus.Instance.Send(
                new OpenUploadBookWindowMessage(BookManager));
        }

        private void DownloadBook(BookViewModel bookViewModel)
        {
            if (bookViewModel == null)
                return;

            if (BookManager.Users.Count == 0)
            {
                MessageBox.Show(
                    "Kein Benutzer verfuegbar. Bitte erstellen Sie zuerst einen Benutzer.",
                    "Kein Benutzer",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            User currentUser = BookManager.Users.First();

            UploadRecord uploadRecord = BookManager.Uploads.FirstOrDefault(
                record => record.Book.Id == bookViewModel.Id);

            if (uploadRecord == null || string.IsNullOrEmpty(uploadRecord.StoragePath))
            {
                MessageBox.Show(
                    "Dieses Buch hat keine hochgeladene Datei.\n" +
                    "Nur hochgeladene Buecher koennen heruntergeladen werden.",
                    "Keine Datei verfuegbar",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (!File.Exists(uploadRecord.StoragePath))
            {
                MessageBox.Show(
                    $"Die Datei wurde nicht gefunden:\n{uploadRecord.StoragePath}\n\n" +
                    "Moeglicherweise wurde sie geloescht oder verschoben.",
                    "Datei nicht gefunden",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = bookViewModel.Title + ".pdf";
            saveFileDialog.Filter = "PDF Dateien (*.pdf)|*.pdf";
            saveFileDialog.Title = "Buch herunterladen";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.Copy(uploadRecord.StoragePath, saveFileDialog.FileName, true);

                    File.SetCreationTime(saveFileDialog.FileName, DateTime.Now);
                    File.SetLastWriteTime(saveFileDialog.FileName, DateTime.Now);
                    File.SetLastAccessTime(saveFileDialog.FileName, DateTime.Now);

                    currentUser.DownloadBook(BookManager, bookViewModel.Model,
                                           saveFileDialog.FileName);

                    MessageBox.Show(
                        $"Buch '{bookViewModel.Title}' erfolgreich heruntergeladen nach:\n" +
                        saveFileDialog.FileName,
                        "Download erfolgreich",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Fehler beim Download: {ex.Message}",
                        "Download fehlgeschlagen",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void OnRatingAdded()
        {
            SyncCollectionsFromManager();
        }

        private void OnBookUploaded()
        {
            SyncCollectionsFromManager();
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}