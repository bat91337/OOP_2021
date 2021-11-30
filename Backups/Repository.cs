using System;
using System.Collections.Generic;
using Ionic.Zip;

namespace Backups
{
    public class Repository
    {
        public Repository()
        {
            Storages = new List<Storage>();
        }

        public List<Storage> Storages { get; }

        public void CreateStorageZip(List<JobObject> jobObjects, IAlgorithm algorithm, string path, int id, Backupjob backupJob)
        {
            string changedPath = path;
            List<Storage> storages = backupJob.CreateStorages1(jobObjects, algorithm);
            Storages.AddRange(storages);
            foreach (Storage storage in storages)
            {
                var zipFile = new ZipFile();
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    zipFile.AddFile(jobObject.File.FullName);
                    changedPath = $"{path}{jobObject.File.Name}-{id}.zip";
                }

                zipFile.Save(changedPath);
            }
        }

        public List<Storage> CreateStorageZipVirtual(List<JobObject> jobObjects, IAlgorithm algorithm, string path, int id, Backupjob backupJob)
        {
            string changedPath = path;
            List<Storage> storages = backupJob.CreateStorages1(jobObjects, algorithm);
            Storages.AddRange(storages);
            foreach (Storage storage in storages)
            {
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    var jobObject1 = new JobObject(jobObject.File.FullName);
                    changedPath = $"{path}{jobObject1.File.Name}-{id}.zip";
                }
            }

            return storages;
        }
    }
}