namespace Isu.Properties
{
    public class Student
    {
        public Student(string name, int id)
        {
            StudentsName = name;
            Id = id;
        }

        public int Id { get; }

        public string StudentsName { get; }
    }
}