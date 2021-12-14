using System;
using System.Collections;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class DeleteRestorePointByCountOrTime : IDeleteRestorePoint
    {
        public void DeleteRestorePoint(Backupjob backupJob, DateTime dateTime, int countRestorePoint)
        {
            backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
            if (DeleteByTime(backupJob, dateTime) ^ DeleteByCount(backupJob, countRestorePoint))
            {
                int count = backupJob.RestorePoints.Count - countRestorePoint;
                backupJob.RestorePoints.RemoveRange(0, count);
                int countList = 0;
                backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
                foreach (RestorePoint restorePoint in backupJob.RestorePoints)
                {
                    if (restorePoint.Date < dateTime)
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
            backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
            if (backupJob.RestorePoints.Count > countRestorPoint)
            {
                return true;
            }

            return false;
        }
    }
}