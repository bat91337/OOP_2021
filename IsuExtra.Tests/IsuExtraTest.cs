using System;
using NUnit.Framework;
using Isu.Models;
using Isu.Properties;

namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private IsuExtraService _isuExtraService;

        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();

        }

        [Test]
        public void AddNewOgnp_NewOgnpHasBeenAdd()
        {
            Faculty faculty = _isuExtraService.AddFaculty("ФиТИП", "ОГНП", 'M');
           Discipline discipline = _isuExtraService.AddDiscipline("discipline", faculty);
           Assert.Contains(discipline, faculty.Disciplines);

        }

        [Test]
        public void RecordingStudentInOGNP_StudentRecordedInOGNP()
        {
            Group group = _isuExtraService.AddGroup("V3208");
            Student student = _isuExtraService.AddStudent("Дорошенко Семен", group);
            Faculty faculty = _isuExtraService.AddFaculty("ФиТИП", "ОГНП", 'M');
            ScheduleGroup scheduleGroup = _isuExtraService.AddScheduleGroup(group);
            Day day = _isuExtraService.AddDayInScheduleGroup(DayOfWeek.Friday, scheduleGroup);
            ScheduleFaculty scheduleFaculty = _isuExtraService.AddScheduleFaculty(faculty);
            Day dayFaculty = _isuExtraService.AddDayInScheduleFaculty(DayOfWeek.Friday, scheduleFaculty);
            _isuExtraService.AddScheduleFaculty(faculty);
            _isuExtraService.AddScheduleGroup(group);
            _isuExtraService.AddGroupPairInSchedule(group,"maths", "123", DayOfWeek.Friday,1);
            _isuExtraService.AddDisciplinePair(faculty, "кибербез", "234",2,DayOfWeek.Friday);
            _isuExtraService.RecordingStudentInOgnp(faculty, student);
            Assert.Contains(student, faculty.Students);
        }

        [Test]
        public void StudentRemoveFromOgnp_StudentRemovedFromOgnp()
        {
            Group group = _isuExtraService.AddGroup("V3208");
            Student student = _isuExtraService.AddStudent("Дорошенко Семен", group);
            Faculty faculty = _isuExtraService.AddFaculty("ФиТИП", "ОГНП", 'M');
            ScheduleGroup scheduleGroup = _isuExtraService.AddScheduleGroup(group);
            Day day = _isuExtraService.AddDayInScheduleGroup(DayOfWeek.Friday, scheduleGroup);
            ScheduleFaculty scheduleFaculty = _isuExtraService.AddScheduleFaculty(faculty);
            Day dayFaculty = _isuExtraService.AddDayInScheduleFaculty(DayOfWeek.Friday, scheduleFaculty);
            _isuExtraService.AddScheduleFaculty(faculty);
            _isuExtraService.AddScheduleGroup(group);
            _isuExtraService.AddGroupPairInSchedule(group,"maths", "123", DayOfWeek.Friday,1);
            _isuExtraService.AddDisciplinePair(faculty, "кибербез", "234",2,DayOfWeek.Friday);
            _isuExtraService.RemoveStudentFromOgnp(faculty, student);
            Assert.Catch<Exception>(() =>
            {
                Assert.Contains(student, faculty.Students);
            });
        }
    }
}