using System;

namespace BackupsExtra
{
    public class PredicateRestorePoint
    {
        public PredicateRestorePoint(DateTime dateTime, int count, bool algorithm)
        {
            DateTime = dateTime;
            Count = count;
            Algorithm = algorithm;
        }

        public DateTime DateTime { get; }
        public int Count { get; }
        public bool Algorithm { get; }
    }
}