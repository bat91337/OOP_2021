using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Backups
{
    public class LocalRepository : IRepository
    {
        public void CreateStorageZip(List<JobObject> jobObjects, IAlgorithm algorithm, string path, string id, Backupjob backupJob, DateTime dateTime, RestorePoint restorePoint)
        {
            string changedPath = path;
            
            List<Storage> storages = restorePoint.Algorithm.CreateStorages(jobObjects);
            string newPath = $"{path}/{restorePoint.NameDirectory}";
            var directory = new DirectoryInfo(newPath);
            directory.Create();
            foreach (Storage storage in storages)
            {
                var zipFile = new ZipFile();
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    zipFile.AddFile(jobObject.File.Path);
                    changedPath = $"{directory.FullName}{jobObject.File.Name}-{id}.zip";
                }

                zipFile.Save(changedPath);
            }

            restorePoint.ListStorages.AddRange(storages);
        }
    }
}