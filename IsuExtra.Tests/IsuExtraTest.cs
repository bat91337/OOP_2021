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
            Faculty faculty = _isuExtraService.AddFaculty("ФиТИП", 'M');
            Ognp ognp = _isuExtraService.AddOgnpInFaculty("КиберБез", faculty);
            Discipline discipline = _isuExtraService.AddDiscipline("discipline", faculty, ognp);
            Assert.Contains(discipline, ognp.Disciplines);

        }
        
        [Test]
        public void RecordingStudentInOGNP_StudentRecordedInOGNP()
        {
            Group group = _isuExtraService.AddGroup("V3208");
            Student student = _isuExtraService.AddStudent("Дорошенко Семен", group);
            Faculty faculty = _isuExtraService.AddFaculty("ФиТИП", 'M');
            Ognp ognp = _isuExtraService.AddOgnpInFaculty("КиберБез", faculty);
            ScheduleGroup scheduleGroup = _isuExtraService.AddScheduleGroup(group);
            Day day = _isuExtraService.AddDayInScheduleGroup(DayOfWeek.Friday, scheduleGroup);
            ScheduleFaculty scheduleFaculty = _isuExtraService.AddScheduleFaculty(faculty);
            Day dayFaculty = _isuExtraService.AddDayInScheduleFaculty(DayOfWeek.Friday, scheduleFaculty);
            _isuExtraService.AddScheduleFaculty(faculty);
            _isuExtraService.AddScheduleGroup(group);
            _isuExtraService.AddGroupPairInSchedule(group,"maths", "123", DayOfWeek.Friday,1,"M3208","daw");
            _isuExtraService.AddDisciplinePair(faculty, "кибербез", "234",2,DayOfWeek.Friday, "M3208","daw");
            _isuExtraService.RecordingStudentInOgnp(faculty, student, ognp);
            Assert.Contains(student, ognp.Students);
        }
        
        [Test]
        public void StudentRemoveFromOgnp_StudentRemovedFromOgnp()
        {
            Group group = _isuExtraService.AddGroup("V3208");
            Student student = _isuExtraService.AddStudent("Дорошенко Семен", group);
            Faculty faculty = _isuExtraService.AddFaculty("ФиТИП", 'M');
            Ognp ognp = _isuExtraService.AddOgnpInFaculty("КиберБез", faculty);
            ScheduleGroup scheduleGroup = _isuExtraService.AddScheduleGroup(group);
            Day day = _isuExtraService.AddDayInScheduleGroup(DayOfWeek.Friday, scheduleGroup);
            ScheduleFaculty scheduleFaculty = _isuExtraService.AddScheduleFaculty(faculty);
            Day dayFaculty = _isuExtraService.AddDayInScheduleFaculty(DayOfWeek.Friday, scheduleFaculty);
            _isuExtraService.AddScheduleFaculty(faculty);
            _isuExtraService.AddScheduleGroup(group);
            _isuExtraService.AddGroupPairInSchedule(group,"maths", "123", DayOfWeek.Friday,1, "M3208","daw");
            _isuExtraService.AddDisciplinePair(faculty, "кибербез", "234",2,DayOfWeek.Friday, "M3208","daw");
            _isuExtraService.RemoveStudentFromOgnp(faculty, student, ognp);
            Assert.Catch<Exception>(() =>
            {
                Assert.Contains(student, ognp.Students);
            });
        }
    }
}