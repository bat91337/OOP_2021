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

        public Ognp AddOgnpInFaculty(string nameOgnp, Faculty faculty)
        {
            if (_faculties.Any(faculty1 => faculty1.NameFaculty.Equals(faculty.NameFaculty)))
            {
                Ognp ognp = new Ognp(nameOgnp);
                faculty.Ognp.Add(ognp);
                return ognp;
            }

            return new Ognp(nameOgnp);
        }

        public void AddGroupPairInSchedule(Group group, string pairName, string classRoom, DayOfWeek pairDay, int pairNumber, string groupName, string teacherName)
        {
            foreach (ScheduleGroup scheduleGroup in _scheduleGroups)
            {
                if (group.GroupName.Equals(scheduleGroup.Group.GroupName))
                {
                    foreach (Day day in scheduleGroup.Schedule.Days)
                    {
                        if (pairDay.Equals(day.DayOfWeek))
                        {
                            var pair = new Pair(pairNumber, pairName, classRoom, groupName, teacherName);
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

        public Faculty AddFaculty(string nameFaculty, char firstLetter)
        {
            var faculty = new Faculty(nameFaculty, firstLetter);
            _faculties.Add(faculty);
            return faculty;
        }

        public Discipline AddDiscipline(string nameDiscipline, Faculty nameFaculty, Ognp ognp)
        {
            if (_faculties.Any(faculty => faculty.NameFaculty.Equals(nameFaculty.NameFaculty)))
            {
                var discipline = new Discipline(nameDiscipline);
                ognp.Disciplines.Add(discipline);
                return discipline;
            }

            return null;
        }

        public void AddDisciplinePair(Faculty nameFaculty, string nameDiscipline, string classRoom, int pairNumber, DayOfWeek pairDay, string groupName, string teacherName)
        {
            ScheduleFaculty scheduleFaculty = _scheduleFaculties.FirstOrDefault(scheduleFaculty => scheduleFaculty.Faculty.NameFaculty.Equals(nameFaculty.NameFaculty));
            Day day = scheduleFaculty.Schedule.Days.FirstOrDefault(day => day.DayOfWeek.Equals(pairDay));
            var pair = new Pair(pairNumber, nameDiscipline, classRoom, groupName, teacherName);
            day.Pairs.Add(pair);
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

        public void RecordingStudentInOgnp(Faculty nameFaculty, Student student, Ognp ognp)
        {
            string groupName = GetGroupName(student);
            char firstLetter = char.Parse(groupName.Substring(0, 1));
            if (!CheckSchedule(groupName, nameFaculty))
            {
                foreach (Faculty faculty in _faculties.Where(faculty => faculty.NameFaculty.Equals(nameFaculty.NameFaculty)))
                {
                    if (!faculty.FirstLetter.Equals(firstLetter))
                    {
                        Ognp ognp1 = faculty.Ognp.FirstOrDefault(ognp1 => ognp1.NameOgnp.Equals(ognp.NameOgnp));
                        ognp1.Students.Add(student);
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

        public void RemoveStudentFromOgnp(Faculty nameFaculty, Student student, Ognp ognp)
        {
            Faculty faculty = _faculties.FirstOrDefault(faculty => faculty.NameFaculty.Equals(nameFaculty.NameFaculty));
            Ognp ognp1 = faculty.Ognp.FirstOrDefault(ognp1 => ognp1.NameOgnp.Equals(ognp.NameOgnp));
            foreach (Student students in ognp1.Students.Where(students => students.StudentsName.Equals(student.StudentsName)))
            {
                ognp1.Students.Remove(student);
            }
        }

        public List<Ognp> FindGroupOgnp(Ognp ognp)
        {
            var listFaculties = new List<Ognp>();
            foreach (Faculty faculty in _faculties)
            {
                Ognp ognp1 = faculty.Ognp.FirstOrDefault(ognp1 => ognp1.NameOgnp.Equals(ognp.NameOgnp));
                listFaculties.Add(ognp1);
            }

            return listFaculties;
        }

        public List<Student> FindStudentsInOgnp(Faculty faculty, Ognp ognp)
        {
            var listStudent = new List<Student>();
            Ognp ognp1 = faculty.Ognp.FirstOrDefault(ognp1 => ognp1.NameOgnp.Equals(ognp.NameOgnp));
            foreach (Student students in ognp1.Students)
            {
                listStudent.Add(students);
            }

            return listStudent;
        }

        public IEnumerable<Student> GetListNotRecordedStudent()
        {
            foreach (Group group in _isuService.GetListGroup())
            {
                foreach (IEnumerable<Student> result in _faculties.SelectMany(faculty => faculty.Ognp.Select(ognp => @group.Students.Except(ognp.Students))))
                {
                    return result;
                }
            }

            return null;
        }

        public Group AddGroup(string nameGroup)
        {
            return _isuService.AddGroup(nameGroup);
        }

        public Student AddStudent(string studentName, Group group)
        {
            return _isuService.AddStudent(@group, studentName);
        }

        private bool CheckSchedule(string groupName, Faculty nameFaculty)
        {
            ScheduleGroup scheduleGroup = _scheduleGroups.FirstOrDefault(scheduleGroup => scheduleGroup.Group.GroupName.Equals(groupName));
            foreach (ScheduleFaculty scheduleFaculty in _scheduleFaculties.Where(scheduleFaculty => scheduleFaculty.Faculty.NameFaculty.Equals(nameFaculty.NameFaculty)))
            {
                if (scheduleFaculty.Schedule.CheckShceduleDay(scheduleGroup))
                {
                    return true;
                }
            }

            return false;
        }

        private string GetGroupName(Student student)
        {
            foreach (Group group in _isuService.GetListGroup().Where(group => group.Students.Any(students => students.Id.Equals(student.Id))))
            {
                return group.GroupName;
            }

            return null;
        }
    }
}