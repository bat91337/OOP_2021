using System.Collections.Generic;
using System.IO;
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

        public void CreateStorageZip(List<JobObject> jobObjects, IAlgorithm algorithm, string path, string id, Backupjob backupJob)
        {
            string changedPath = path;
            List<Storage> storages = backupJob.CreateStorages(jobObjects, algorithm);
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
    }
}