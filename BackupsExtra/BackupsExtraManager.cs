using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Backups;

// using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BackupsExtra
{
    public class BackupsExtraManager : BackupManager
    {
        private BackupManager _backupManager;
        public BackupsExtraManager()
        {
            _backupManager = new BackupManager();
        }

        public void Delete(IDeleteRestorePoint delete, Backupjob backupjob, DateTime dateTime, int countRestorePoint)
        {
            delete.DeleteRestorePoint(backupjob, dateTime, countRestorePoint);
        }

        public List<RestorePoint> SearchSingleAlgorithm(List<RestorePoint> restorePoints)
        {
            var restorePoints1 = new List<RestorePoint>();
            foreach (var restorePoint in restorePoints)
            {
                if (restorePoint.Algorithm is SingleAlgorithm)
                {
                    restorePoints1.Add(restorePoint);
                }
            }

            return restorePoints1;
        }

        public void DeleteRestorePoints(List<RestorePoint> restorePoints, Backupjob backupJob)
        {
            foreach (RestorePoint restorePoint in restorePoints)
            {
                backupJob.RestorePoints.Remove(restorePoint);
            }
        }

        public void Merge(Backupjob backupJob)
        {
            var restorePoints = new List<RestorePoint>();
            RestorePoint restorePointNew = backupJob.RestorePoints.Last();
            backupJob.RestorePoints.Remove(restorePointNew);
            if (restorePointNew.Algorithm is SingleAlgorithm)
            {
                foreach (RestorePoint restorePoint in backupJob.RestorePoints)
                {
                    restorePoints.Add(restorePoint);
                }

                DeleteRestorePoints(restorePoints, backupJob);
            }
            else
            {
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
            }

            backupJob.RestorePoints.Add(restorePointNew);
        }

        public void CreateBackupExtra(IAlgorithm algorithm, string path, List<JobObject> jobObjects, IRepository repository, DateTime dateTime, ILogger logger)
        {
            _backupManager.CreateBackup(algorithm, path, jobObjects, repository, dateTime);
            logger.Notify();
        }

        public void CreateRestorePointJson(RestorePoint restorePoint, string path)
        {
            string json = JsonConvert.SerializeObject(restorePoint, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });

            File.WriteAllText(path, json);
        }

        public RestorePoint RestorePointJson(string path)
        {
            string jsonM = File.ReadAllText(path);
            RestorePoint restorePoint = JsonConvert.DeserializeObject<RestorePoint>(jsonM, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });

            return restorePoint;
        }
    }
}