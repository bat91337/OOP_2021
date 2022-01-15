using System.Collections.Generic;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class MergeByTime
    {
        public void Merge(Backupjob backupJob)
        {
            var restorePoints = new List<RestorePoint>();
            RestorePoint restorePointNew = backupJob.RestorePoints.Last();
            backupJob.RestorePoints.Remove(restorePointNew);
            foreach (RestorePoint restorePoint in backupJob.RestorePoints)
            {
                foreach (Storage storage in restorePoint.ListStorages)
                {
                    foreach (Storage storageNew in restorePointNew.ListStorages)
                    {
                        if (storage.JobObjects.SequenceEqual(storageNew.JobObjects))
                        {
                            restorePoints.Add(restorePoint);
                        }
                        else
                        {
                            IEnumerable<JobObject> result = storage.JobObjects.Except(storageNew.JobObjects);
                            storageNew.JobObjects.AddRange(result);
                        }
                    }
                }
            }

            DeleteRestorePoints(restorePoints, backupJob);
            backupJob.RestorePoints.Add(restorePointNew);
        }

        public void DeleteRestorePoints(List<RestorePoint> restorePoints, Backupjob backupJob)
        {
            foreach (RestorePoint restorePoint in restorePoints)
            {
                backupJob.RestorePoints.Remove(restorePoint);
            }
        }
    }
}