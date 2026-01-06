using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagerConsoleApp051.Models
{
    public class BookManager
    {
        private readonly IList<Book> _books = new List<Book>();
        private readonly IList<User> _users = new List<User>();
        private readonly IList<UploadRecord> _uploads = new List<UploadRecord>();
        private readonly IList<DownloadRecord> _downloads = new List<DownloadRecord>();
        private readonly IList<DailyRanking> _dailyRankings = new List<DailyRanking>();

        public BookOfTheDaySelection? BookOfTheDay { get; private set; }

        public IReadOnlyCollection<Book> Books => _books.ToList();

        public IReadOnlyCollection<User> Users => _users.ToList();

        public IReadOnlyCollection<UploadRecord> Uploads => _uploads.ToList();

        public IReadOnlyCollection<DownloadRecord> Downloads => _downloads.ToList();

        public IReadOnlyCollection<DailyRanking> DailyRankings => _dailyRankings.ToList();

        public void RegisterUser(User user)
        {
            if (_users.Any(existing => existing.Id == user.Id))
            {
                return;
            }

            _users.Add(user);
        }

        public void AddBook(Book book)
        {
            if (_books.Any(existing => existing.Id == book.Id))
            {
                return;
            }

            _books.Add(book);
        }

        public UploadRecord UploadBook(User user, Book book, string storagePath)
        {
            AddBook(book);
            var record = new UploadRecord(user, book, DateTime.UtcNow, storagePath);
            _uploads.Add(record);
            return record;
        }

        public DownloadRecord DownloadBook(User user, Book book, string downloadLocation)
        {
            var record = new DownloadRecord(user, book, DateTime.UtcNow, downloadLocation);
            _downloads.Add(record);
            return record;
        }

        public Rating AddRating(User user, Book book, int score, string comment)
        {
            var rating = new Rating(user, book, score, comment, DateTime.UtcNow);
            book.AddRating(rating);
            return rating;
        }

        public void SelectBookOfTheDay(Book book, string reason)
        {
            BookOfTheDay = new BookOfTheDaySelection(DateTime.UtcNow.Date, book, reason);
            book.IsHighlighted = true;
        }

        public DailyRanking BuildDailyRanking()
        {
            var orderedBooks = _books
                .OrderByDescending(book => book.AverageRating)
                .ThenBy(book => book.Title)
                .ToList();

            var entries = orderedBooks
                .Select((book, index) => new RankingEntry(book, index + 1, book.AverageRating))
                .ToList();

            var ranking = new DailyRanking(DateTime.UtcNow.Date, entries);
            _dailyRankings.Add(ranking);
            return ranking;
        }

        public BookSearch BuildSearch() => new BookSearch(_books);
    }
}