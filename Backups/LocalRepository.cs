using System.Collections.Generic;
using Ionic.Zip;

namespace Backups
{
    public class LocalRepository : IRepository
    {
        public LocalRepository()
        {
            Storages = new List<Storage>();
        }

        public List<Storage> Storages { get; }
        public List<Storage> CreateStorageZip(List<JobObject> jobObjects, IAlgorithm algorithm, string path, string id, Backupjob backupJob)
        {
            string changedPath = path;
            List<Storage> storages = backupJob.CreateStorages1(jobObjects, algorithm);
            Storages.AddRange(storages);
            foreach (Storage storage in storages)
            {
                var zipFile = new ZipFile();
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    zipFile.AddFile(jobObject.File.Path);
                    changedPath = $"{path}{jobObject.File.Name}-{id}.zip";
                }

                zipFile.Save(changedPath);
            }

            return storages;
        }
    }
}