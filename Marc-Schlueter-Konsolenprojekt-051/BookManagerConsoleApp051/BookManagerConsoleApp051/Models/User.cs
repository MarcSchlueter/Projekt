using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagerConsoleApp051.Models
{
    public class User
    {
        public User(string id, string username, string displayName, string email, string password)
        {
            Id = id;
            Username = username;
            DisplayName = displayName;
            Email = email;
            Password = password;
        }

        public string Id { get; }

        public string Username { get; }

        public string DisplayName { get; }

        public string Email { get; }

        public string Password { get; }

        public IList<UploadRecord> Uploads { get; } = new List<UploadRecord>();

        public IList<DownloadRecord> Downloads { get; } = new List<DownloadRecord>();

        public IEnumerable<Book> Library => Uploads.Select(record => record.Book);

        public Rating RateBook(BookManager manager, Book book, int score, string comment)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            return manager.AddRating(this, book, score, comment);
        }

        public UploadRecord UploadBook(BookManager manager, Book book, string storagePath)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            var record = manager.UploadBook(this, book, storagePath);
            Uploads.Add(record);
            return record;
        }

        public DownloadRecord DownloadBook(BookManager manager, Book book, string downloadLocation)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            var record = manager.DownloadBook(this, book, downloadLocation);
            Downloads.Add(record);
            return record;
        }
    }
}