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

        public virtual void CreateBackup(IAlgorithm algorithm, string path, List<JobObject> jobObjects, IRepository repository, Backupjob backupJob, DateTime dateTime)
        {
            repository.CreateStorageZip(jobObjects, algorithm, path, _zipId, backupJob, dateTime);
            backupJob.CreateRestorePoint(path, jobObjects, algorithm, dateTime);
        }

        // public void CreateBackupVirtual(IAlgorithm algorithm, string path, List<JobObject> jobObjects, IRepository repository, Backupjob backupJob)
        // {
        //     repository.CreateStorageZip(jobObjects, algorithm, path, _zipId);
        //     backupJob.CreateRestorePoint(path, jobObjects, algorithm);
        // }
    }
}