using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backups
{
    public class VirtualRepository : IRepository
    {
        public void CreateStorageZip(List<JobObject> jobObjects, IAlgorithm algorithm, string path, string id, Backupjob backupJob, DateTime dateTime, RestorePoint restorePoint)
        {
            string changedPath = path;
            List<Storage> storages = restorePoint.Algorithm.CreateStorages(jobObjects);
            string newPath = $"{path}/{restorePoint.NameDirectory}";
            var directory = new DirectoryInfo(newPath);
            foreach (var jobObject in storages.SelectMany(storage => storage.JobObjects))
            {
                changedPath = $"{directory.FullName}{jobObject.File.Name}-{id}.zip";
                jobObject.File.Path = changedPath;
            }

            restorePoint.ListStorages.AddRange(storages);
        }
    }
}