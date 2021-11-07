namespace IsuExtra
{
    public class ScheduleFaculty
    {
        public ScheduleFaculty(Faculty faculty)
        {
            Schedule = new Schedule();
            Faculty = faculty;
        }

        public Schedule Schedule { get; }
        public Faculty Faculty { get; }
    }
}