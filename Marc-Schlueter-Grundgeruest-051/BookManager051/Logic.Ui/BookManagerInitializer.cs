using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using System;

namespace De.HsFlensburg.ClientApp051.Logic.Ui
{
    public static class BookManagerInitializer
    {
        public static BookManager CreateDemoData()
        {
            var manager = new BookManager();

            var alice = new User("u-100", "alice", "Alice Becker",
                                "alice@example.com", "Passw0rd!");
            var bob = new User("u-200", "bob", "Bob Stein",
                              "bob@example.com", "Secret123");
            var charlie = new User("u-300", "charlie", "Charlie Mueller",
                                  "charlie@example.com", "Secure456");

            manager.RegisterUser(alice);
            manager.RegisterUser(bob);
            manager.RegisterUser(charlie);

            var handbook = new Book("b-050", "C# Praktiker-Handbuch",
                                   "Nora Weiss", 250, "Referenz");
            var mysteryBook = new Book("b-100", "Der Schattenpfad",
                                      "Mara Klein", 420, "Mystery");
            var fantasyBook = new Book("b-200", "Drachenflug",
                                      "Felix Sturm", 310, "Fantasy");
            var scienceBook = new Book("b-300", "Quantenlicht",
                                      "Dr. Eva Sommer", 280, "Science");
            var thrillerBook = new Book("b-400", "Mitternachtsjagd",
                                       "Tom Berg", 380, "Thriller");

            manager.AddBook(handbook);
            alice.UploadBook(manager, mysteryBook,
                           "/library/mystery/der-schattenpfad.pdf");
            alice.UploadBook(manager, fantasyBook,
                           "/library/fantasy/drachenflug.epub");
            bob.UploadBook(manager, scienceBook,
                         "/library/science/quantenlicht.pdf");
            charlie.UploadBook(manager, thrillerBook,
                             "/library/thriller/mitternachtsjagd.pdf");

            bob.DownloadBook(manager, mysteryBook,
                           "/downloads/bob/der-schattenpfad.pdf");
            alice.DownloadBook(manager, scienceBook,
                             "/downloads/alice/quantenlicht.pdf");
            charlie.DownloadBook(manager, fantasyBook,
                               "/downloads/charlie/drachenflug.epub");

            alice.RateBook(manager, mysteryBook, 5,
                         "Spannung bis zur letzten Seite");
            bob.RateBook(manager, mysteryBook, 4,
                       "Tolle Figuren, kleiner Haenger im Mittelteil");
            charlie.RateBook(manager, mysteryBook, 5,
                           "Absolut fesselnd!");

            alice.RateBook(manager, fantasyBook, 4,
                         "Schoene Fantasywelt");
            bob.RateBook(manager, scienceBook, 5,
                       "Sehr informativ und gut geschrieben");
            charlie.RateBook(manager, thrillerBook, 3,
                           "Solide, aber vorhersehbar");

            manager.SelectBookOfTheDay(mysteryBook,
                "Hoechste Bewertung und spannende Handlung");

            manager.BuildDailyRanking();

            return manager;
        }
    }
}