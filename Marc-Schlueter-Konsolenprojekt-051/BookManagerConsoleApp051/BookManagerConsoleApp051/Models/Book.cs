using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagerConsoleApp051.Models
{
    public class Book
    {
        public Book(string id, string title, string author, int pageCount, string category)
        {
            Id = id;
            Title = title;
            Author = author;
            PageCount = pageCount;
            Category = category;
        }

        public string Id { get; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int PageCount { get; set; }

        public string Category { get; set; }

        public bool IsHighlighted { get; set; }

        public IList<Rating> Ratings { get; } = new List<Rating>();

        public double AverageRating => Ratings.Any() ? Ratings.Average(rating => rating.Score) : 0.0;

        public void AddRating(Rating rating)
        {
            if (rating == null)
            {
                throw new ArgumentNullException(nameof(rating));
            }

            Ratings.Add(rating);
        }
    }
}