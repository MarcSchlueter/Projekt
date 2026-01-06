using System.Collections.Generic;
using System.Linq;

namespace BookManagerConsoleApp051.Models
{
    public class BookSearch
    {
        private readonly IEnumerable<Book> _catalog;

        public BookSearch(IEnumerable<Book> catalog)
        {
            _catalog = catalog;
        }

        public IEnumerable<Book> SearchByKeyword(string keyword)
        {
            return _catalog.Where(book =>
                book.Title.ToLowerInvariant().Contains(keyword.ToLowerInvariant()) ||
                book.Author.ToLowerInvariant().Contains(keyword.ToLowerInvariant()));
        }

        public IEnumerable<Book> SearchByRating(double minimumAverageRating)
        {
            return _catalog.Where(book => book.AverageRating >= minimumAverageRating);
        }
    }
}