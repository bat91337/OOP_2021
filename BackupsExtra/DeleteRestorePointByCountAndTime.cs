using System;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class DeleteRestorePointByCountAndTime : IDeleteRestorePoint
    {
        public void DeleteRestorePoint(Backupjob backupJob, PredicateRestorePoint predicateRestorePoint)
        {
            backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
            if (DeleteByTime(backupJob, predicateRestorePoint.DateTime) && DeleteByCount(backupJob, predicateRestorePoint.Count))
            {
                int count = backupJob.RestorePoints.Count - predicateRestorePoint.Count;
                backupJob.RestorePoints.RemoveRange(0, count);
                backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
                int countList = 0;
                foreach (RestorePoint restorePoint in backupJob.RestorePoints)
                {
                    if (restorePoint.Date < predicateRestorePoint.DateTime)
                    {
                        countList++;
                    }
                    else
                    {
                        break;
                    }
                }

                backupJob.RestorePoints.RemoveRange(0, countList);
            }
        }

        public bool DeleteByTime(Backupjob backupJob, DateTime dateTime)
        {
            backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
            foreach (RestorePoint restorePoint in backupJob.RestorePoints)
            {
                if (restorePoint.Date > dateTime)
                {
                    return true;
                }
            }

            return false;
        }

        public bool DeleteByCount(Backupjob backupJob, int countRestorPoint)
        {
            var sorted = backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
            if (sorted.Count > countRestorPoint)
            {
                return true;
            }

            return false;
        }
    }
}