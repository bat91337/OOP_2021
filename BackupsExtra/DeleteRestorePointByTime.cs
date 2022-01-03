using System;
using System.Collections.Generic;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class DeleteRestorePointByTime : IDeleteRestorePoint
    {
        public void DeleteRestorePoint(Backupjob backupJob, PredicateRestorePoint predicateRestorePoint)
        {
            int count = 0;
            backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
            foreach (RestorePoint restorePoint in backupJob.RestorePoints)
            {
                if (restorePoint.Date < predicateRestorePoint.DateTime)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            backupJob.RestorePoints.RemoveRange(0, count);
        }
    }
}