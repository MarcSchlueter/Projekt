using System;
using System.Collections.Generic;
using System.Linq;

namespace De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects
{
    [Serializable]
    public class BookManager
    {
        private List<Book> books;
        private List<User> users;
        private List<UploadRecord> uploads;
        private List<DownloadRecord> downloads;
        private List<DailyRanking> dailyRankings;

        public BookOfTheDaySelection BookOfTheDay { get; private set; }

        public List<Book> Books
        {
            get { return books; }
        }

        public List<User> Users
        {
            get { return users; }
        }

        public List<UploadRecord> Uploads
        {
            get { return uploads; }
        }

        public List<DownloadRecord> Downloads
        {
            get { return downloads; }
        }

        public List<DailyRanking> DailyRankings
        {
            get { return dailyRankings; }
        }

        public BookManager()
        {
            books = new List<Book>();
            users = new List<User>();
            uploads = new List<UploadRecord>();
            downloads = new List<DownloadRecord>();
            dailyRankings = new List<DailyRanking>();
        }

        public void RegisterUser(User user)
        {
            if (users.Any(existing => existing.Id == user.Id))
            {
                return;
            }

            users.Add(user);
        }

        public void AddBook(Book book)
        {
            if (books.Any(existing => existing.Id == book.Id))
            {
                return;
            }

            books.Add(book);
        }

        public UploadRecord UploadBook(User user, Book book,
                                      string storagePath)
        {
            AddBook(book);
            UploadRecord record = new UploadRecord(user, book,
                                                  DateTime.UtcNow,
                                                  storagePath);
            uploads.Add(record);
            return record;
        }

        public DownloadRecord DownloadBook(User user, Book book,
                                          string downloadLocation)
        {
            DownloadRecord record = new DownloadRecord(user, book,
                                                      DateTime.UtcNow,
                                                      downloadLocation);
            downloads.Add(record);
            return record;
        }

        public Rating AddRating(User user, Book book, int score,
                               string comment)
        {
            Rating rating = new Rating(user, book, score, comment,
                                      DateTime.UtcNow);
            book.AddRating(rating);
            return rating;
        }

        public void SelectBookOfTheDay(Book book, string reason)
        {
            BookOfTheDay = new BookOfTheDaySelection(DateTime.UtcNow.Date,
                                                     book, reason);
            book.IsHighlighted = true;
        }

        public DailyRanking BuildDailyRanking()
        {
            List<Book> orderedBooks = books
                .OrderByDescending(book => book.AverageRating)
                .ThenBy(book => book.Title)
                .ToList();

            List<RankingEntry> entries = orderedBooks
                .Select((book, index) => new RankingEntry(book,
                                                         index + 1,
                                                         book.AverageRating))
                .ToList();

            DailyRanking ranking = new DailyRanking(DateTime.UtcNow.Date,
                                                    entries);
            dailyRankings.Add(ranking);
            return ranking;
        }

        public BookSearch BuildSearch()
        {
            return new BookSearch(books);
        }
    }
}