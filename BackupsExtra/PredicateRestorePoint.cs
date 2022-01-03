using System;

namespace BackupsExtra
{
    public class PredicateRestorePoint
    {
        public PredicateRestorePoint(DateTime dateTime, int count)
        {
            DateTime = dateTime;
            Count = count;
        }

        public DateTime DateTime { get; }
        public int Count { get; }
    }
}