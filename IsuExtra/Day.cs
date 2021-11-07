using System;
using System.Collections.Generic;
using System.Linq;

namespace IsuExtra
{
    public class Day
    {
        public Day(DayOfWeek dayOfWeek)
        {
            Pairs = new List<Pair>();
            DayOfWeek = dayOfWeek;
        }

        public List<Pair> Pairs { get; }
        public DayOfWeek DayOfWeek { get; }

        public bool CheckSchedulePair(Day day)
        {
            return day.Pairs.Any(pair => Pairs.Any(pairs => pair.PairNumber.Equals(pairs.PairNumber)));
        }
    }
}