using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.ViewModels
{
    public class UploadBookWindowViewModel : INotifyPropertyChanged
    {
        private BookManager bookManager;
        private User selectedUser;
        private string title;
        private string author;
        private string category;
        private string description;
        private string selectedFilePath;
        private string validationMessage;
        private int pageCount;

        public List<User> AvailableUsers { get; set; }

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                UpdateValidationMessage();
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
                UpdateValidationMessage();
            }
        }

        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged(nameof(Author));
                UpdateValidationMessage();
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
                UpdateValidationMessage();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
                UpdateValidationMessage();
            }
        }

        public int PageCount
        {
            get { return pageCount; }
            set
            {
                pageCount = value;
                OnPropertyChanged(nameof(PageCount));
                UpdateValidationMessage();
            }
        }

        public string SelectedFilePath
        {
            get { return selectedFilePath; }
            set
            {
                selectedFilePath = value;
                OnPropertyChanged(nameof(SelectedFilePath));
                OnPropertyChanged(nameof(FileName));
                UpdateValidationMessage();
            }
        }

        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(selectedFilePath))
                    return "Keine Datei ausgewaehlt";
                return Path.GetFileName(selectedFilePath);
            }
        }

        public string ValidationMessage
        {
            get { return validationMessage; }
            set
            {
                validationMessage = value;
                OnPropertyChanged(nameof(ValidationMessage));
            }
        }

        public ICommand SelectFileCommand { get; }
        public ICommand SubmitCommand { get; }

        public event EventHandler BookUploaded;

        public UploadBookWindowViewModel(BookManager manager)
        {
            bookManager = manager;
            AvailableUsers = bookManager.Users.ToList();

            title = string.Empty;
            author = string.Empty;
            category = string.Empty;
            description = string.Empty;
            selectedFilePath = string.Empty;
            validationMessage = string.Empty;
            pageCount = 0;

            SelectFileCommand = new RelayCommand(SelectFile);
            SubmitCommand = new RelayCommand(SubmitUpload);

            UpdateValidationMessage();
        }

        private void SelectFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Dateien (*.pdf)|*.pdf";
            openFileDialog.Title = "Buch-Datei auswaehlen";

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFilePath = openFileDialog.FileName;
            }
        }

        private void UpdateValidationMessage()
        {
            if (SelectedUser == null)
            {
                ValidationMessage = "Bitte waehlen Sie einen Benutzer aus.";
            }
            else if (string.IsNullOrWhiteSpace(Title))
            {
                ValidationMessage = "Bitte geben Sie einen Titel ein.";
            }
            else if (string.IsNullOrWhiteSpace(Author))
            {
                ValidationMessage = "Bitte geben Sie einen Autor ein.";
            }
            else if (string.IsNullOrWhiteSpace(Category))
            {
                ValidationMessage = "Bitte geben Sie eine Kategorie ein.";
            }
            else if (string.IsNullOrWhiteSpace(Description))
            {
                ValidationMessage = "Bitte geben Sie eine Beschreibung ein.";
            }
            else if (PageCount <= 0)
            {
                ValidationMessage = "Bitte geben Sie die Seitenzahl ein.";
            }
            else if (string.IsNullOrEmpty(SelectedFilePath))
            {
                ValidationMessage = "Bitte waehlen Sie eine PDF-Datei aus.";
            }
            else
            {
                ValidationMessage = string.Empty;
            }
        }

        private void SubmitUpload()
        {
            if (SelectedUser == null || string.IsNullOrWhiteSpace(Title) ||
                string.IsNullOrWhiteSpace(Author) ||
                string.IsNullOrWhiteSpace(Category) ||
                string.IsNullOrWhiteSpace(Description) ||
                PageCount <= 0 ||
                string.IsNullOrEmpty(SelectedFilePath))
            {
                MessageBox.Show(
                    "Bitte fuellen Sie alle Pflichtfelder aus:\n" +
                    "- Benutzer\n- Titel\n- Autor\n- Kategorie\n" +
                    "- Beschreibung\n- Seitenzahl\n- PDF-Datei",
                    "Unvollstaendige Eingabe",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                string bookId = "b-" + Guid.NewGuid().ToString().Substring(0, 8);

                string storageDirectory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "BookManagerStorage",
                    Category.ToLower());

                if (!Directory.Exists(storageDirectory))
                {
                    Directory.CreateDirectory(storageDirectory);
                }

                string fileName = bookId + "_" + Path.GetFileName(SelectedFilePath);
                string storagePath = Path.Combine(storageDirectory, fileName);

                File.Copy(SelectedFilePath, storagePath, true);

                Book newBook = new Book(bookId, Title, Author, PageCount, Category);

                SelectedUser.UploadBook(bookManager, newBook, storagePath);

                BookUploaded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Fehler beim Hochladen: {ex.Message}",
                    "Upload fehlgeschlagen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}