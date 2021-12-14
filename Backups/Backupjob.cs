using System;
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
        public List<Storage> CreateStorages1(List<JobObject> jobObjects, IAlgorithm algorithm, DateTime dateTime)
        {
            var restorePoints = new RestorePoint(algorithm, dateTime);
            List<Storage> storages = restorePoints.Algorithm.CreateStorages(jobObjects);
            return storages;
        }

        public void CreateRestorePoint(List<JobObject> jobObjects, IAlgorithm algorithm, Backupjob backupJob, DateTime dateTime)
        {
            List<Storage> storages = backupJob.CreateStorages1(jobObjects, algorithm, dateTime);
            var restorePoints = new RestorePoint(algorithm, dateTime);
            restorePoints.ListStorages.AddRange(storages);
            RestorePoints.Add(restorePoints);
        }
    }
}