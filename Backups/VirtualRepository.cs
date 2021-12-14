using System;
using System.Collections.Generic;

namespace Backups
{
    public class VirtualRepository : IRepository
    {
        public VirtualRepository()
        {
            Storages = new List<Storage>();
        }

        public List<Storage> Storages { get; }
        public List<Storage> CreateStorageZip(List<JobObject> jobObjects, IAlgorithm algorithm, string path, string id, Backupjob backupJob, DateTime dateTime)
        {
            List<Storage> storages = backupJob.CreateStorages1(jobObjects, algorithm, dateTime);
            Storages.AddRange(storages);
            foreach (Storage storage in storages)
            {
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    string changedPath = $"{path}-{id}.zip";
                    jobObject.File.Path = changedPath;
                }
            }

            return storages;
        }
    }
}