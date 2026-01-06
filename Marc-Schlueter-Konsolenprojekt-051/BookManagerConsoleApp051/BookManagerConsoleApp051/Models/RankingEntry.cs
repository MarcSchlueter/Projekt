namespace BookManagerConsoleApp051.Models
{
    public class RankingEntry
    {
        public RankingEntry(Book book, int position, double score)
        {
            Book = book;
            Position = position;
            Score = score;
        }

        public Book Book { get; }

        public int Position { get; }

        public double Score { get; }
    }
}