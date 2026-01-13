using System;

namespace De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects
{
    [Serializable]
    public class BookOfTheDaySelection
    {
        public DateTime SelectedAt { get; private set; }
        public Book Book { get; private set; }
        public string Reason { get; private set; }

        public BookOfTheDaySelection()
        {
        }

        public BookOfTheDaySelection(DateTime selectedAt, Book book,
                                    string reason)
        {
            SelectedAt = selectedAt;
            Book = book;
            Reason = reason;
        }
    }
}