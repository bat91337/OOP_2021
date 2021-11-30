using System.Collections.Generic;

namespace Backups
{
    public class BackupManager
    {
        private static int _zipId;
        public BackupManager()
        {
            BackupJob = new Backupjob();
            _zipId = 0;
        }

        private Backupjob BackupJob { get; }

        public List<JobObject> AddJobObject(string path)
        {
            var jobObject = new JobObject(path);
            BackupJob.JobObjects.Add(jobObject);
            return BackupJob.JobObjects;
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            BackupJob.JobObjects.Remove(jobObject);
        }

        public void CreateBackup(IAlgorithm algorithm, string path, List<JobObject> jobObjects, Repository repository, Backupjob backupJob)
        {
            repository.CreateStorageZip(jobObjects, algorithm, path, _zipId++, backupJob);
            backupJob.CreateRestorePoint(path, jobObjects, algorithm);
        }

        public void CreateBackupVirtual(IAlgorithm algorithm, string path, List<JobObject> jobObjects, Repository repository, Backupjob backupJob)
        {
            repository.CreateStorageZipVirtual(jobObjects, algorithm, path, _zipId++, backupJob);
            backupJob.CreateRestorePoint(path, jobObjects, algorithm);
        }
    }
}