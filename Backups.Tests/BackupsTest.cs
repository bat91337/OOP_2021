using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        private BackupManager _backupManager;

        [SetUp]
        public void Setup()
        {
            _backupManager = new BackupManager();
        }

        [Test]
        public void CreateSplitStorages()
        {
            var repository = new LocalRepository();
            var jobObject = new JobObject(@"../../../Files/FileB", "FileB");
            List<JobObject> jobObjects1 = _backupManager.AddJobObject(@"../../../Files/FileA", "FileA");
            jobObjects1.Add(jobObject);
            IAlgorithm split = new SplitAlgorithm();
            DateTime dateTime = DateTime.Now;
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            _backupManager.RemoveJobObject(jobObject);
            jobObjects1.Remove(jobObject);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            Backupjob backupjob = _backupManager.GetBackupjob();
            int storagesCount = 0;
            foreach (var restorePoints in backupjob.RestorePoints)
            {
                storagesCount += restorePoints.ListStorages.Count;
            }
            Assert.AreEqual(storagesCount, 3);
            Assert.AreEqual(_backupManager.GetBackupjob().RestorePoints.Count, 2);
        }
      
        [Test]
        public void CreateSingleStorages()
        {
            var repository = new LocalRepository();
            var jobObject = new JobObject(@"../../../Files/FileA", "FileA");
            List<JobObject> jobObjects = _backupManager.AddJobObject(@"../../../Files/FileB", "FileA");
            jobObjects.Add(jobObject);
            IAlgorithm single = new SingleAlgorithm();
            DateTime dateTime = DateTime.Now;
            _backupManager.CreateBackup(single, @"../../../BackupFiles/Single", jobObjects, repository, dateTime);
            Assert.AreEqual(_backupManager.GetBackupjob().RestorePoints.Count, 1);
        }
    }
}