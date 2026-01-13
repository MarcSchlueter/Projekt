using System;
using System.Collections.Generic;
using System.Linq;

namespace De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects
{
    [Serializable]
    public class BookSearch
    {
        private List<Book> catalog;

        public BookSearch()
        {
            catalog = new List<Book>();
        }

        public BookSearch(IEnumerable<Book> catalogBooks)
        {
            catalog = catalogBooks.ToList();
        }

        public IEnumerable<Book> SearchByKeyword(string keyword)
        {
            return catalog.Where(book =>
                book.Title.ToLowerInvariant()
                    .Contains(keyword.ToLowerInvariant()) ||
                book.Author.ToLowerInvariant()
                    .Contains(keyword.ToLowerInvariant()));
        }

        public IEnumerable<Book> SearchByRating(double minimumAverageRating)
        {
            return catalog.Where(book =>
                book.AverageRating >= minimumAverageRating);
        }
    }
}