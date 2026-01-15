using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.ViewModels
{
    public class AddRatingWindowViewModel : INotifyPropertyChanged
    {
        private BookManager bookManager;
        private User selectedUser;
        private Book selectedBook;
        private int selectedScore;
        private string comment;
        private string validationMessage;

        public List<User> AvailableUsers { get; set; }
        public List<Book> AvailableBooks { get; set; }
        public List<int> AvailableScores { get; set; }

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

        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
                UpdateValidationMessage();
            }
        }

        public int SelectedScore
        {
            get { return selectedScore; }
            set
            {
                selectedScore = value;
                OnPropertyChanged(nameof(SelectedScore));
                UpdateValidationMessage();
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
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

        public ICommand SubmitCommand { get; }

        public event EventHandler RatingAdded;

        public AddRatingWindowViewModel(BookManager manager)
        {
            bookManager = manager;
            AvailableUsers = bookManager.Users.ToList();
            AvailableBooks = bookManager.Books.ToList();
            AvailableScores = new List<int> { 1, 2, 3, 4, 5 };

            comment = string.Empty;
            selectedScore = 3;
            validationMessage = string.Empty;

            SubmitCommand = new RelayCommand(SubmitRating);

            UpdateValidationMessage();
        }

        private void UpdateValidationMessage()
        {
            if (SelectedUser == null)
            {
                ValidationMessage = "Bitte waehlen Sie einen Benutzer aus.";
            }
            else if (SelectedBook == null)
            {
                ValidationMessage = "Bitte waehlen Sie ein Buch aus.";
            }
            else if (SelectedScore <= 0)
            {
                ValidationMessage = "Bitte waehlen Sie eine Bewertung aus.";
            }
            else
            {
                ValidationMessage = string.Empty;
            }
        }

        private void SubmitRating()
        {
            if (SelectedUser == null || SelectedBook == null ||
                SelectedScore <= 0)
            {
                MessageBox.Show(
                    "Bitte fuellen Sie alle Pflichtfelder aus:\n" +
                    "- Benutzer\n- Buch\n- Bewertung (1-5)",
                    "Unvollstaendige Eingabe",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            selectedUser.RateBook(bookManager, selectedBook,
                                selectedScore, comment);

            RatingAdded?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}