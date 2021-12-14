using System;
using System.Collections.Generic;
using Backups;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTest
    {
        private BackupManager _backupManager;
        private BackupsExtraManager _backupsExtraManager;

        [SetUp]
        public void Setup()
        {
            _backupManager = new BackupManager();
            _backupsExtraManager = new BackupsExtraManager();
        }

        [Test]
        public void CreateSplitStorages()
        {
            var repository = new VirtualRepository();
            var backupJob = new Backupjob();
            var jobObject = new JobObject(@"../../../Files/FileB", "FileB");
            List<JobObject> jobObjects1 = _backupManager.AddJobObject(@"../../../Files/FileA", "FileA");
            jobObjects1.Add(jobObject);
            IAlgorithm split = new SplitAlgorithm();
            DateTime dateTime = DateTime.Now;
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, backupJob, dateTime);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, backupJob, dateTime);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, backupJob, dateTime);
            DateTime addDays = dateTime.AddDays(30);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, backupJob, addDays);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, backupJob, addDays);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, backupJob, addDays);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, backupJob, addDays);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, backupJob, addDays);
            IDeleteRestorePoint deleteRestorePoint = new DeleteRestorePointByCountOrTime();
            DateTime addDays1 = dateTime.AddDays(31);
            _backupsExtraManager.Delete(deleteRestorePoint, backupJob, addDays1, 3);
            Assert.AreEqual(backupJob.RestorePoints.Count, 5);
        }
    }
}