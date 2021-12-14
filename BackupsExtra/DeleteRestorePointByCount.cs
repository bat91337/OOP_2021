using System;
using System.Collections.Generic;
using System.Linq;
using Backups;
namespace BackupsExtra
{
    public class DeleteRestorePointByCount : IDeleteRestorePoint
    {
        public void DeleteRestorePoint(Backupjob backupJob, DateTime dateTime, int countRestorePoint)
        {
             backupJob.RestorePoints.OrderBy(x => x.Date).ToList();
             int count = backupJob.RestorePoints.Count - countRestorePoint;

            // кинуть эксепшен, если удаляются все точки
             backupJob.RestorePoints.RemoveRange(0, count);
        }
    }
}