using System;
using System.Collections.Generic;
using System.Linq;
using BookManagerConsoleApp051.Models;

namespace BookManagerConsoleApp051
{
    internal class Program
    {
        private static void Main()
        {
            var manager = new BookManager();

            Console.WriteLine("### Nutzerregistrierung (BookManager.RegisterUser)");
            ShowUsers(manager.Users, "Vorher");
            var alice = new User("u-100", "alice", "Alice Becker", "alice@example.com", "Passw0rd!");
            var bob = new User("u-200", "bob", "Bob Stein", "bob@example.com", "Secret123");
            manager.RegisterUser(alice);
            manager.RegisterUser(bob);
            ShowUsers(manager.Users, "Nachher");

            Console.WriteLine();
            Console.WriteLine("### Direkte Bucherfassung (BookManager.AddBook)");
            ShowBooks(manager.Books, "Vorher");
            var handbook = new Book("b-050", "C# Praktiker-Handbuch", "Nora Weiss", 250, "Referenz");
            manager.AddBook(handbook);
            ShowBooks(manager.Books, "Nachher");

            Console.WriteLine();
            Console.WriteLine("### Uploads (User.UploadBook → BookManager.UploadBook)");
            ShowUploads(manager.Uploads, "Vorher");
            var mysteryBook = new Book("b-100", "Der Schattenpfad", "Mara Klein", 420, "Mystery");
            var fantasyBook = new Book("b-200", "Drachenflug", "Felix Sturm", 310, "Fantasy");
            var scienceBook = new Book("b-300", "Quantenlicht", "Dr. Eva Sommer", 280, "Science");

            alice.UploadBook(manager, mysteryBook, "/library/mystery/der-schattenpfad.pdf");
            alice.UploadBook(manager, fantasyBook, "/library/fantasy/drachenflug.epub");
            bob.UploadBook(manager, scienceBook, "/library/science/quantenlicht.pdf");
            ShowUploads(manager.Uploads, "Nachher");
            ShowUserUploads(alice);
            ShowUserUploads(bob);

            Console.WriteLine();
            Console.WriteLine("### Downloads (User.DownloadBook → BookManager.DownloadBook)");
            ShowDownloads(manager.Downloads, "Vorher");
            bob.DownloadBook(manager, mysteryBook, "/downloads/bob/der-schattenpfad.pdf");
            alice.DownloadBook(manager, scienceBook, "/downloads/alice/quantenlicht.pdf");
            ShowDownloads(manager.Downloads, "Nachher");
            ShowUserDownloads(alice);
            ShowUserDownloads(bob);

            Console.WriteLine();
            Console.WriteLine("### Bewertungen (User.RateBook → Book.AddRating)");
            ShowRatingsForBook(mysteryBook, "Vorher");
            alice.RateBook(manager, mysteryBook, 5, "Spannung bis zur letzten Seite");
            bob.RateBook(manager, mysteryBook, 4, "Tolle Figuren, kleiner Hänger im Mittelteil");
            ShowRatingsForBook(mysteryBook, "Nachher");

            Console.WriteLine();
            Console.WriteLine("### Buch des Tages (BookManager.SelectBookOfTheDay)");
            Console.WriteLine("Vorher: kein Eintrag");
            manager.SelectBookOfTheDay(mysteryBook, "Höchste Bewertung und spannende Handlung");
            var bookOfTheDay = manager.BookOfTheDay;
            if (bookOfTheDay != null)
            {
                Console.WriteLine(
                    $"Nachher: {bookOfTheDay.Book.Title} am {bookOfTheDay.SelectedAt:yyyy-MM-dd} – {bookOfTheDay.Reason}");
            }

            Console.WriteLine();
            Console.WriteLine("### Tagesrangliste (BookManager.BuildDailyRanking)");
            Console.WriteLine($"Vorher: {manager.DailyRankings.Count} gespeicherte Ranglisten");
            var ranking = manager.BuildDailyRanking();
            Console.WriteLine($"Nachher: {manager.DailyRankings.Count} gespeicherte Ranglisten");
            foreach (var entry in ranking.Entries)
            {
                Console.WriteLine($"{entry.Position}. {entry.Book.Title} – Score {entry.Score:F1}");
            }

            Console.WriteLine();
            Console.WriteLine("### Suche (BookManager.BuildSearch + BookSearch.SearchByKeyword)");
            var search = manager.BuildSearch();
            var searchResults = search.SearchByKeyword("drachen");
            Console.WriteLine("Suchergebnisse für 'drachen':");
            foreach (var result in searchResults)
            {
                Console.WriteLine($"- {result.Title} ({result.Category})");
            }

            Console.WriteLine();
            Console.WriteLine("### Zusammenfassung zum Debuggen");
            Console.WriteLine("Kontrolliere im Debugger die Objekte manager, alice und bob, um alle Listen einzusehen.");
        }

        private static void ShowUsers(IEnumerable<User> users, string label)
        {
            var list = users.ToList();
            Console.WriteLine($"{label}: {list.Count} Nutzer");
            foreach (var user in list)
            {
                Console.WriteLine($"- {user.DisplayName} ({user.Username})");
            }
        }

        private static void ShowBooks(IEnumerable<Book> books, string label)
        {
            var list = books.ToList();
            Console.WriteLine($"{label}: {list.Count} Bücher");
            foreach (var book in list)
            {
                Console.WriteLine($"- {book.Title} von {book.Author} [{book.Category}]");
            }
        }

        private static void ShowUploads(IEnumerable<UploadRecord> uploads, string label)
        {
            var list = uploads.ToList();
            Console.WriteLine($"{label}: {list.Count} Uploads");
            foreach (var upload in list)
            {
                Console.WriteLine(
                    $"- {upload.Uploader.DisplayName} → '{upload.Book.Title}' nach {upload.StoragePath} am {upload.UploadedAt:HH:mm:ss} UTC");
            }
        }

        private static void ShowDownloads(IEnumerable<DownloadRecord> downloads, string label)
        {
            var list = downloads.ToList();
            Console.WriteLine($"{label}: {list.Count} Downloads");
            foreach (var download in list)
            {
                Console.WriteLine(
                    $"- {download.Downloader.DisplayName} hat '{download.Book.Title}' nach {download.LocalPath} am {download.DownloadedAt:HH:mm:ss} UTC");
            }
        }

        private static void ShowUserUploads(User user)
        {
            var list = user.Uploads.ToList();
            Console.WriteLine($"Uploads von {user.DisplayName}: {list.Count}");
            foreach (var upload in list)
            {
                Console.WriteLine($"- '{upload.Book.Title}' nach {upload.StoragePath}");
            }
        }

        private static void ShowUserDownloads(User user)
        {
            var list = user.Downloads.ToList();
            Console.WriteLine($"Downloads von {user.DisplayName}: {list.Count}");
            foreach (var download in list)
            {
                Console.WriteLine($"- '{download.Book.Title}' nach {download.LocalPath}");
            }
        }

        private static void ShowRatingsForBook(Book book, string label)
        {
            Console.WriteLine($"{label}: {book.Title} – {book.Ratings.Count} Bewertungen, Ø {book.AverageRating:F1}");
            foreach (var rating in book.Ratings)
            {
                Console.WriteLine($"- {rating.Score}/5 von {rating.Reviewer.DisplayName}: {rating.Comment}");
            }
        }
    }
}