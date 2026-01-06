using System;

namespace BookManagerConsoleApp051.Models
{
    public class Rating
    {
        public Rating(User reviewer, Book book, int score, string comment, DateTime createdAt)
        {
            Reviewer = reviewer;
            Book = book;
            Score = score;
            Comment = comment;
            CreatedAt = createdAt;
        }

        public User Reviewer { get; }

        public Book Book { get; }

        public int Score { get; }

        public string Comment { get; }

        public DateTime CreatedAt { get; }
    }
}