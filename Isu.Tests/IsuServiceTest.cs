using System.Linq;
using Isu.Models;
using Isu.Properties;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            
            _isuService = new IsuService(20);
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3105");
            Student student = _isuService.AddStudent(group,"name");

            Assert.Contains(student, group.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3206");
                for (int i = 0; i < 100; i++)
                {
                    _isuService.AddStudent(group, "Семен");
                }

            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group20 = _isuService.AddGroup("T3302");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group = _isuService.AddGroup("M3105");
            Student student = _isuService.AddStudent(group,"name");
            _isuService.ChangeStudentGroup(student , group);
            Assert.True(_isuService.FindGroup("M3105").Students.All(studentTemp => studentTemp == student));
        }
    }
}