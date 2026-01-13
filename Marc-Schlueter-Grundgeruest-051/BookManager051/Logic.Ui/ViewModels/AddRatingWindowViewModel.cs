using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public int SelectedScore
        {
            get { return selectedScore; }
            set
            {
                selectedScore = value;
                OnPropertyChanged(nameof(SelectedScore));
                OnPropertyChanged(nameof(CanSubmit));
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

        public bool CanSubmit
        {
            get
            {
                return SelectedUser != null &&
                       SelectedBook != null &&
                       SelectedScore > 0;
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
            selectedScore = 0;

            SubmitCommand = new RelayCommand(SubmitRating,
                                            () => CanSubmit);
        }

        private void SubmitRating()
        {
            if (!CanSubmit)
                return;

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