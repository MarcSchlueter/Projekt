using System;

namespace De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects
{
    [Serializable]
    public class Rating
    {
        public User Reviewer { get; private set; }
        public Book Book { get; private set; }
        public int Score { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Rating()
        {
        }

        public Rating(User reviewer, Book book, int score,
                     string comment, DateTime createdAt)
        {
            Reviewer = reviewer;
            Book = book;
            Score = score;
            Comment = comment;
            CreatedAt = createdAt;
        }
    }
}