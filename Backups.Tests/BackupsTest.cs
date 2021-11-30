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
            var repository = new Repository();
            var backupJob = new Backupjob();
            var jobObject = new JobObject("../../../Files/FileB");
            List<JobObject> jobObjects1 = _backupManager.AddJobObject("../../../Files/FileA");
            jobObjects1.Add(jobObject);
            IAlgorithm split = new SplitAlgorithm();
            _backupManager.CreateBackupVirtual(split, "../../../BackupFiles/", jobObjects1, repository, backupJob);
            _backupManager.RemoveJobObject(jobObject); 
            _backupManager.CreateBackupVirtual(split, "../../../BackupFiles/", jobObjects1, repository, backupJob);
            Assert.AreEqual(repository.Storages.Count, 3);
            Assert.AreEqual(backupJob.RestorePoints.Count, 2);
            
        }
    [Test]
    public void CreateSplitStorages1()
    {
        var repository = new Repository();
        var backupJob = new Backupjob();
        var jobObject = new JobObject("../../../Files/FileA");
        List<JobObject> jobObjects = _backupManager.AddJobObject("../../../Files/FileB");
        jobObjects.Add(jobObject);
        IAlgorithm single = new SingleAlgorithm();
        _backupManager.CreateBackupVirtual(single, "../../../BackupFiles/Single", jobObjects, repository, backupJob);
        Assert.AreEqual(repository.Storages.Count, 1);
    }
     }
}