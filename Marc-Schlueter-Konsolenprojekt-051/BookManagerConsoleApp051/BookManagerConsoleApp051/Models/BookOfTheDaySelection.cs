using System;

namespace BookManagerConsoleApp051.Models
{
    public class BookOfTheDaySelection
    {
        public BookOfTheDaySelection(DateTime selectedAt, Book book, string reason)
        {
            SelectedAt = selectedAt;
            Book = book;
            Reason = reason;
        }

        public DateTime SelectedAt { get; }

        public Book Book { get; }

        public string Reason { get; }
    }
}