using System;
using Backups;

namespace BackupsExtra
{
    public interface IDeleteRestorePoint
    {
        public void DeleteRestorePoint(Backupjob backupJob, DateTime dateTime, int countRestorePoint);
    }
}