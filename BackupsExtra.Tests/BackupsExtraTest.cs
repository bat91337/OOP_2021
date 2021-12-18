using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
            var jobObject = new JobObject(@"../../../Files/FileB", "FileB");
            List<JobObject> jobObjects1 = _backupManager.AddJobObject(@"../../../Files/FileA", "FileA");
            jobObjects1.Add(jobObject);
            IAlgorithm split = new SplitAlgorithm();
            DateTime dateTime = DateTime.Now;
            ILogger ilogger = new LoggerConsole();
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            DateTime addDays = dateTime.AddDays(30);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, addDays);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, addDays);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, addDays);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, addDays);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, addDays);
            IDeleteRestorePoint deleteRestorePoint = new DeleteRestorePointByCountOrTime();
            DateTime addDays1 = dateTime.AddDays(31);
             Backupjob backupJob = _backupManager.GetBackupjob();
            _backupsExtraManager.Delete(deleteRestorePoint, backupJob, addDays1, 3);
            Assert.AreEqual(backupJob.RestorePoints.Count, 0);
        }
        
        [Test]
        public void MergeSingle()
        {
            var repository = new VirtualRepository();
            var jobObject = new JobObject(@"../../../Files/FileB", "FileB");
            List<JobObject> jobObjects1 = _backupManager.AddJobObject(@"../../../Files/FileA", "FileA");
            jobObjects1.Add(jobObject);
            IAlgorithm single = new SingleAlgorithm();
            IAlgorithm split = new SplitAlgorithm();
            DateTime dateTime = DateTime.Now;
            _backupManager.CreateBackup(single, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            _backupManager.CreateBackup(single, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            DateTime addDays = dateTime.AddDays(30);
            _backupManager.CreateBackup(single, @"../../../BackupFiles/", jobObjects1, repository, addDays);
            Backupjob backupJob = _backupManager.GetBackupjob();
            _backupsExtraManager.Merge4(backupJob);
            Assert.AreEqual(backupJob.RestorePoints.Count, 1);
        }
        [Test]
        public void MergeSplit()
        {
            var repository = new VirtualRepository();
            var jobObject = new JobObject(@"../../../Files/FileB", "FileB");
            List<JobObject> jobObjects1 = _backupManager.AddJobObject(@"../../../Files/FileA", "FileA");
            jobObjects1.Add(jobObject);
            List<JobObject> jobObjects2 = _backupManager.AddJobObject(@"../../../Files/FileA", "FileA");
            IAlgorithm single = new SingleAlgorithm();
            IAlgorithm split = new SplitAlgorithm();
            DateTime dateTime = DateTime.Now;
            _backupManager.CreateBackup(single, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            _backupManager.CreateBackup(single, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            dateTime.AddDays(30);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects2, repository, dateTime);
            Backupjob backupJob = _backupManager.GetBackupjob();
            _backupsExtraManager.Merge4(backupJob);
            int storageCount = 0;
            foreach (RestorePoint restorePoint in backupJob.RestorePoints)
            {
                 storageCount += restorePoint.ListStorages.Count;
            }
            Assert.AreEqual(storageCount, 4);
        }

        [Test]
        public void Serialization()
        {
            var repository = new VirtualRepository();
            var jobObject = new JobObject(@"../../../Files/FileB", "FileB");
            List<JobObject> jobObjects1 = _backupManager.AddJobObject(@"../../../Files/FileA", "FileA");
            jobObjects1.Add(jobObject);
            IAlgorithm split = new SplitAlgorithm();
            DateTime dateTime = DateTime.Now;
            RestorePoint restorePoint =
                _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            _backupsExtraManager.CreateRestorePointJson(restorePoint, @"../../../RestorePoint/File5.json");
            RestorePoint restorePoint2 = _backupsExtraManager.RestorePointJson(@"../../../RestorePoint/File5.json");
            Assert.AreEqual(restorePoint2.ListStorages.Count, 2);
        }
    }
}