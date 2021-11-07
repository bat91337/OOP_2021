using Isu.Models;

namespace IsuExtra
{
    public class ScheduleGroup
    {
        public ScheduleGroup(Group group)
        {
            Group = group;
            Schedule = new Schedule();
        }

        public Group Group { get; }
        public Schedule Schedule { get; }
    }
}