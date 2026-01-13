using System;
using System.Collections.Generic;

namespace De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects
{
    [Serializable]
    public class DailyRanking
    {
        public DateTime Date { get; private set; }
        public List<RankingEntry> Entries { get; private set; }

        public DailyRanking()
        {
            Entries = new List<RankingEntry>();
        }

        public DailyRanking(DateTime date, List<RankingEntry> entries)
        {
            Date = date;
            Entries = entries;
        }
    }
}