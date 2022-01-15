using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups;
using Newtonsoft.Json;

namespace BackupsExtra
{
    public class BackupsExtraManager : BackupManager
    {
        private BackupManager _backupManager;
        public BackupsExtraManager()
        {
            _backupManager = new BackupManager();
        }

        public void Delete(IDeleteRestorePoint delete, Backupjob backupjob, PredicateRestorePoint restorePoint)
        {
            delete.DeleteRestorePoint(backupjob, restorePoint);
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

        public void CreateBackupExtra(IAlgorithm algorithm, string path, List<JobObject> jobObjects, IRepository repository, DateTime dateTime, ILogger logger, string pathNotify)
        {
           RestorePoint restorePoint = _backupManager.CreateBackup(algorithm, path, jobObjects, repository, dateTime);
           logger.Notify(pathNotify, restorePoint);
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