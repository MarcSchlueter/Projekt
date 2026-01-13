using System;

namespace De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects
{
    [Serializable]
    public class RankingEntry
    {
        public Book Book { get; private set; }
        public int Position { get; private set; }
        public double Score { get; private set; }

        public RankingEntry()
        {
        }

        public RankingEntry(Book book, int position, double score)
        {
            Book = book;
            Position = position;
            Score = score;
        }
    }
}