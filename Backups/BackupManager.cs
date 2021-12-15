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
            return BackupJob.AddJobObject(path, name);
        }

        public void RemoveJobObject(JobObject jobObject)
        {
             BackupJob.RemoveJobObject(jobObject);
        }

        public void CreateBackup(IAlgorithm algorithm, string path, List<JobObject> jobObjects, IRepository repository, DateTime dateTime)
        {
            var restorePoint = new RestorePoint(algorithm, dateTime, path);
            repository.CreateStorageZip(jobObjects, algorithm, path, _zipId, BackupJob, dateTime, restorePoint);
            BackupJob.RestorePoints.Add(restorePoint);
        }

        public Backupjob GetBackupJob()
        {
            return BackupJob;
        }
    }
}