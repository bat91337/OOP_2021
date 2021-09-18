using System;
using System.Collections.Generic;
using Isu.Models;
using Isu.Properties;
using Isu.Services;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<Group> _listgroup;
        private int _maxgroupsize;

        public IsuService(int maxgroupsize)
        {
            _listgroup = new List<Group>();
            _maxgroupsize = maxgroupsize;
        }

        public Group AddGroup(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new IsuException("error");
            }

            var addgroup = new Group(name);
            if (addgroup.CheckGroup(name))
            {
                _listgroup.Add(addgroup);
                return addgroup;
            }

            throw new IsuException("error");
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count >= _maxgroupsize)
            {
                throw new IsuException("error");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new IsuException("error");
            }

            foreach (var roup in _listgroup)
            {
                if (roup.NameGroup.Equals(group.NameGroup))
                {
                    var student = new Student(name);
                    roup.Students.Add(student);
                    return student;
                }
            }

            return null;
        }

        public Student GetStudent(int id)
        {
            foreach (var roup in _listgroup)
            {
                foreach (var student in roup.Students)
                {
                    if (student.Id == id)
                    {
                        return student;
                    }
                }
            }

            throw new IsuException("error");
        }

        public Student FindStudent(string name)
        {
            foreach (var group in _listgroup)
            {
                foreach (var student in group.Students)
                {
                    if (student.NameStudents.Equals(name))
                    {
                        return student;
                    }
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            List<Student> listStudent = new List<Student>();

            foreach (var group in _listgroup)
            {
                foreach (var student in group.Students)
                {
                    if (student.NameStudents.Equals(groupName))
                    {
                        listStudent.Add(student);
                    }
                }
            }

            return listStudent;
        }

        public Group FindGroup(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new IsuException("error");
            }

            foreach (var groups in _listgroup)
            {
                if (groupName.Equals(groups.NameGroup))
                {
                    return groups;
                }
            }

            throw new IsuException("error");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> listStudent = new List<Student>();
            foreach (var group in FindGroups(courseNumber))
            {
                listStudent.AddRange(group.Students);
            }

            return listStudent;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> listgroup = new List<Group>();
            foreach (var groups in _listgroup)
            {
                if (groups.CourseNumber.Equals(courseNumber))
                {
                    listgroup.Add(groups);
                }
            }

            return listgroup;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group previousGroup = _listgroup.Find(group => group.Students.Contains(student));
            newGroup.Students.Add(student);
            previousGroup.Students.Remove(student);
        }
    }
}