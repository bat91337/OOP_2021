using System;
using System.Collections.Generic;
using System.IO;

namespace Backups
{
    public class VirtualRepository : IRepository
    {
        public void CreateStorageZip(List<JobObject> jobObjects, IAlgorithm algorithm, string path, string id, Backupjob backupJob, DateTime dateTime, RestorePoint restorePoint)
        {
            Storages = new List<Storage>();
        }

        public List<Storage> Storages { get; }
        public void CreateStorageZip(List<JobObject> jobObjects, IAlgorithm algorithm, string path, string id, Backupjob backupJob, DateTime dateTime, RestorePoint restorePoint)
        {
            string changedPath = path;
            List<Storage> storages = restorePoint.Algorithm.CreateStorages(jobObjects);
            string newPath = $"{path}/{restorePoint.NameDirectory}";
            var directory = new DirectoryInfo(newPath);
            foreach (Storage storage in Storages)
            {
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    changedPath = $"{directory.FullName}{jobObject.File.Name}-{id}.zip";
                    jobObject.File.Path = changedPath;
                }
            }

            restorePoint.ListStorages.AddRange(storages);
        }
    }
}