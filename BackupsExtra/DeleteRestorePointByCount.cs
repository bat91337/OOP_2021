using System;
using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Tools;

namespace BackupsExtra
{
    public class DeleteRestorePointByCount : IDeleteRestorePoint
    {
        public void DeleteRestorePoint(Backupjob backupJob, PredicateRestorePoint predicateRestorePoint)
        {
            int count = backupJob.RestorePoints.Count - predicateRestorePoint.Count;

            if (backupJob.RestorePoints.Count.Equals(predicateRestorePoint.Count))
            {
                throw new BackupExtraException("you cannot delete all points");
            }

            backupJob.RestorePoints.RemoveRange(0, count);
        }
    }
}