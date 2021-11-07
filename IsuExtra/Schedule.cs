using System.Collections.Generic;
using System.Linq;

namespace IsuExtra
{
    public class Schedule
    {
        public Schedule()
        {
            Days = new List<Day>();
        }

        public List<Day> Days { get; }

        public bool CheckShceduleDays(ScheduleGroup scheduleGroup)
        {
            foreach (Day day in scheduleGroup.Schedule.Days)
            {
                if (Days.Where(days => day.DayOfWeek.Equals(days.DayOfWeek)).Any(days => days.CheckSchedulePair(day)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}