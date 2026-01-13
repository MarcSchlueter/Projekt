using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects
{
    [Serializable]
    public class User : INotifyPropertyChanged
    {
        private string id;
        private string username;
        private string displayName;
        private string email;
        private string password;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public List<UploadRecord> Uploads { get; private set; }

        public List<DownloadRecord> Downloads { get; private set; }

        public IEnumerable<Book> Library
        {
            get { return Uploads.Select(record => record.Book); }
        }

        public User()
        {
            Uploads = new List<UploadRecord>();
            Downloads = new List<DownloadRecord>();
        }

        public User(string id, string username, string displayName, 
                    string email, string password)
        {
            this.id = id;
            this.username = username;
            this.displayName = displayName;
            this.email = email;
            this.password = password;
            Uploads = new List<UploadRecord>();
            Downloads = new List<DownloadRecord>();
        }

        public Rating RateBook(BookManager manager, Book book, 
                              int score, string comment)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            return manager.AddRating(this, book, score, comment);
        }

        public UploadRecord UploadBook(BookManager manager, Book book, 
                                      string storagePath)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            UploadRecord record = manager.UploadBook(this, book, storagePath);
            Uploads.Add(record);
            return record;
        }

        public DownloadRecord DownloadBook(BookManager manager, Book book, 
                                          string downloadLocation)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            DownloadRecord record = manager.DownloadBook(this, book, 
                                                        downloadLocation);
            Downloads.Add(record);
            return record;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}