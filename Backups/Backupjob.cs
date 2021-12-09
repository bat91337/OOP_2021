using System.Collections.Generic;
namespace Backups
{
    public class Backupjob
    {
        public Backupjob()
        {
            JobObjects = new List<JobObject>();
            RestorePoints = new List<RestorePoint>();
        }

        public List<JobObject> JobObjects { get; }
        public List<RestorePoint> RestorePoints { get; }
        public List<Storage> CreateStorages(List<JobObject> jobObjects, IAlgorithm algorithm)
        {
            var restorePoints = new RestorePoint(algorithm);
            List<Storage> storages = restorePoints.Algorithm.CreateStorages(jobObjects);
            return storages;
        }

        public void CreateRestorePoint(string path, List<JobObject> jobObjects, IAlgorithm algorithm)
        {
            var backupJob = new Backupjob();
            List<Storage> storages = backupJob.CreateStorages(jobObjects, algorithm);
            var restorePoints = new RestorePoint(algorithm);
            restorePoints.ListStorages.AddRange(storages);
            RestorePoints.Add(restorePoints);
        }
    }
}