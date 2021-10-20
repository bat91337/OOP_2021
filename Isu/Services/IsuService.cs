using System.Collections.Generic;
using System.Linq;
using Isu.Models;
using Isu.Properties;
using Isu.Services;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<Group> _groupList;
        private int _maxGroupSize;
        private int _studentId;

        public IsuService(int maxgroupsize)
        {
            _groupList = new List<Group>();
            _maxGroupSize = maxgroupsize;
            _studentId = 0;
        }

        public Group AddGroup(string name)
        {
            var group = new Group(name);
            _groupList.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count >= _maxGroupSize)
            {
                throw new IsuException("error");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new IsuException("error");
            }

            foreach (Group group1 in _groupList)
            {
                if (group1.GroupName.Equals(group.GroupName))
                {
                    var student = new Student(name, _studentId++);
                    group1.Students.Add(student);
                    return student;
                }
            }

            return null;
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in _groupList)
            {
                Student student = group.Students.Find(student => student.Id == id);
                if (student != null)
                {
                    return student;
                }
            }

            throw new IsuException("error");
        }

        public Student FindStudent(string name)
        {
            foreach (Group group in _groupList)
            {
                Student student = group.Students.Find(student => student.StudentsName.Equals(name));
                if (student != null)
                {
                    return student;
                }
            }

            throw new IsuException("error");
        }

        public List<Student> FindStudents(string groupName)
        {
            return _groupList.Where(@group => groupName.Equals(@group)).SelectMany(@group => @group.Students).ToList();
        }

        public Group FindGroup(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new IsuException("empty line");
            }

            Group group = _groupList.Find(group => group.GroupName.Equals(groupName));
            if (group != null)
            {
                return group;
            }

            throw new IsuException("error");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return FindGroups(courseNumber).SelectMany(group => group.Students).ToList();
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groupList.Where(group => group.CourseNumber.Equals(courseNumber)).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group previousGroup = _groupList.Find(group => group.Students.Contains(student));
            newGroup.Students.Add(student);
            previousGroup.Students.Remove(student);
        }
    }
}