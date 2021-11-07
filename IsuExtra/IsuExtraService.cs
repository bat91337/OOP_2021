using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;
using Isu.Properties;
using Isu.Services;
using Isu.Tools;

namespace IsuExtra
{
    public class IsuExtraService
    {
        private readonly List<Faculty> _faculties;
        private readonly IsuService _isuService;
        private readonly List<ScheduleGroup> _scheduleGroups;
        private readonly List<ScheduleFaculty> _scheduleFaculties;
        public IsuExtraService()
        {
            _faculties = new List<Faculty>();
            _scheduleGroups = new List<ScheduleGroup>();
            _isuService = new IsuService(20);
            _scheduleFaculties = new List<ScheduleFaculty>();
        }

        public void AddGroupPairInSchedule(Group group, string pairName, string classRoom, DayOfWeek pairDay, int pairNumber)
        {
            foreach (ScheduleGroup scheduleGroup in _scheduleGroups)
            {
                if (group.GroupName.Equals(scheduleGroup.Group.GroupName))
                {
                    foreach (Day day in scheduleGroup.Schedule.Days)
                    {
                        if (pairDay.Equals(day.DayOfWeek))
                        {
                                    var pair = new Pair(pairNumber, pairName, classRoom);
                                    day.Pairs.Add(pair);
                        }
                    }
                }
            }
        }

        public ScheduleGroup AddScheduleGroup(Group nameGroup)
        {
            var schedule = new ScheduleGroup(nameGroup);
            _scheduleGroups.Add(schedule);
            return schedule;
        }

        public Faculty AddFaculty(string nameFaculty, string nameOgnp, char firstLetter)
        {
            var faculty = new Faculty(nameFaculty, nameOgnp, firstLetter);
            _faculties.Add(faculty);
            return faculty;
        }

        public Discipline AddDiscipline(string nameDiscipline, Faculty nameFaculty)
        {
            foreach (Faculty faculty in _faculties)
            {
                if (faculty.NameFaculty.Equals(nameFaculty.NameFaculty))
                {
                    {
                        var discipline = new Discipline(nameDiscipline);
                        faculty.Disciplines.Add(discipline);
                        return discipline;
                    }
                }
            }

            return null;
        }

        public void AddDisciplinePair(Faculty nameFaculty, string nameDiscipline, string classRoom, int pairNumber, DayOfWeek pairDays)
        {
            foreach (ScheduleFaculty scheduleFaculty in _scheduleFaculties)
            {
                if (scheduleFaculty.Faculty.NameFaculty.Equals(nameFaculty.NameFaculty))
                {
                    foreach (Day day in scheduleFaculty.Schedule.Days)
                    {
                        if (pairDays.Equals(day.DayOfWeek))
                        {
                            var pair = new Pair(pairNumber, nameDiscipline, classRoom);
                            day.Pairs.Add(pair);
                        }
                    }
                }
            }
        }

        public Day AddDayInScheduleGroup(DayOfWeek dayofweek, ScheduleGroup group)
        {
            foreach (ScheduleGroup scheduleGroup in _scheduleGroups)
            {
                if (scheduleGroup.Group.GroupName.Equals(group.Group.GroupName))
                {
                    var day = new Day(dayofweek);
                    scheduleGroup.Schedule.Days.Add(day);
                    return day;
                }
            }

            return null;
        }

        public ScheduleFaculty AddScheduleFaculty(Faculty faculty)
        {
            var schedule = new ScheduleFaculty(faculty);
            _scheduleFaculties.Add(schedule);
            return schedule;
        }

        public Day AddDayInScheduleFaculty(DayOfWeek dayOfWeek, ScheduleFaculty schedulefacultu)
        {
            foreach (ScheduleFaculty scheduleFaculty in _scheduleFaculties)
            {
                if (scheduleFaculty.Faculty.NameFaculty.Equals(schedulefacultu.Faculty.NameFaculty))
                {
                    var day = new Day(dayOfWeek);
                    scheduleFaculty.Schedule.Days.Add(day);
                    return day;
                }
            }

            return null;
        }

        public void RecordingStudentInOgnp(Faculty nameFaculty, Student student)
        {
            string groupName = GetGroupName(student);
            char firstLetter = char.Parse(groupName.Substring(0, 1));
            if (!CheckSchedule(groupName, nameFaculty))
            {
                foreach (Faculty faculty in _faculties.Where(faculty => faculty.NameFaculty.Equals(nameFaculty.NameFaculty)))
                {
                    if (!faculty.FirstLetter.Equals(firstLetter))
                    {
                        faculty.Students.Add(student);
                    }
                    else
                    {
                        throw new IsuExtraException("you cannot enroll in the OGNP of your faculty");
                    }
                }
            }
            else
            {
                throw new IsuExtraException("error");
            }
        }

        public void RemoveStudentFromOgnp(Faculty nameFaculty, Student student)
        {
            foreach (Faculty faculty in _faculties)
            {
                if (faculty.Equals(nameFaculty))
                {
                    foreach (Student students in faculty.Students.Where(students => students.StudentsName.Equals(student.StudentsName)))
                    {
                        faculty.Students.Remove(student);
                    }
                }
            }
        }

        public List<Faculty> FindStudentsInOgnp()
        {
            var listFaculties = new List<Faculty>();
            foreach (Faculty faculty in _faculties)
            {
                foreach (Student students in faculty.Students)
                {
                    faculty.Students.Add(students);
                }
            }

            return listFaculties;
        }

        public List<Student> FindStudentsInOgnp(Faculty faculty)
        {
            var listStudent = new List<Student>();
            foreach (Faculty faculties in _faculties)
            {
                if (faculties.Equals(faculty))
                {
                    foreach (Student students in faculties.Students)
                    {
                        faculties.Students.Add(students);
                    }
                }
            }

            return listStudent;
        }

        public IEnumerable<Student> GetListNotRecordedStudent()
        {
            foreach (Group group in _isuService.GetListGroup())
            {
                foreach (IEnumerable<Student> result in _faculties.Select(faculty => @group.Students.Except(faculty.Students)))
                {
                    return result;
                }
            }

            return null;
        }

        public Group AddGroup(string nameGroup)
        {
            Group group = _isuService.AddGroup(nameGroup);
            return group;
        }

        public Student AddStudent(string studentName, Group group)
        {
            Student student = _isuService.AddStudent(@group, studentName);

            return student;
        }

        private bool CheckSchedule(string groupName, Faculty nameFaculty)
        {
            foreach (ScheduleGroup scheduleGroup in _scheduleGroups)
            {
                if (scheduleGroup.Group.GroupName.Equals(groupName))
                {
                    foreach (ScheduleFaculty scheduleFaculty in _scheduleFaculties.Where(scheduleFaculty => scheduleFaculty.Faculty.NameFaculty.Equals(nameFaculty.NameFaculty)))
                    {
                        if (scheduleFaculty.Schedule.CheckShceduleDays(scheduleGroup))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private string GetGroupName(Student student)
        {
            foreach (Group group in _isuService.GetListGroup())
            {
                if (@group.Students.Any(students => students.Id.Equals(student.Id)))
                {
                    return @group.GroupName;
                }
            }

            return null;
        }
    }
}