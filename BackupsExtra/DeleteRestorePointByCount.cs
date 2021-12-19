using System;
using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Tools;

namespace BackupsExtra
{
    public class DeleteRestorePointByCount : IDeleteRestorePoint
    {
        public void DeleteRestorePoint(Backupjob backupJob, DateTime dateTime, int countRestorePoint)
        {
             backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
             int count = backupJob.RestorePoints.Count - countRestorePoint;

             if (backupJob.RestorePoints.Count.Equals(countRestorePoint))
             {
                 throw new BackupExtraException("you cannot delete all points");
             }

             backupJob.RestorePoints.RemoveRange(0, count);
        }
    }
}