using System;
using System.Collections.Generic;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class BackupsExtraManager : BackupManager
    {
        public void Delete(IDeleteRestorePoint delete, Backupjob backupjob, DateTime dateTime, int countRestorePoint)
        {
            delete.DeleteRestorePoint(backupjob, dateTime, countRestorePoint);
        }

        public void Merge20(Backupjob backupJob)
        {
            RestorePoint restorePoint2 = SearchRestorePointWithMinimalDate(backupJob);
            foreach (RestorePoint restorePoint1 in backupJob.RestorePoints)
            {
                if (restorePoint1.Algorithm is SingleAlgorithm)
                {
                    if (restorePoint2.Algorithm is SingleAlgorithm)
                    {
                        backupJob.RestorePoints.Remove(restorePoint1);
                        foreach (Storage storage1 in restorePoint1.ListStorages)
                        {
                            foreach (Storage storage2 in restorePoint2.ListStorages)
                            {
                                if (storage1.JobObjects.SequenceEqual(storage2.JobObjects))
                                {
                                    backupJob.RestorePoints.Remove(restorePoint1);
                                }
                                else
                                {
                                    IEnumerable<JobObject> result = storage1.JobObjects.Except(storage2.JobObjects);
                                    storage2.JobObjects.AddRange(result);
                                    backupJob.RestorePoints.Remove(restorePoint1);
                                }
                            }
                        }
                    }
                }
            }
        }

        public RestorePoint SearchRestorePointWithMinimalDate(Backupjob backupJob)
        {
            backupJob.RestorePoints.OrderByDescending(x => x.Date).ToList();
            RestorePoint restorePoint1 = backupJob.RestorePoints.First();
            return restorePoint1;
        }

        public void CreateStorage(Backupjob backupjob, List<JobObject> jobObjects, IAlgorithm algorithm, DateTime dateTime, ILogger logger)
        {
            backupjob.CreateStorages1(jobObjects, algorithm, dateTime);
            logger.Notify();
        }

        public void CreateRestorePoint(Backupjob backupjob, string path, List<JobObject> jobObjects, IAlgorithm algorithm, DateTime dateTime, ILogger logger)
        {
            backupjob.CreateRestorePoint(path, jobObjects, algorithm, dateTime);
            logger.Notify();
        }
    }
}