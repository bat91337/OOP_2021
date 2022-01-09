using System;
using System.Collections;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class DeleteRestorePointHybrid : IDeleteRestorePoint
    {
        public void DeleteRestorePoint(Backupjob backupJob, PredicateRestorePoint predicateRestorePoint)
        {
            if (predicateRestorePoint.Algorithm.Equals(true))
            {
                DeleteRestorePointCountOrTime(backupJob, predicateRestorePoint);
            }
            else
            {
                DeleteRestorePointByCountAndTime(backupJob, predicateRestorePoint);
            }
        }

        public void DeleteRestorePointCountOrTime(Backupjob backupJob, PredicateRestorePoint predicateRestorePoint)
        {
            if (DeleteByTime(backupJob, predicateRestorePoint.DateTime) || DeleteByCount(backupJob, predicateRestorePoint.Count))
            {
                int count = backupJob.RestorePoints.Count - predicateRestorePoint.Count;
                backupJob.RestorePoints.RemoveRange(0, count);
                int countList = 0;
                backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
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
            backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
            if (backupJob.RestorePoints.Count > countRestorPoint)
            {
                return true;
            }

            return false;
        }

        public void DeleteRestorePointByCountAndTime(Backupjob backupJob, PredicateRestorePoint predicateRestorePoint)
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
    }
}