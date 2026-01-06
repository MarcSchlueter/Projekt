using System;
using System.Collections.Generic;

namespace BookManagerConsoleApp051.Models
{
    public class DailyRanking
    {
        public DailyRanking(DateTime date, IList<RankingEntry> entries)
        {
            Date = date;
            Entries = entries;
        }

        public DateTime Date { get; }

        public IList<RankingEntry> Entries { get; }
    }
}