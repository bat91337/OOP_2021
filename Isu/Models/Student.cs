using Isu.Tools;
namespace Isu.Properties
{
    public class Student
    {
        public Student(string name, int id)
        {
            StudentsName = name;
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new IsuException("empty line");
            }

            Id = id;
        }

        public int Id { get; }

        public string StudentsName { get; }
    }
}