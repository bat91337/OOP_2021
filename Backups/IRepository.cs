using System;
using System.Collections.Generic;

namespace Backups
{
    public interface IRepository
    {
        public void CreateStorageZip(List<JobObject> jobObjects, IAlgorithm algorithm, string path, string id, Backupjob backupJob, DateTime dateTime, RestorePoint restorePoint);
    }
}