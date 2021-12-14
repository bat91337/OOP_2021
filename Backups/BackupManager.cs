using System;
using System.Collections.Generic;

namespace Backups
{
    public class BackupManager
    {
        private static string _zipId;
        public BackupManager()
        {
            BackupJob = new Backupjob();
            _zipId = Guid.NewGuid().ToString();
        }

        private Backupjob BackupJob { get; }

        public List<JobObject> AddJobObject(string path, string name)
        {
            var jobObject = new JobObject(path, name);
            BackupJob.JobObjects.Add(jobObject);
            return BackupJob.JobObjects;
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            BackupJob.JobObjects.Remove(jobObject);
        }

        public void CreateBackup(IAlgorithm algorithm, string path, List<JobObject> jobObjects, IRepository repository, DateTime dateTime)
        {
            repository.CreateStorageZip(jobObjects, algorithm, path, _zipId, BackupJob, dateTime);
            BackupJob.CreateRestorePoint(jobObjects, algorithm, BackupJob, dateTime);
        }

        public Backupjob GetBackupJob()
        {
            return BackupJob;
        }
    }
}